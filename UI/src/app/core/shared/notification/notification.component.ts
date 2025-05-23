import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { NotificationMessage } from "../models/shared.models";
import { NotificationService } from "../services/notification.service";
import {DatePipe, NgClass, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
  imports: [
    NgClass,
    DatePipe,
    NgForOf,
    NgIf
  ],
  standalone: true,
})
export class NotificationComponent implements OnInit, OnDestroy {
  notifications: (NotificationMessage & { leaving?: boolean })[] = [];
  private notificationSubscription!: Subscription;

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationSubscription = this.notificationService.notification$
      .subscribe(notification => {
        if (notification) {
          const newNotification = { ...notification };
          this.notifications.push(newNotification);

          setTimeout(() => this.startRemovingNotification(newNotification), 3000);
        }
      });
  }

  ngOnDestroy(): void {
    if (this.notificationSubscription) {
      this.notificationSubscription.unsubscribe();
    }
  }

  startRemovingNotification(notification: NotificationMessage & { leaving?: boolean }): void {
    notification.leaving = true;

    setTimeout(() => {
      this.notifications = this.notifications.filter(n => n !== notification);
    }, 300); // match CSS animation duration
  }

  getSeverityClass(severity: number): string {
    switch (severity) {
      case 1: return 'error';
      case 2: return 'alert';
      case 3: return 'information';
      default: return '';
    }
  }

  getSeverityIcon(severity: number): string {
    switch (severity) {
      case 1: return 'error';
      case 2: return 'warning';
      case 3: return 'info';
      default: return 'notifications';
    }
  }
}
