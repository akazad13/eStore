import { AuthService } from './../../../shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { finalize, firstValueFrom, Subject } from 'rxjs';
import { IAuthApi } from 'src/app/api/base';
import { handleError } from 'src/app/shared/functions/error-handler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loginInProgress = false;

  successResponse = null;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authApi: IAuthApi,
    authService: AuthService
  ) {
    if (authService.loggedIn()) {
      this.router.navigateByUrl('/dashboard');
    }
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }
  get email() {
    return this.loginForm.get('email') as FormControl;
  }
  get password() {
    return this.loginForm.get('password') as FormControl;
  }

  ngOnInit() {}

  async login(): Promise<void> {
    this.loginForm.markAllAsTouched();

    if (this.loginInProgress || this.loginForm.invalid) {
      return;
    }
    this.loginInProgress = true;
    try {
      const data = await firstValueFrom(
        this.authApi
          .login(this.loginForm.value.email, this.loginForm.value.password)
          .pipe(finalize(() => (this.loginInProgress = false)))
      ).then((value) => {
        this.router.navigateByUrl('/dashboard');
      });
    } catch (error) {
      handleError(this.loginForm, error);
    }
  }

  ngOnDestroy(): void {}
}
