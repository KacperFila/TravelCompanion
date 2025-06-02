import {
  Component, OnDestroy, OnInit
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TravelDetailsDto } from "../../models/travel.models";
import {PlanPointComponent} from "../../../plans/components/plan-point/plan-point.component";
import {Subscription} from "rxjs";
import {TravelsSignalRService} from "../../services/plans/travels-signalR.service";
import {TravelPointComponent} from "../travel-point/travel-point.component";
import {TravelRatingComponent} from "../travel-rating/travel-rating/travel-rating.component";
import {TravelsService} from "../../services/plans/travels.service";

@Component({
  selector: 'app-travel-roadmap',
  templateUrl: './travel-roadmap.component.html',
  styleUrls: ['./travel-roadmap.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PlanPointComponent,
    TravelPointComponent,
    TravelRatingComponent
  ],
})
export class TravelRoadmapComponent implements OnInit, OnDestroy {
  constructor(private travelsSignalRService: TravelsSignalRService, private travelsService: TravelsService) {}

  travel: TravelDetailsDto = {} as TravelDetailsDto;
  private travelSubscription!: Subscription;

  ngOnInit(): void {
    this.travelSubscription = this.travelsSignalRService.travel$.subscribe(
      (travel) =>
      {
        console.log("Received travel: ", travel)
          this.onTravel(travel)
      }
    );
  }

  ngOnDestroy(): void {
    this.travelSubscription?.unsubscribe();
  }

  private onTravel(travel: TravelDetailsDto | null): void {
    if (travel) {
      console.log("Currently active travel: ", travel);
      this.travel = travel;
    }
  }

  protected onRated(rating: number) {
    this.travelsService.rateTravel(this.travel.id, rating).subscribe();
  }
}
