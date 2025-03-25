import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {AuthService} from "../../../auth/auth.service";
import {User} from "../../../auth/user.model";


@Injectable({
  providedIn: 'root',
})
export class PlansSignalRService {
  private hubConnection!: signalR.HubConnection;

  private currentUser: User | null = null;

  constructor(private authService: AuthService) {
    this.authService.user.subscribe((user) => {
      this.currentUser = user;
    });
  }

  startConnection(): void {
    if (this.hubConnection) return;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/travelPlanHub`, {
        withCredentials: true,
        accessTokenFactory: () => this.currentUser?.token ?? ''
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('✅ SignalR Connected: ' + this.hubConnection.connectionId))
      .catch((err) => console.error('❌ Error connecting to SignalR: ', err));
  }

  listenForUpdates(eventName: string, callback: (data: any) => void): void {
    console.log('📌 Started listening to :', eventName);

    this.hubConnection.on(eventName, (data) => {
      console.log('📌 Received data: ', data);
      callback(data);
    });
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
