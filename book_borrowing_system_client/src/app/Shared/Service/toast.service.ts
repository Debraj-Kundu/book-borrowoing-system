import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor(private toastr: ToastrService) {}

  successToast(msg:string){
    this.toastr.success(msg);
  }

  errorToast(msg:string){
    this.toastr.error(msg);
  }
}
