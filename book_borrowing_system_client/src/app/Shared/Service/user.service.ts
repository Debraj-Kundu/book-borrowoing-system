import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User } from '../Interface/User.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiurl = `${environment.baseurl}/UserAccount`;

  constructor(private http: HttpClient) {}

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiurl}/${id}`);
  }
}
