import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Interface/User.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  apiurl = `${environment.baseurl}/UserAccount/register`;

  constructor(private http: HttpClient) {}

  postUser(user: any) {
    return this.http.post<any>(`${this.apiurl}`, user);
  }
}
