import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PointsRoadmapComponent } from '../../features/plans/components/points-roadmap/points-roadmap.component';

@Component({
  selector: 'app-plans-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PointsRoadmapComponent
  ],
  templateUrl: './plans-dashboard.component.html',
  styleUrls: ['./plans-dashboard.component.css'],
})
export class PlansDashboardComponent {
  constructor() {}
}
