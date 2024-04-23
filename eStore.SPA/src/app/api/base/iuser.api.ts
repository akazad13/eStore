import { Observable } from 'rxjs';
import { AuthUser } from '../../shared/model/User/authUser.model';

export abstract class IUserApi {
  abstract authUser: AuthUser | null;

  abstract authUser$: Observable<AuthUser | null>;

  abstract Update(userid: number, lang: string): Observable<any>;
}
