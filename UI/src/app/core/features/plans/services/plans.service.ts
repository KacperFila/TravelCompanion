import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, switchMap, tap } from 'rxjs';
import { AuthService } from '../../../auth/auth.service';
import { TravelPlan, TravelPlanResponse } from '../models/plan-models';

@Injectable({ providedIn: 'root' })
export class PlansService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  createPlan(
    title: string,
    description: string | null,
    from: Date | null,
    to: Date | null
  ): Observable<TravelPlan> {
    const requestBody = { title, description, from, to };

    return this.http
      .post(`${environment.apiBaseUrl}/travelplans-module/Plan`, requestBody)
      .pipe(
        switchMap(() => this.getUserLastPlan()),
        tap((response) => {
          if (response.items.length > 0) {
            this.authService.updateActivePlan(response.items[0]);
          }
        }),
        switchMap((response) => response.items)
      );
  }

  setActivePlan(travelPlan: TravelPlan) {
    this.authService.updateActivePlan(travelPlan);
  }

  getPlansForUser(): Observable<TravelPlanResponse> {
    return this.http.get<TravelPlanResponse>(
      `${environment.apiBaseUrl}/travelplans-module/Plan`
    );
  }

  getUserLastPlan(): Observable<TravelPlanResponse> {
    let params = new HttpParams()
      .set('Page', 0)
      .set('Results', 1)
      .set('OrderBy', 'createdOnUtc')
      .set('SortOrder', 'DESC');

    return this.http.get<TravelPlanResponse>(
      `${environment.apiBaseUrl}/travelplans-module/Plan`,
      { params }
    );
  }
}
