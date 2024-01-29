import { Observable } from 'rxjs';
import { AuthUser } from '../../shared/model/User/authUser.model';

export abstract class IAuthApi {
  abstract authUser: AuthUser | null;

  abstract authUser$: Observable<AuthUser | null>;

  abstract login(email: string, password: string): Observable<any>;
  abstract forgotPassword(phone: string): Observable<any>;
  abstract resetPassword(
    userId: number,
    newPassword: string,
    resetCode: string
  ): Observable<any>;
  abstract setUser(authUser: AuthUser | null): void;
}
