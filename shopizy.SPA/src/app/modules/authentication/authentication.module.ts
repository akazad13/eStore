import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [AuthenticationRoutingModule, SharedModule],
  declarations: [LoginComponent, ResetPasswordComponent]
})
export class AuthenticationModule {}
