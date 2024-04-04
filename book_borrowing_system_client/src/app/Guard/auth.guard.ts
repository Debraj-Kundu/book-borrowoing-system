import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../Shared/Service/login.service';
import { UserStoreService } from '../Shared/Service/user-store.service';
import { ToastService } from '../Shared/Service/toast.service';

export const AuthGuard = () => {
  const router = inject(Router);
  const loginService = inject(LoginService);
  const toast = inject(ToastService);

  if (loginService.isLoggedIn()) {
    return true;
  }
  router.navigate(['/login']);
  toast.errorToast('Login first');
  return false;
};

