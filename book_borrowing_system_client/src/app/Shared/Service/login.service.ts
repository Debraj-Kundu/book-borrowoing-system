import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from '../Interface/Login.interface';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  apiurl = `${environment.baseurl}/UserAccount/login`;

  private userPayload!: any;

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    this.userPayload = this.decodeToken();
  }

  login(loginUser: Login): Observable<any> {
    return this.http.post<any>(`${this.apiurl}`, loginUser);
  }

  storeToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token && !this.jwtHelper.isTokenExpired(token)) return true;
    this.logout();
    return false;
  }

  logout() {
    localStorage.clear();
  }

  decodeToken() {
    const token = this.getToken()!;
    return this.jwtHelper.decodeToken(token);
  }

  getFullNameFromToken() {
    if (this.userPayload) return this.userPayload.unique_name;
  }
  getUserToken() {
    if (this.userPayload) return this.userPayload.token;
  }
  getIdFromToken() {
    if (this.userPayload) return this.userPayload.nameid;
  }
}
