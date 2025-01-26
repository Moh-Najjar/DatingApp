import { HttpInterceptorFn } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError((error) => {
      if (error) {
        switch (error.status) {
          case 400:
            if (error.error.errors) {
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            } else {
              toastr.error(error.error, error.status);
            }
            break;
          case 401:
            toastr.error('Unauthorized', error.status);
            break;
          case 404:
            router.navigate(['/not-found']);
            break;
          case 500:
            router.navigate(['/server-error'], {
              state: { error: error.error },
            });
            break;
          default:
            toastr.error('Something unexpected went wrong');
            break;
        }
      }
      throw error;
    })
  );
};
