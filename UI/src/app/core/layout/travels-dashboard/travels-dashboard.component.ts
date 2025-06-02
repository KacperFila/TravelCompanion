import { Component } from '@angular/core';
import {TravelRoadmapComponent} from "../../features/travels/components/travel-roadmap/travel-roadmap.component";

@Component({
  selector: 'app-travels-dashboard',
  standalone: true,
  templateUrl: './travels-dashboard.component.html',
  styleUrls: ['./travels-dashboard.component.css'],
  imports: [
    TravelRoadmapComponent
  ]
})
export class TravelsDashboardComponent {}
