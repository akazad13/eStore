import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { JwtModule } from '@auth0/angular-jwt';

import { ApiModule } from './api/implementation/api.module';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

import { AppComponent } from './app.component';
import { environment } from 'src/environments/environment';
import { AuthGuard } from './shared/guard/auth.guard';

export function tokenGetter(): string {
  const storedUser = localStorage.getItem('user');
  const user = storedUser == null ? null : JSON.parse(storedUser);
  if (user == null) {
    return user;
  }
  return user.jwt;
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    ApiModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: [environment.apiUrl.split('//')[1]], // needs to remove the https:// portion
        skipWhenExpired: true,
        disallowedRoutes: [`${environment.apiUrl.split('//')[1]} + /api/auth`]
      }
    })
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule {}
