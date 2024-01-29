import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup } from '@angular/forms';

export function handleError(form: FormGroup | null, e: any): void {
  let errorMessage = '';
  if (e instanceof HttpErrorResponse) {
    switch (e.status) {
      case 400: {
        if (!!e.error.errors && e.error.errors != null) {
          errorMessage = e.error.errors.join('\n');
        } else {
          errorMessage = e.error.message;
        }
        break;
      }
      case 404: {
        errorMessage =
          'Failed to find service to process the request. Please contact the administrator.';
        break;
      }

      case 0:
      case 401:
      case 403:
      case 405: {
        errorMessage = e.statusText;
        break;
      }

      case 500:
      case 429: {
        errorMessage =
          'We are unable to process your request at this time. Please try again later';
        break;
      }

      default: {
        errorMessage = e.error.message;
      }
    }
  } else {
    errorMessage = e.message;
  }

  if (!!form) {
    form.setErrors({
      server: errorMessage
    });
  } else {
    alert(errorMessage);
  }
}
