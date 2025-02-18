import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { exhaustMap, take } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  console.log('Interceptor triggered'); // Debugging

  return authService.user.pipe(
    take(1), // Get the latest user value and complete
    exhaustMap((user) => {
      if (!user) {
        return next(req); // Proceed without modifying request if user is null
      }

      const modifiedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`,
        },
      });

      return next(modifiedRequest);
    })
  );
};
