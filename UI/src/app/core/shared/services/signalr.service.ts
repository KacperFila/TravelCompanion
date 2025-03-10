import { Injectable } from '@angular/core';
import { environment } from "../../../../environments/environment";
import * as signalR from '@microsoft/signalr';


@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  constructor() {}

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/travelPlanHub`, {
        withCredentials: true,
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('✅ SignalR Connected'))
      .catch((err) => console.error('❌ Error connecting to SignalR: ', err));
  }

  listenForUpdates(callback: (data: any) => void): void {
    this.hubConnection.on('ReceivePlanUpdate', (data) => {
      console.log('📌 Received Plan Update:', data);
      callback(data);
    });
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }
}
