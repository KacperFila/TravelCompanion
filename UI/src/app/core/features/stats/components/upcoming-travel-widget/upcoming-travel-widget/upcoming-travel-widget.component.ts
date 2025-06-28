import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TravelDetailsDto} from "../../../../travels/models/travel.models";

@Component({
  selector: 'app-upcoming-travel-widget',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upcoming-travel-widget.component.html',
  styleUrl: './upcoming-travel-widget.component.css'
})
export class UpcomingTravelWidgetComponent {
  @Input() travels: TravelDetailsDto[] = [];
}
