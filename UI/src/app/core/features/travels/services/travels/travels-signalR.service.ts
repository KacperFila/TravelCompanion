import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
import { AuthService } from "../../../../auth/auth.service";
import { User } from "../../../../auth/user.model";
import {TravelDetailsDto} from "../../models/travel.models";
import {TravelsService} from "./travels.service";
import {environment} from "../../../../../../environments/environment.prod";

@Injectable({
  providedIn: 'root',
})
export class TravelsSignalRService {
  private hubConnection!: signalR.HubConnection;
  private currentUser: User | null = null;

  private travelSubject = new BehaviorSubject<TravelDetailsDto | null>(null);
  travel$ = this.travelSubject.asObservable();

  private activeTravelChangedEvent = new Subject<void>();
  activeTravelChanged$ = this.activeTravelChangedEvent.asObservable();

  constructor(private authService: AuthService, private travelsService: TravelsService) {
    this.authService.user.subscribe((user) => {
      this.currentUser = user;
      if (user) {
        this.startConnection();
        this.setupListeners();

        this.initialFetchTravel();
      } else {
        this.stopConnection();
      }
    });
  }

  startConnection = async (): Promise<void> => {
    if (this.hubConnection) {
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiBaseUrl}/travelHub`, {
        withCredentials: true,
        accessTokenFactory: () => this.currentUser?.token ?? ''
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('✅ SignalR Connected (travels): ' + this.hubConnection.connectionId);
      })
      .catch(err => {
        console.error('❌ SignalR Connection Error: ', err);
      });
  }

  private setupListeners(): void {
    this.hubConnection.on("ReceiveActiveTravelChanged", (activeTravelId: string) => {
      if (this.currentUser) {
        const updatedUser = new User(
          this.currentUser.email,
          this.currentUser.id,
          this.currentUser.role,
          this.currentUser.activePlanId,
          activeTravelId,
          this.currentUser.token!,
          this.currentUser['_claims'],
          this.currentUser['expirationDate']
        );

        localStorage.setItem('user', JSON.stringify(updatedUser));
        this.authService.user.next(updatedUser);

        this.activeTravelChangedEvent.next();
      }
    });

    this.hubConnection.on("ReceiveTravelUpdate", (updatedTravel: TravelDetailsDto) => {
      if (updatedTravel.id === this.currentUser?.activeTravelId) {
        this.travelSubject.next(updatedTravel);
      }
    });
  }

  initialFetchTravel(): void {
    this.travelsService.getActiveTravel()
      .subscribe((activeTravel) => {
        this.travelSubject.next(activeTravel);
      });
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().then(() => {
        console.log('❌ SignalR Disconnected');
        this.hubConnection = undefined!;
      });
    }
  }
}
