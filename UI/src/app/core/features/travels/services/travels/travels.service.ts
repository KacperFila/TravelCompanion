import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TravelDetailsDto} from "../../models/travel.models";
import {CommonTravelCompanion} from "../../../stats/models/stats.models";
import {environment} from "../../../../../../environments/environment.prod";

@Injectable({ providedIn: 'root' })
export class TravelsService {
  constructor(private http: HttpClient) {}

  getActiveTravel(): Observable<TravelDetailsDto> {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Active`
    );
  }

  getTravelsForUser(): Observable<TravelDetailsDto[]> {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Browse`
    );
  }

  setActiveTravel(travelId: string): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/${travelId}/Active`,
      null
    )
  }

  rateTravel(travelId: string, rating: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/${travelId}/Rating`,
      rating
    );
  }

  markPointAsVisited(pointId: string): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Point/${pointId}/Visitation`,
      null
    )
  }

  markPointAsUnvisited(pointId: string): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Point/${pointId}/Unvisitation`,
      null
    )
  }

  completeTravel(travelId: string): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/${travelId}/Complete`,
      null
    )
  }

  getTravelsCount(): Observable<number> {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Count`
    );
  }

  getFinishedTravelsCount(): Observable<number> {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Count/Finished`
    );
  }

  getUpcomingTravels(): Observable<TravelDetailsDto[]>
  {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Upcoming`
    );
  }

  getTopCompanions(): Observable<CommonTravelCompanion[]>
  {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Companions`
    );
  }
}
