import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { exhaustMap, take } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  return authService.user.pipe(
    take(1),
    exhaustMap((user) => {
      if (!user) {
        return next(req);
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
