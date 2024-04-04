import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserStoreService {
  private fullname$ = new BehaviorSubject<string>('');
  private token$ = new BehaviorSubject<number>(0);
  private id$ = new BehaviorSubject<string>('');

  constructor() {}

  public getIdFormStore(): Observable<string> {
    return this.id$.asObservable();
  }
  public setIdForStore(id: string) {
    this.id$.next(id);
  }

  public getTokenFormStore(): Observable<number> {
    return this.token$.asObservable();
  }
  public setTokenForStore(token: number) {
    this.token$.next(token);
  }

  public getfullnameFormStore(): Observable<string> {
    return this.fullname$.asObservable();
  }
  public setfullnameForStore(fullname: string) {
    this.fullname$.next(fullname);
  }
}
