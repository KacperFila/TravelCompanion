export interface NotificationMessage {
  id: string,
  title: string,
  message: string,
  sentFrom: string | null,
  sentAt: string,
  severity: NotificationSeverity
}

export enum NotificationSeverity {
  Alert = 1,
  Error = 2,
  Information = 3
}

export interface APIError {
  code: string;
  message: string;
}

export interface UserInfoDto
{
  userId: string,
  userName: string,
  email: string,
  activePlanId: string
}

