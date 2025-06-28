import { Component } from '@angular/core';
import { NumberWidgetComponent } from "../../features/stats/components/number-widget/number-widget.component";
import { TravelsService } from "../../features/travels/services/plans/travels.service";
import { PlansService } from "../../features/plans/services/plans/plans.service";
import {TravelDetailsDto} from "../../features/travels/models/travel.models";
import {
  UpcomingTravelWidgetComponent
} from "../../features/stats/components/upcoming-travel-widget/upcoming-travel-widget/upcoming-travel-widget.component";

@Component({
  selector: 'app-stats-dashboard',
  standalone: true,
  templateUrl: './stats-dashboard.component.html',
  styleUrls: ['./stats-dashboard.component.css'],
  imports: [
    NumberWidgetComponent,
    UpcomingTravelWidgetComponent
  ]
})
export class StatsDashboardComponent {
  constructor(private travelsService: TravelsService, private plansService: PlansService) {
    this.travelsService.getTravelsCount().subscribe((response) => {
      this.travelsCount = response;
    });

    this.travelsService.getFinishedTravelsCount().subscribe((response) => {
      this.finishedTravelsCount = response;
    });

    this.plansService.getPlansCount().subscribe((response) => {
      this.plansCount = response;
    });


    this.travelsService.getUpcomingTravels().subscribe((response) => {
      this.upcomingTravels = response;
    });
  }

  travelsCount : number = 0;
  plansCount : number = 0;
  finishedTravelsCount : number = 0;
  upcomingTravels : TravelDetailsDto[] = [];
}
