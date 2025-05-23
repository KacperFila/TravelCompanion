import {
  Component
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TravelDetailsDto } from "../../models/travel.models";

@Component({
  selector: 'app-travel-roadmap',
  templateUrl: './travel-roadmap.component.html',
  styleUrls: ['./travel-roadmap.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
})
export class TravelRoadmapComponent {

  travel: TravelDetailsDto = {} as TravelDetailsDto;

}
