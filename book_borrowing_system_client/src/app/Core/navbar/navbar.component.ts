import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginService } from '../../Shared/Service/login.service';
import { UserStoreService } from '../../Shared/Service/user-store.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from '../../Shared/Service/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  username!: string;
  token!: number;

  isLoggedIn: boolean = false;

  constructor(
    private loginService: LoginService,
    private userStore: UserStoreService,
    private userService: UserService,
    private router: Router
  ) {}
  private subscription: Subscription = new Subscription();

  ngOnInit(): void {
    const nameFormToken = this.loginService.getFullNameFromToken();
    this.subscription.add(
      this.userStore.getfullnameFormStore().subscribe((val) => {
        this.username = val || nameFormToken;
        this.isLoggedIn = this.loginService.isLoggedIn();
      })
    );
    const userToken = this.loginService.getUserToken();

    this.subscription.add(
      this.userStore.getTokenFormStore().subscribe((val) => {
        this.token = val || userToken;
        this.isLoggedIn = this.loginService.isLoggedIn();
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
    return uid;
  }

  logout() {
    this.loginService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['/home']);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
