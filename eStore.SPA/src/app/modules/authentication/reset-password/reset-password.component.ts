import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { mustMatchValidator } from 'src/app/shared/functions/must-match';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  private destroy$: Subject<void> = new Subject<void>();
  resetToken!: string;
  resetForm!: FormGroup;
  resetInProgress = false;
  successResponse = null;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  get password() {
    return this.resetForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.resetForm.get('confirmPassword') as FormControl;
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: any) => {
      this.resetToken = params.resetToken;
    });

    this.resetForm = this.fb.group(
      {
        password: ['', [Validators.required]],
        confirmPassword: ['', [Validators.required]]
      },
      { validators: [mustMatchValidator('password', 'confirmPassword')] }
    );
  }
  reset(): void {
    this.resetForm.markAllAsTouched();

    if (this.resetInProgress || this.resetForm.invalid) {
      return;
    }

    this.resetInProgress = true;

    // this.account
    //   .resetPassword(this.resetForm.value.password, this.resetToken)
    //   .pipe(
    //     finalize(() => (this.resetInProgress = false)),
    //     takeUntil(this.destroy$)
    //   )
    //   .subscribe(
    //     (response: any) => {
    //       this.successResponse = response.message;
    //     },
    //     (error) => {
    //       if (error instanceof HttpErrorResponse) {
    //         this.resetForm.setErrors({
    //           server: error.error.message
    //         });
    //       } else {
    //         alert(error);
    //       }
    //     }
    //   );
  }
}
