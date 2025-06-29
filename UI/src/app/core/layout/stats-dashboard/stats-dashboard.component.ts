import { Component } from '@angular/core';
import { NumberWidgetComponent } from "../../features/stats/components/number-widget/number-widget.component";
import { TravelsService } from "../../features/travels/services/plans/travels.service";
import { PlansService } from "../../features/plans/services/plans/plans.service";
import { TravelDetailsDto } from "../../features/travels/models/travel.models";
import {
  UpcomingTravelWidgetComponent
} from "../../features/stats/components/upcoming-travel-widget/upcoming-travel-widget/upcoming-travel-widget.component";
import { CommonTravelCompanion } from "../../features/stats/models/stats.models";
import {
  TopThreeItemsComponent
} from "../../features/stats/components/top-three-items-component/top-three-items/top-three-items.component";

@Component({
  selector: 'app-stats-dashboard',
  standalone: true,
  templateUrl: './stats-dashboard.component.html',
  styleUrls: ['./stats-dashboard.component.css'],
  imports: [
    NumberWidgetComponent,
    UpcomingTravelWidgetComponent,
    TopThreeItemsComponent
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

    this.travelsService.getTopCompanions().subscribe((response) => {
      this.topCompanions = response;
    });
  }

  travelsCount : number = 0;
  plansCount : number = 0;
  finishedTravelsCount : number = 0;
  upcomingTravels : TravelDetailsDto[] = [];
  topCompanions : CommonTravelCompanion[] = [];

  protected getTopCompanions(): { label: string; itemCount: number }[] {
    return this.topCompanions.map(c => ({ label: c.email, itemCount: c.travelsCount}))
  }
}
