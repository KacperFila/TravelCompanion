import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from "rxjs";
import {environment} from "../../../../../../environments/environment";
import {UserInfoDto} from "../../models/user.models";

@Injectable({ providedIn: 'root' })
export class UsersService {
  constructor(private http: HttpClient) {}

  browseUsers(): Observable<UserInfoDto[]> {
    return this.http.get<UserInfoDto[]>(
      `${environment.apiBaseUrl}/users-module/Users/browse`
    );
  }
}
