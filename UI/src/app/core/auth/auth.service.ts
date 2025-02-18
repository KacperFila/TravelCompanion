import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Subject, tap } from 'rxjs';
import { User } from './user.model';

interface AuthResponse {
  email: string;
  accessToken: string;
  refreshToken: string | null;
  expires: number;
  id: string;
  role: string;
  claims: {
    permissions: string[];
  };
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  user = new BehaviorSubject<User | null>(null);

  constructor(private http: HttpClient) {}

  signup(email: string, password: string) {
    return this.http.post<AuthResponse>(
      `${environment.apiBaseUrl}/users-module/account/sign-up`,
      {
        email: email,
        password: password,
        role: 'user',
        claims: {
          permissions: ['users'],
        },
      }
    );
  }

  login(email: string, password: string) {
    return this.http
      .post<AuthResponse>(
        `${environment.apiBaseUrl}/users-module/account/sign-in`,
        {
          email: email,
          password: password,
        }
      )
      .pipe(
        tap((res) => {
          this.handleAuthentication(
            res.email,
            res.id,
            res.role,
            res.accessToken,
            res.expires,
            res.claims
          );
        })
      );
  }

  private handleAuthentication(
    email: string,
    id: string,
    role: string,
    accessToken: string,
    expires: number,
    claims: { permissions: string[] }
  ) {
    const expiresAt = new Date(expires);
    const user = new User(email, id, role, accessToken, claims, expiresAt);

    this.user.next(user);
  }
}
