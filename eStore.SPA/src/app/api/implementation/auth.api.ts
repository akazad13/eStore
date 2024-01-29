import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { AuthUser } from '../../shared/model/User/authUser.model';
import { IAuthApi } from '../base';

@Injectable()
export class AuthApi extends IAuthApi {
  private userSubject: BehaviorSubject<AuthUser | null>;
  authUrl = environment.apiUrl + '/api/auth/';

  get authUser(): AuthUser | null {
    return this.userSubject.value;
  }

  readonly authUser$: Observable<AuthUser | null>;

  constructor(private http: HttpClient) {
    super();

    const storedUser = localStorage.getItem('user');

    this.userSubject = new BehaviorSubject<AuthUser | null>(
      storedUser ? JSON.parse(storedUser) : null
    );
    this.authUser$ = this.userSubject.asObservable();
  }

  login(email: string, password: string): Observable<any> {
    return this.http
      .post<AuthUser>(this.authUrl + 'login', {
        email,
        password
      })
      .pipe(tap((user) => this.setUser(user)));
  }

  forgotPassword(phone: string): Observable<any> {
    return this.http.post<any>(this.authUrl + 'forgot-password', {
      phoneNumber: phone
    });
  }

  resetPassword(
    userid: number,
    newPassword: string,
    resetCode: string
  ): Observable<any> {
    return this.http.post<any>(this.authUrl + 'reset-password', {
      userid,
      newPassword,
      resetCode
    });
  }

  setUser(authUser: AuthUser | null): void {
    this.userSubject.next(authUser);

    localStorage.setItem('user', JSON.stringify(authUser));
  }
}
