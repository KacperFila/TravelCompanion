import { Injectable } from '@angular/core';
import { environment } from '../../../../../../environments/environment';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import {Receipt, TravelDetailsDto} from "../../models/travel.models";

@Injectable({ providedIn: 'root' })
export class TravelsService {
  constructor(private http: HttpClient) {}

  getActiveTravel(): Observable<TravelDetailsDto> {
    return this.http.get<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/Active`
    );
  }

  rateTravel(travelId: string, rating: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/${travelId}/Rating`,
      rating
    );
  }

  getReceiptsForPoint(travelPointId: string): Observable<Receipt[]> {
    return this.http.put<any>(
      `${environment.apiBaseUrl}/travels-module/Travel/${travelId}/Rating`,
      rating
    );
  }
}
