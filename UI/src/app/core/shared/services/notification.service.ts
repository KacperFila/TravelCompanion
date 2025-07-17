import { Injectable } from "@angular/core";
import { NotificationMessage } from "../models/shared.models";
import { BehaviorSubject } from "rxjs";
import { User } from "../../auth/user.model";
import { AuthService } from "../../auth/auth.service";
import * as signalR from '@microsoft/signalr';
import {environment} from "../../../../environments/environment.prod";

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection!: signalR.HubConnection;
  private currentUser: User | null = null;

  private notificationSubject = new BehaviorSubject<NotificationMessage | null>(null);
  notification$ = this.notificationSubject.asObservable();

  private recentNotificationIds = new Set<string>();

  constructor(private authService: AuthService) {
    this.authService.user.subscribe((user) => {
      this.currentUser = user;
      if (user) {
        this.startConnection();
        this.setupListeners();
      }
      else {
        this.stopConnection();
      }
    });
  }

  startConnection(): void {
    if (this.hubConnection) {
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiBaseUrl}/notifications`, {
        withCredentials: true,
        accessTokenFactory: () => this.currentUser?.token ?? ''
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('✅ SignalR Connected to notifications: ' + this.hubConnection.connectionId);
      })
      .catch(err => {
        console.error('❌ SignalR Connection Error: ', err);
      });
  }

  private setupListeners(): void {
    this.hubConnection.on("ReceiveNotification", (notification: NotificationMessage) => {
      this.showNotification(notification);
    });
  }

  private showNotification(notification: NotificationMessage): void {
    const key = notification.id;

    if (this.recentNotificationIds.has(key)) {
      return;
    }

    this.recentNotificationIds.add(key);
    this.notificationSubject.next(notification);

    if (this.recentNotificationIds.size > 50) {
      const first = this.recentNotificationIds.values().next().value;
      if (first != null)
      {
        this.recentNotificationIds.delete(first);
      }
    }
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
