import {Injectable} from '@angular/core';
import {environment} from '../../../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {map, Observable, switchMap, tap} from 'rxjs';
import {TravelPlan, TravelPoint, TravelPointUpdateRequest} from '../../models/plan.models';
import {PlanInvitationResponse} from "./plans-signalR-responses.models";

@Injectable({ providedIn: 'root' })
export class PlansService {
  constructor(private http: HttpClient) {}

  createPlan(
    title: string,
    description: string | null,
    from: string | null,
    to: string | null) : Observable<void>
  {
    const requestBody = { title, description, from, to };

    return this.http
      .post(`${environment.apiBaseUrl}/travelplans-module/Plan`, requestBody)
      .pipe(
        switchMap(() => this.getUserLastPlan()),
        tap((response) => {
        }),
        map(() => void 0)
      );
  }

  setActivePlan(planId: string) : Observable<any> {
    const requestBody = { planId };
    return this.http
      .post(`${environment.apiBaseUrl}/travelplans-module/Plan/Active`, requestBody);
  }

  getPlansForUser(): Observable<TravelPlan[]> {
    return this.http.get<TravelPlan[]>(
      `${environment.apiBaseUrl}/travelplans-module/Plan`
    );
  }

  getUserLastPlan(): Observable<TravelPlan[]> {
    return this.http.get<TravelPlan[]>(
      `${environment.apiBaseUrl}/travelplans-module/Plan`
    );
  }

  getActivePlanWithPoints(): Observable<TravelPlan> {
    return this.http.get<TravelPlan>(
      `${environment.apiBaseUrl}/travelplans-module/Plan/Active`,
      {}
    );
  }

  addPointToPlan(travelPlanId: string, placeName: string): Observable<any> {
    const requestBody = { travelPlanId, placeName };

    return this.http
      .post(`${environment.apiBaseUrl}/travelplans-module/Point`, requestBody);
  }

  deletePoint(travelPointId: string): Observable<any>{
    return this.http
      .delete(
        `${environment.apiBaseUrl}/travelplans-module/Point/${travelPointId}`
      );
  }

  updatePoint(travelPoint: TravelPoint): Observable<any> {
    const requestBody = {
      pointId: travelPoint.id,
      placeName: travelPoint.placeName
    };

    return this.http
      .put(
        `${environment.apiBaseUrl}/travelplans-module/Point/Update`,
        requestBody
      )
  }

  getTravelPointEditRequests(pointId: string): Observable<TravelPointUpdateRequest[]>
  {
    return this.http
      .get<TravelPointUpdateRequest[]>(
        `${environment.apiBaseUrl}/travelplans-module/Point/${pointId}/UpdateRequests`)
  }

  acceptUpdateRequest(updateRequestId: string): Observable<any> {
    return this.http
      .put(
        `${environment.apiBaseUrl}/travelplans-module/Point/Update/${updateRequestId}/Acceptance`,
        {}
      );
  }

  rejectUpdateRequest(updateRequestId: string): Observable<any> {
    return this.http
      .delete(
        `${environment.apiBaseUrl}/travelplans-module/Point/Update/${updateRequestId}/Rejection`,
        {}
      );
  }

  inviteUserToPlan(planId: string, inviteeId: string) {
    return this.http.post(
      `${environment.apiBaseUrl}/travelplans-module/Invitation/${planId}/${inviteeId}`,
      {}
    );
  }

  acceptPlanInvitation(invitationId: string)
  {
    return this.http
      .put(
        `${environment.apiBaseUrl}/travelplans-module/Invitation/${invitationId}/Acceptance`, null
      );
  }

  rejectPlanInvitation(invitationId: string)
  {
    return this.http
      .delete(
        `${environment.apiBaseUrl}/travelplans-module/Invitation/${invitationId}/Rejection`
      );
  }

  getInvitationsForUser()
  {
    return this.http.get<PlanInvitationResponse[]>(
      `${environment.apiBaseUrl}/travelplans-module/Invitation`
    );
  }
}
