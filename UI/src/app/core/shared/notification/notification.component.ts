import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import {NotificationMessage} from "../models/shared.models";
import {NotificationService} from "../services/notification.service";
import {NgClass, NgIf} from "@angular/common";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  imports: [
    NgClass,
    NgIf
  ],
  standalone: true
})
export class NotificationComponent implements OnInit, OnDestroy {
  notification: NotificationMessage | null = null;
  private notificationSubscription!: Subscription;

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.notificationSubscription = this.notificationService.notification$.subscribe(notification => {
      this.notification = notification;
    });
  }

  ngOnDestroy(): void {
    if (this.notificationSubscription) {
      this.notificationSubscription.unsubscribe();
    }
  }

  // Method to get severity class
  getSeverityClass(severity: string): string {
    switch (severity) {
      case 'Alert': return 'alert-class';
      case 'Error': return 'error-class';
      case 'Information': return 'info-class';
      default: return '';
    }
  }
}
