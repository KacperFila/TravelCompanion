import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, tap } from 'rxjs';
import { User } from './user.model';
import { TravelPlan } from '../features/plans/models/plan.models';

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
  constructor(private http: HttpClient) {}

  user = new BehaviorSubject<User | null>(null);

  private activePlanSubject = new BehaviorSubject<TravelPlan | null>(null);
  activePlan$ = this.activePlanSubject.asObservable();

  signup(email: string, password: string) {
    return this.http.post<AuthResponse>(
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

          const storedPlan = localStorage.getItem('activePlan');
          if (storedPlan) {
            this.activePlanSubject.next(JSON.parse(storedPlan));
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('user');
    this.user.next(null);
  }

  autoLogin() {
    const loggedUser = localStorage.getItem('user');
    if (!loggedUser) return;

    const parsedUser = JSON.parse(loggedUser);

    if (!parsedUser._token) {
      console.error('Access token is missing!');
      return;
    }

    const currentUser = new User(
      parsedUser.email,
      parsedUser.id,
      parsedUser.role,
      parsedUser._token,
      parsedUser.claims,
      new Date(parsedUser.expires)
    );

    this.user.next(currentUser);

    const storedPlan = localStorage.getItem('activePlan');
    if (storedPlan) {
      this.activePlanSubject.next(JSON.parse(storedPlan));
    }
  }

  getUserActivePlan(): TravelPlan | null {
    return this.activePlanSubject.getValue();
  }

  updateActivePlan(plan: TravelPlan) {
    localStorage.setItem('activePlan', JSON.stringify(plan));
    this.activePlanSubject.next(plan);
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
