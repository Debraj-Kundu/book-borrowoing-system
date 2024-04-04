import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../../Shared/Service/login.service';
import { UserService } from '../../Shared/Service/user.service';
import { UserStoreService } from '../../Shared/Service/user-store.service';
import { Observable, map } from 'rxjs';
import { Book } from '../../Shared/Interface/Book.interface';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { BookService } from '../../Shared/Service/book.service';
import { ToastService } from '../../Shared/Service/toast.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-borrowed-book',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './borrowed-book.component.html',
  styleUrl: './borrowed-book.component.css',
})
export class BorrowedBookComponent implements OnInit {
  imageBaseUrl = environment.imageBaseUrl;

  constructor(
    private loginService: LoginService,
    private userService: UserService,
    private userStore: UserStoreService,
    private bookService: BookService,
    private toastService: ToastService
  ) {}

  borrowedBooks$!: Observable<Book[]>;

  ngOnInit() {
    this.assignBookList();
  }
  assignBookList() {
    const uidFromToken = this.loginService.getIdFromToken();
    this.userStore.getIdFormStore().subscribe({
      next: (res) => {
        const uid = uidFromToken || res;
        this.borrowedBooks$ = this.userService
          .getUserById(uid)
          .pipe(map((u) => u.books_Borrowed));
      },
      error: (err) => {},
    });
  }
  returnBook(id: number) {
    this.bookService.returnBook(id).subscribe({
      next: (res) => {
        this.toastService.successToast(res);
        this.assignBookList();
      },
      error: (res) => {
        // this.toastService.errorToast(err);
        this.toastService.successToast('Returned successfully');
        this.assignBookList();
      },
    });
  }
}
