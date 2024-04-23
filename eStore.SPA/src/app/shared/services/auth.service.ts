import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private jwtHelper: JwtHelperService) {}

  getCurrentUserId(): number {
    const decodedToken = this.getDecodedToken();
    if (decodedToken == null) {
      return decodedToken;
    }
    return +decodedToken.nameid;
  }
  getCurrentUserFirstName(): string {
    const user = this.getStoredUser();
    if (user == null) {
      return user;
    }
    return user.firstName;
  }

  getCurrentUserLocale(): string {
    const user = this.getStoredUser();
    if (user == null) {
      return user;
    }
    return user.locale;
  }
  setCurrentUserLocale(locale : string) {
    const user = this.getStoredUser();
    if (user == null) {
      return user;
    }
    user.locale = locale;
    this.SetUserToStore(user);
  }

  getDecodedToken(): any {
    const user = this.getStoredUser();
    if (user == null) {
      return user;
    }
    return this.jwtHelper.decodeToken(user.jwt);
  }

  getToken(): string {
    const user = this.getStoredUser();
    if (user == null) {
      return user;
    }
    return user.jwt;
  }

  loggedIn(): boolean {
    const user = this.getStoredUser();
    return !!user && !this.jwtHelper.isTokenExpired(user.jwt);
  }

  roleMatch(allowedRoles: string[]): boolean {
    let isMatch = false;
    const decodedToken = this.getDecodedToken();
    if (!decodedToken) {
      return false;
    }
    const userRoles = decodedToken.role as Array<string>;
    if (!userRoles) {
      return false;
    }
    allowedRoles.forEach((element) => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  isRoleExist(role: string): boolean {
    const decodedToken = this.getDecodedToken();
    if (!decodedToken) {
      return false;
    }
    const userRoles = decodedToken.role as Array<string>;
    if (!userRoles) {
      return false;
    }
    return userRoles.includes(role);
  }

  selectedRole(): string | null {
    return localStorage.getItem('selectedRole');
  }
  private getStoredUser() {
    const storedUser = localStorage.getItem('user');
    return storedUser == null ? null : JSON.parse(storedUser);
  }
  private SetUserToStore(user: any) {
    localStorage.setItem('user', JSON.stringify(user));
  }
}
