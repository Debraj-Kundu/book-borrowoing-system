import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { User } from '../../Shared/Interface/User.interface';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LoginService } from '../../Shared/Service/login.service';
import { RegisterService } from '../../Shared/Service/register.service';
import { Router } from '@angular/router';
import { ToastService } from '../../Shared/Service/toast.service';
import { Subscription } from 'rxjs';

const matModules = [
  MatFormFieldModule,
  MatCardModule,
  MatButtonModule,
  MatInputModule,
  MatDatepickerModule,
  MatNativeDateModule,
];

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ...matModules],
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm!: FormGroup;
  userInfo: User = {
    id: 0,
    name: '',
    username: '',
    password: '',
    tokens_Available: 0,
    books_Borrowed: [],
    books_Lent: []
  };
  private subscription: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toast: ToastService,
    private registerService: RegisterService
  ) {
    this.registerForm = this.fb.group(
      {
        Name: new FormControl('', {
          validators: [
            Validators.required,
            Validators.maxLength(50),
            Validators.pattern('^[a-zA-Z]+$'),
          ],
        }),
        Username: new FormControl('', {
          validators: [
            Validators.required,
          ],
        }),
        Password: new FormControl('', {
          validators: [
            Validators.required,
          ],
        }),
      },
    );
  }

  ngOnInit(): void {}

  register() {
    if (this.registerForm.valid) {
      this.userInfo.name = this.registerForm.value.Name;
      this.userInfo.username = this.registerForm.value.Username;
      this.userInfo.password = this.registerForm.value.Password;

      this.subscription.add(
        this.registerService.postUser(this.userInfo).subscribe({
          next: (res) => {
            console.log(res);
            this.registerForm.reset();
            this.toast.successToast('SignUp Successful!');
            this.router.navigate(['login']);
          },
          error: (err) => {
            console.log(err);
            this.toast.errorToast('SignUp Failed! Username already exist');
          },
        })
      );
    }
  }

  ClearForm() {
    this.registerForm.reset();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
