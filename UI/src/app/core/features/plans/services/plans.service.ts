import {Injectable} from '@angular/core';
import {environment} from '../../../../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {map, Observable, switchMap, tap} from 'rxjs';
import {AuthService} from '../../../auth/auth.service';
import {TravelPlan, TravelPlanResponse, TravelPoint, TravelPointUpdateRequest} from '../models/plan.models';

@Injectable({ providedIn: 'root' })
export class PlansService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  createPlan(
    title: string,
    description: string | null,
    from: Date | null,
    to: Date | null
  ): Observable<void> {
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
        map(() => void 0)
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

  getActivePlanWithPoints(planId: string): Observable<TravelPlan> {
    return this.http.get<TravelPlan>(
      `${environment.apiBaseUrl}/travelplans-module/Plan/${planId}/Points`
    );
  }

  addPointToPlan(travelPlanId: string, placeName: string): Observable<void> {
    const requestBody = { travelPlanId, placeName };

    return this.http
      .post(`${environment.apiBaseUrl}/travelplans-module/Point`, requestBody)
      .pipe(map(() => void 0));
  }

  deletePoint(travelPointId: string): Observable<void> {
    return this.http
      .delete(
        `${environment.apiBaseUrl}/travelplans-module/Point/${travelPointId}`
      )
      .pipe(map(() => void 0));
  }

  updatePoint(travelPoint: TravelPoint): Observable<void> {
    const requestBody = {
      pointId: travelPoint.id,
      placeName: travelPoint.placeName
    };

    return this.http
      .put(
        `${environment.apiBaseUrl}/travelplans-module/Point/Update`,
        requestBody
      )
      .pipe(map(() => void 0));
  }

  getTravelPointEditRequests(travelPointId: string): Observable<TravelPointUpdateRequest[]>
  {
    return this.http
      .get<TravelPointUpdateRequest[]>(
        `${environment.apiBaseUrl}/travelplans-module/Point/${travelPointId}/UpdateRequests`)
  }
}
