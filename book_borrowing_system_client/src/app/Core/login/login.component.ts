import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LoginService } from '../../Shared/Service/login.service';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Login } from '../../Shared/Interface/Login.interface';
import { Router } from '@angular/router';
import { UserStoreService } from '../../Shared/Service/user-store.service';
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
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, ...matModules],
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm!: FormGroup;
  userLogin: Login = {
    Username: '',
    Password: '',
  };

  constructor(
    private fb: FormBuilder,
    private loginService: LoginService,
    private router: Router,
    private userStore: UserStoreService,
    private toast: ToastService
  ) {}
  private subscription: Subscription = new Subscription();

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      Username: new FormControl('', { validators: [Validators.required] }),
      Password: new FormControl('', { validators: [Validators.required] }),
    });
  }

  Login() {
    if (this.loginForm.valid) {
      this.userLogin.Username = this.loginForm.value.Username;
      this.userLogin.Password = this.loginForm.value.Password;
      this.subscription.add(
        this.loginService.login(this.userLogin).subscribe({
          next: (res) => {
            this.loginForm.reset();
            // console.log(res.token);
            this.loginService.storeToken(res.token);

            const tokenPayload = this.loginService.decodeToken();
            this.userStore.setfullnameForStore(tokenPayload.unique_name);
            this.userStore.setTokenForStore(tokenPayload.token);
            this.userStore.setIdForStore(tokenPayload.nameid);
            this.toast.successToast('Login Successful!');
            this.router.navigate(['home']);
          },
          error: (err) => {
            console.log(err);
            this.toast.errorToast('Login Failed!');
          },
        })
      );
    }
  }
  ClearForm() {
    this.loginForm.reset();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
