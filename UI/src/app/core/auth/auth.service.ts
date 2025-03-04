import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Subject, tap } from 'rxjs';
import { User } from './user.model';
import { TravelPlan } from '../features/plans/models/plan-models';

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

    console.log('Parsed User Token:', parsedUser.token); // Debugging log

    if (!parsedUser.token) {
      console.error('Access token is missing!');
      return;
    }

    const currentUser = new User(
      parsedUser.email,
      parsedUser.id,
      parsedUser.role,
      parsedUser.token, // Ensure token is passed
      parsedUser.claims,
      new Date(parsedUser.expires)
    );

    console.log('Current User Token:', currentUser.token); // Debugging log

    this.user.next(currentUser);
  }

  getUserActivePlan(): TravelPlan | null {
    const activePlan = localStorage.getItem('activePlan');
    if (activePlan) {
      return JSON.parse(activePlan) as TravelPlan;
    }
    return null;
  }

  updateActivePlan(plan: TravelPlan) {
    const currentUser = this.user.value;
    if (currentUser) {
      currentUser.activePlan = plan;
      localStorage.setItem(
        'activePlan',
        JSON.stringify(currentUser.activePlan)
      );
      this.user.next(currentUser);
    }
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
