import {
  Component, OnDestroy, OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TravelDetailsDto } from "../../models/travel.models";
import { Subscription } from "rxjs";
import { TravelsSignalRService } from "../../services/travels/travels-signalR.service";
import { TravelPointComponent } from "../travel-point/travel-point.component";
import { TravelRatingComponent } from "../travel-rating/travel-rating/travel-rating.component";
import { TravelsService } from "../../services/travels/travels.service";
import {ChangeActiveTravelModal} from "../change-active-travel-modal/change-active-travel-modal.component";
import {AuthService} from "../../../../auth/auth.service";
import {User} from "../../../../auth/user.model";

@Component({
  selector: 'app-travel-roadmap',
  templateUrl: './travel-roadmap.component.html',
  styleUrls: ['./travel-roadmap.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TravelPointComponent,
    TravelRatingComponent,
    ChangeActiveTravelModal
  ],
})
export class TravelRoadmapComponent implements OnInit, OnDestroy {
  constructor(private travelsSignalRService: TravelsSignalRService,
              private travelsService: TravelsService,
              private authService: AuthService) {}

  travel: TravelDetailsDto = {} as TravelDetailsDto;
  loading = true;
  currentUser: User | null = null;
  private travelSubscription!: Subscription;
  protected isChangeActiveTravelModalOpen: boolean = false;

  ngOnInit(): void {
    this.travelSubscription = this.travelsSignalRService.travel$.subscribe(
      (travel) =>
      {
          this.onTravel(travel)
      }
    );

    this.authService.user.subscribe((user) =>
    {
      this.currentUser = user;
    })
  }

  ngOnDestroy(): void {
    this.travelSubscription?.unsubscribe();
  }

  private onTravel(travel: TravelDetailsDto | null): void {
    if (travel) {
      this.travel = travel;
      this.loading = false
    }
  }

  protected onRated(rating: number) {
    this.travelsService.rateTravel(this.travel.id, rating).subscribe();
  }

  protected completeTravel() {
    this.travelsService.completeTravel(this.travel.id).subscribe();
  }

  protected changeActiveTravel() {
    this.isChangeActiveTravelModalOpen = true;
  }

  protected closeChangeActiveModal() {
    this.isChangeActiveTravelModalOpen = false;
  }
}
