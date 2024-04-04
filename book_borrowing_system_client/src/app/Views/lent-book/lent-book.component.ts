import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../../Shared/Service/login.service';
import { UserService } from '../../Shared/Service/user.service';
import { User } from '../../Shared/Interface/User.interface';
import { UserStoreService } from '../../Shared/Service/user-store.service';
import { Observable, map } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { Book } from '../../Shared/Interface/Book.interface';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-lent-book',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './lent-book.component.html',
  styleUrl: './lent-book.component.css',
})
export class LentBookComponent implements OnInit {
  imageBaseUrl = environment.imageBaseUrl;

  constructor(
    private loginService: LoginService,
    private userService: UserService,
    private userStore: UserStoreService
  ) {}

  lentBooks$!: Observable<Book[]>;

  ngOnInit() {
    this.assignBooks();
  }
  assignBooks() {
    const uidFromToken = this.loginService.getIdFromToken();
    this.userStore.getIdFormStore().subscribe({
      next: (res) => {
        const uid = uidFromToken || res;
        this.lentBooks$ = this.userService
          .getUserById(uid)
          .pipe(map((u) => u.books_Lent));
      },
      error: (err) => {},
    });
  }
}
