import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from "rxjs";
import {UserInfoDto} from "../models/shared.models";
import {environment} from "../../../../environments/environment.prod";

@Injectable({ providedIn: 'root' })
export class UsersService {
  constructor(private http: HttpClient) {}

  browseUsers(): Observable<UserInfoDto[]> {
    return this.http.get<UserInfoDto[]>(
      `${environment.apiBaseUrl}/users-module/Users/browse`
    );
  }
}
