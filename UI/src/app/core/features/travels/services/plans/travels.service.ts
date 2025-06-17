import { Injectable } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TravelDetailsDto} from "../../models/travel.models";

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
}
