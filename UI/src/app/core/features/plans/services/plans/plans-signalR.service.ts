import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from "../../../../auth/auth.service";
import { PlansService } from "./plans.service";
import { User } from "../../../../auth/user.model";
import { PlanInvitationResponse } from "./plans-signalR-responses.models";
import {TravelPlan} from "../../models/plan.models";

@Injectable({
  providedIn: 'root',
})
export class PlansSignalRService {
  private hubConnection!: signalR.HubConnection;
  private currentUser: User | null = null;

  private invitationsSubject = new BehaviorSubject<PlanInvitationResponse[]>([]);
  invitations$ = this.invitationsSubject.asObservable();
  private travelPlanSubject = new BehaviorSubject<TravelPlan | null>(null);
  travelPlan$ = this.travelPlanSubject.asObservable();

  constructor(private authService: AuthService, private plansService: PlansService) {
    this.authService.user.subscribe(
      (user) => {
      this.currentUser = user;
      if (user) {
        this.startConnection();
        this.setupListeners();

        this.initialFetchInvitations();
        this.initialFetchTravelPlan();
      }
      else {
        this.stopConnection();
      }
    });
  }

  startConnection(): void {
    if (this.hubConnection){
      return;
    }

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
      .then(() => {
        console.log('✅ SignalR Connected: ' + this.hubConnection.connectionId);
      })
      .catch(err => {
        console.error('❌ SignalR Connection Error: ', err);
      });
  }

  private setupListeners(): void {
    this.hubConnection.on("ReceivePlanInvitation", (newInvitation: PlanInvitationResponse) => {
      const currentInvitations = this.invitationsSubject.value;
      this.invitationsSubject.next([...currentInvitations, newInvitation]);
    });

    this.hubConnection.on("ReceiveActivePlanChanged", (activePlanId: string) => {
      if (this.currentUser) {
        const updatedUser = new User(
          this.currentUser.email,
          this.currentUser.id,
          this.currentUser.role,
          activePlanId,
          this.currentUser.token!,
          this.currentUser['_claims'],
          this.currentUser['expirationDate']
        );

        localStorage.setItem('user', JSON.stringify(updatedUser));
        this.authService.user.next(updatedUser);
      }
    });

    this.hubConnection.on("ReceivePlanUpdate", (updatedPlan: TravelPlan) => {
      if(updatedPlan.id === this.currentUser?.activePlanId)
      {
        this.travelPlanSubject.next(updatedPlan);
      }
    });
  }

  initialFetchInvitations(): void {
    this.plansService.getInvitationsForUser()
      .subscribe((invitations) => {
       this.invitationsSubject.next(invitations);
    });
  }

  initialFetchTravelPlan(): void {
  this.plansService.getActivePlanWithPoints()
    .subscribe((activePlan) => {
      this.travelPlanSubject.next(activePlan);
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
