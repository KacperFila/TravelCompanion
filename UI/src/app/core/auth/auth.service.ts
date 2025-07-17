import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, switchMap, tap } from 'rxjs';
import { User } from './user.model';
import {environment} from "../../../environments/environment.prod";

interface AccountDTO {
  email: string;
  accessToken: string;
  refreshToken: string | null;
  expires: number;
  id: string;
  role: string;
  activePlanId: string;
  activeTravelId: string;
  claims: {
    permissions: string[];
  };
}

interface JsonWebToken {
  accessToken: string,
  refreshToken: string | null,
  expires: number,
  id: string,
  role: string,
  email: string,
  claims: {
    permissions: string[];
  };
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  user = new BehaviorSubject<User | null>(null);

  signup(email: string, password: string) {
    return this.http.post(
      `${environment.apiBaseUrl}/users-module/account/sign-up`,
      {
        email: email,
        password: password,
        role: 'user',
        claims: {
          permissions: ['users'],
          activePlanId: [],
        },
      }
    );
  }

  login(email: string, password: string) : Observable<any> {
    return this.http
      .post<JsonWebToken>(
        `${environment.apiBaseUrl}/users-module/account/sign-in`,
        {
          email: email,
          password: password,
        }
      )
      .pipe(
        switchMap((token) => {
          return this.handleAuthentication(token.accessToken);
        })
      );
  }

  logout() {
    localStorage.removeItem('user');
    this.user.next(null);
  }

  autoLogin() {
    const loggedUser = localStorage.getItem('user');
    if (!loggedUser)
    {
      return;
    }

    const parsedUser = JSON.parse(loggedUser);

    if (!parsedUser._token) {
      console.error('Access token is missing!');
      return;
    }

    const currentUser = new User(
      parsedUser.email,
      parsedUser.id,
      parsedUser.role,
      parsedUser.activePlanId,
      parsedUser.activeTravelId,
      parsedUser._token,
      parsedUser.claims,
      new Date(parsedUser.expires)
    );

    this.user.next(currentUser);
  }

  private handleAuthentication(token: string): Observable<any> {
    return this.http
      .get<AccountDTO>(`${environment.apiBaseUrl}/users-module/account/info`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .pipe(
        tap((response) => {
          const expiresAt = new Date(response.expires);
          const user = new User(
            response.email,
            response.id,
            response.role,
            response.activePlanId,
            response.activeTravelId,
            token,
            response.claims,
            expiresAt
          );

          localStorage.setItem('user', JSON.stringify(user));
          this.user.next(user);
        }
      )
    );
  }
}
