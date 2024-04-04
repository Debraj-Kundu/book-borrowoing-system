import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookService } from '../../Shared/Service/book.service';
import { Book } from '../../Shared/Interface/Book.interface';
import { Observable, Subscription, map } from 'rxjs';

import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSort, MatSortModule } from '@angular/material/sort';

import { LoginService } from '../../Shared/Service/login.service';
import { UserStoreService } from '../../Shared/Service/user-store.service';
import { Router, RouterModule } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ToastService } from '../../Shared/Service/toast.service';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../Shared/Service/user.service';
import { User } from '../../Shared/Interface/User.interface';
import { environment } from '../../../environments/environment';
import { BookComponent } from '../book/book.component';
const matModules = [
  MatButtonModule,
  MatTableModule,
  MatPaginatorModule,
  MatIconModule,
  MatDialogModule,
  MatSelectModule,
  MatFormFieldModule,
  MatSortModule,
  MatInputModule,
  MatFormFieldModule,
];

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    ...matModules,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(
    private bookService: BookService,
    private loginService: LoginService,
    private userStore: UserStoreService,
    private userService: UserService,
    private router: Router,
    private toast: ToastService,
    private fb: FormBuilder,
    private dialog: MatDialog
  ) {}

  imageBaseUrl = environment.imageBaseUrl;
  uid!: number;
  booksList$: Observable<Book[]> = this.bookService.getAllBooks();

  selected!: any;
  searchBox!: FormGroup;
  searchText = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayColumns: string[] = ['name', 'author', 'genre', 'image', 'actions'];
  private dataSource = new MatTableDataSource<Book>();

  tableData$: Observable<any> = this.booksList$.pipe(
    map((item) => {
      const dataSource = this.dataSource;
      dataSource.data = item;
      dataSource.paginator = this.paginator;
      dataSource.sort = this.sort;
      return dataSource;
    })
  );

  private subscription: Subscription = new Subscription();

  ngOnInit(): void {
    this.searchBox = this.fb.group({
      search: new FormControl(''),
    });
    this.uid = this.getUserId();
  }

  valueChange(value: any) {
    if (value == null) {
      this.tableData$ = this.bookService.getAllBooks().pipe(
        map((item) => {
          const dataSource = this.dataSource;
          dataSource.data = item;
          dataSource.paginator = this.paginator;
          dataSource.sort = this.sort;
          return dataSource;
        })
      );
      return;
    }
    this.toast.successToast(value.name);
    this.tableData$ = this.booksList$.pipe(
      map((item) => {
        const dataSource = this.dataSource;
        dataSource.paginator = this.paginator;
        dataSource.sort = this.sort;
        return dataSource;
      })
    );
  }
  filterByName() {
    this.tableData$ = this.booksList$.pipe(
      map((item) => {
        const dataSource = this.dataSource;
        dataSource.data = item.filter(
          (prod) =>
            prod.name
              .toLocaleLowerCase()
              .includes(this.searchBox.value.search.toLocaleLowerCase()) ||
            prod.author
              .toLocaleLowerCase()
              .includes(this.searchBox.value.search.toLocaleLowerCase()) ||
            prod.genre
              .toLocaleLowerCase()
              .includes(this.searchBox.value.search.toLocaleLowerCase())
        );
        dataSource.paginator = this.paginator;
        dataSource.sort = this.sort;
        return dataSource;
      })
    );
  }

  borrowBook(id: string) {
    const loggedIn: Boolean = this.loginService.isLoggedIn();
    if (!loggedIn) {
      this.router.navigate(['/login']);
      return;
    }

    this.subscription.add(
      this.bookService.borrowBook(id).subscribe({
        next: (res) => {
          // this.toast.successToast('Book borrowed successfully!');
        },
        error: (err) => {
          
          this.userStore.getIdFormStore().subscribe((uid) => {
            this.userService.getUserById(Number(uid)).subscribe({
              next: (res) => {
                this.toast.successToast('Book borrowed successfully!');
                this.assingBookList(res);
                this.userStore.setTokenForStore(res.tokens_Available);
              },
              error: (res) => {
                this.toast.errorToast('Not enough tokens');
                this.assingBookList(res);
                console.log('error ' + res.tokens_Available);
              },
            });
          });
        },
      })
    );
  }
  assingBookList(res: User) {
    this.booksList$ = this.bookService.getAllBooks();
    this.tableData$ = this.booksList$.pipe(
      map((item) => {
        const dataSource = this.dataSource;
        dataSource.data = item;
        dataSource.paginator = this.paginator;
        dataSource.sort = this.sort;
        return dataSource;
      })
    );
  }

  getUserId() {
    const uidFromToken = this.loginService.getIdFromToken();
    var uid = uidFromToken;
    this.userStore.getIdFormStore().subscribe({
      next: (res) => {
        uid = uidFromToken || res;
      },
      error: (err) => {},
    });
    console.log(uid);
    return uid;
  }

  openPopUp(id: number) {
    this.dialog.open(BookComponent, {
      width: '60%',
      data: {
        id,
      },
    });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
