export interface NotificationMessage{
  title: string,
  message: string,
  sentFrom: string | null,
  sentAt: string,
  severity: NotificationSeverity
}

export enum NotificationSeverity {
  Alert = 'Alert',
  Error = 'Error',
  Information = 'Information',
}

export interface APIError {
  code: string;
  message: string;
}
