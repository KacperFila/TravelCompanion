import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import {
  CreateTravelPointRequest,
  TravelPoint,
} from '../../models/plan.models';
import { PlansService } from '../../services/plans.service';
import { AuthService } from '../../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ModalComponent } from '../../../../shared/modal/modal.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-travel-point',
  templateUrl: './travel-point.component.html',
  styleUrls: ['./travel-point.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule],
})
export class TravelPointComponent {
  @Input() travelPoint: TravelPoint = {id: '', placeName: '', totalCost: 0};
  @Input() nodeNumber: number = 0;

  @Output() pointDeletedEvent = new EventEmitter<TravelPoint>();

  isPointDetailsModalOpen: boolean = false;

  deletePoint(point: TravelPoint) {
    this.pointDeletedEvent.emit(point);
  }

  openPointDetailsModal() {
    this.isPointDetailsModalOpen = true;
  }

  closePointDetailsModal() {
    this.isPointDetailsModalOpen = false;
  }
}
