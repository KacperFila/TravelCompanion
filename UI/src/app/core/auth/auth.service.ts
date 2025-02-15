import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";

interface AuthResponse
{
    accessToken: string;
  refreshToken: string | null;
  expires: number;
  id: string;
  role: string;
  claims: {
    permissions: string[];
  };
}
const baseApiUrl = 'localhost:5000'

@Injectable({ providedIn: 'root' })
export class AuthService
{
    constructor(private http: HttpClient) {}

    signup(email: string, password: string)
    {
        return this.http.post<AuthResponse>(
            `${environment.apiBaseUrl}/users-module/account/sign-up`,
            {
                email: email,
                password: password,
                role: 'user',
                claims: {
                    "permissions": [
                    "users"
                    ]
                }
            }
        )
    }
}