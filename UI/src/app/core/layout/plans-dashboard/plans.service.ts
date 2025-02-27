import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PlanDetailsDTO {
  id: string;
  ownerId: string;
  participants: string[];
  title: string;
  description: string | null;
  from: string;
  to: string;
  additionalCostsValue: number;
  totalCostValue: number;
  planStatus: string;
}

interface TravelPlan {
  id: string;
  ownerId: string;
  participants: string[];
  title: string;
  description: string;
  from: string;
  to: string;
  additionalCostsValue: number;
  totalCostValue: number;
  planStatus: string;
}

interface TravelPlanResponse {
  items: TravelPlan[];
  empty: boolean;
  currentPage: number;
  resultsPerPage: number;
  totalPages: number;
  totalResults: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

@Injectable({ providedIn: 'root' })
export class PlansService {
  constructor(private http: HttpClient) {}

  createPlan(
    title: string,
    description: string | null,
    from: Date | null,
    to: Date | null
  ): Observable<any> {
    const requestBody = { title, description, from, to };

    return this.http.post(
      `${environment.apiBaseUrl}/travelplans-module/Plan`,
      requestBody
    );
  }

  getPlansForUser(): Observable<TravelPlanResponse> {
    return this.http.get<TravelPlanResponse>(
      `${environment.apiBaseUrl}/travelplans-module/Plan`
    );
  }

  setActivePlan(planId: string): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/travelplans-module/Plan/Active`,
      { planId }
    );
  }

  getActivePlan(userId: string): Observable<PlanDetailsDTO> {
    const requestBody = { userId: userId };

    return this.http.post<PlanDetailsDTO>(
      `${environment.apiBaseUrl}/travelplans-module/Plan/Active`,
      requestBody
    );
  }
}
