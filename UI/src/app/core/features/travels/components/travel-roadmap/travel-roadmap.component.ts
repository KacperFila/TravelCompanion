import {
  Component, OnDestroy, OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TravelDetailsDto } from "../../models/travel.models";
import { Subscription } from "rxjs";
import { TravelsSignalRService } from "../../services/plans/travels-signalR.service";
import { TravelPointComponent } from "../travel-point/travel-point.component";
import { TravelRatingComponent } from "../travel-rating/travel-rating/travel-rating.component";
import { TravelsService } from "../../services/plans/travels.service";
import {
  ChangeActivePlanModal
} from "../../../plans/components/change-active-plan-modal/change-active-plan-modal.component";
import {ChangeActiveTravelModal} from "../change-active-travel-modal/change-active-travel-modal.component";

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
    ChangeActivePlanModal,
    ChangeActiveTravelModal,
  ],
})
export class TravelRoadmapComponent implements OnInit, OnDestroy {
  constructor(private travelsSignalRService: TravelsSignalRService, private travelsService: TravelsService) {}

  travel: TravelDetailsDto = {} as TravelDetailsDto;
  private travelSubscription!: Subscription;
  protected isChangeActiveTravelModalOpen: boolean = false;

  ngOnInit(): void {
    this.travelSubscription = this.travelsSignalRService.travel$.subscribe(
      (travel) =>
      {
          this.onTravel(travel)
      }
    );
  }

  ngOnDestroy(): void {
    this.travelSubscription?.unsubscribe();
  }

  private onTravel(travel: TravelDetailsDto | null): void {
    if (travel) {
      this.travel = travel;
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
