import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BookService } from '../../Shared/Service/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../Shared/Service/toast.service';
import { Book } from '../../Shared/Interface/Book.interface';
import { User } from '../../Shared/Interface/User.interface';
import { MatSelectModule } from '@angular/material/select';
import { Subscription } from 'rxjs';

const matModules = [
  MatFormFieldModule,
  MatCardModule,
  MatButtonModule,
  MatInputModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatSelectModule,
];
@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ...matModules],
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css'],
})
export class AddBookComponent implements OnInit, OnDestroy {
  constructor(
    private bookService: BookService,
    private router: Router,
    private toast: ToastService,
    private fb: FormBuilder,
  ) {}

  id: string | null = '';
  book: Book = {
    name: '',
    description: '',
    author: '',
    genre: '',
    rating: 0,
    is_book_available: true,
    bookImage: '',
    imageFile: new File([], ''),
    id: 0,
    lent_by_user_id: 0,
    currently_borrowed_by_user_id: 0,
    lentByUser: {
      id: 0,
      name: '',
      username: '',
      password: '',
      tokens_Available: 0,
      books_Borrowed: [],
      books_Lent: []
    }
  };

  imageFile!: File;
  productForm!: FormGroup;

  private subscription: Subscription = new Subscription();

  selected!: any;

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: new FormControl('', {
        validators: [
          Validators.required,
          Validators.maxLength(100),
          Validators.pattern('^[a-zA-Z0-9 ]+$'),
        ],
      }),
      description: new FormControl('', {
        validators: [
          Validators.required,
          Validators.maxLength(500),
          // Validators.pattern('^[a-zA-Z0-9 ]+$'),
        ],
      }),
      rating: new FormControl('', { validators: [Validators.required, Validators.max(5), Validators.min(1)] }),
      author: new FormControl('', {validators: [Validators.required]}),
      genre: new FormControl('', { validators: [Validators.required] }),
      image: new FormControl(''),
      id: new FormControl(this.id),
    });
  }

  addProduct() {
    if (this.productForm.valid) {
      // this.productForm.value.categoryId = this.selected.id;
      const formData: Book = Object.assign(this.productForm.value);
      formData.imageFile = this.imageFile;
      this.subscription.add(
        this.bookService.postBook(formData).subscribe({
          next: (res) => {
            this.toast.successToast('Product added successfully!');
            this.router.navigate(['/home']);
          },
          error: (res) => {
            this.toast.errorToast('Error occured retry!');
          },
        })
      );
    }
  }

  onChange(event: any) {
    this.imageFile = event.target.files[0];
  }

  clearForm() {
    this.productForm.reset();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
