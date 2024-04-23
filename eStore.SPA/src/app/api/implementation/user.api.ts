import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { AuthUser } from '../../shared/model/User/authUser.model';
import { IUserApi } from '../base';

@Injectable()
export class UserApi extends IUserApi {
  private userSubject: BehaviorSubject<AuthUser | null>;
  userUrl = environment.apiUrl + '/api/user/';

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

  Update(userid: number, lang: string): Observable<any> {
    return this.http
      .patch<any>(this.userUrl + userid, {
        locale: lang
      });
  }

}
