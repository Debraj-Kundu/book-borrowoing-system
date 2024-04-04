import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookService } from '../../Shared/Service/book.service';
import { LoginService } from '../../Shared/Service/login.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, map } from 'rxjs';
import { Book } from '../../Shared/Interface/Book.interface';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { UserStoreService } from '../../Shared/Service/user-store.service';
import { ToastService } from '../../Shared/Service/toast.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { environment } from '../../../environments/environment';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';

const matModules = [
  MatFormFieldModule,
  MatInputModule,
  MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatDialogModule,
];

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ...matModules],
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css'],
})
export class BookComponent implements OnInit, OnDestroy {
  constructor(
    private bookService: BookService,
    private loginService: LoginService,
    private route: ActivatedRoute,
    private router: Router,
    private userStore: UserStoreService,
    private toast: ToastService,
    private fb: FormBuilder,
    private ref: MatDialogRef<BookComponent>,
    @Inject(MAT_DIALOG_DATA) public bookId: any
  ) {}

  id: string | null = '';
  book$!: Observable<Book>;

  imageBaseUrl = environment.imageBaseUrl;
  private subscription: Subscription = new Subscription();

  isLoggedIn: boolean = false;

  ngOnInit(): void {
    // this.id = this.route.snapshot.paramMap.get('id');
    this.id = this.bookId.id.toString();
    console.log(this.bookId);
    this.book$ = this.bookService.getBookById(this.id);
    this.subscription.add(
      this.userStore.getfullnameFormStore().subscribe((val) => {
        this.isLoggedIn = this.loginService.isLoggedIn();
      })
    );
  }
  closePopUp() {
    this.ref.close();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
