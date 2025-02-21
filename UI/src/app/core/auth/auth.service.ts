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

  logout() {
    this.user.next(null);
    localStorage.removeItem('user');
  }

  autoLogin() {
    const loggedUser = localStorage.getItem('user');

    if (!loggedUser) {
      return;
    }

    const parsedUser: {
      email: string;
      accessToken: string;
      refreshToken: string | null;
      expires: number;
      id: string;
      role: string;
      claims: {
        permissions: string[];
      };
    } = JSON.parse(loggedUser);

    const currentUser = new User(
      parsedUser.email,
      parsedUser.id,
      parsedUser.role,
      parsedUser.accessToken,
      parsedUser.claims,
      new Date(parsedUser.expires)
    );

    this.user.next(currentUser);
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

    localStorage.setItem('user', JSON.stringify(user));

    this.user.next(user);
  }
}
