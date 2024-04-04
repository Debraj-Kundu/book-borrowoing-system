import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Guard/auth.guard';

const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./Core/login/login.component').then((c) => c.LoginComponent),
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./Core/register/register.component').then(
        (c) => c.RegisterComponent
      ),
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./Views/home/home.component').then((c) => c.HomeComponent),
  },
  {
    path: 'lent-book',
    loadComponent: () =>
      import('./Views/lent-book/lent-book.component').then((c) => c.LentBookComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'borrowed-book',
    loadComponent: () =>
      import('./Views/borrowed-book/borrowed-book.component').then((c) => c.BorrowedBookComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'add',
    loadComponent: () =>
      import('./Views/add-book/add-book.component').then(
        (c) => c.AddBookComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    loadComponent: () =>
      import('./Core/error-page/error-page.component').then(
        (c) => c.ErrorPageComponent
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
