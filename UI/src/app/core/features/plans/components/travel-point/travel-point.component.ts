import {
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';
import {
  TravelPoint, TravelPointUpdateRequest,
} from '../../models/plan.models';
import {PlansService} from '../../services/plans/plans.service';
import {CommonModule} from '@angular/common';
import {ModalComponent} from '../../../../shared/modal/modal.component';
import {FormsModule} from '@angular/forms';
import {ItemListComponent} from "../../../../shared/item-list/item-list.components";

@Component({
  selector: 'app-travel-point',
  templateUrl: './travel-point.component.html',
  styleUrls: ['./travel-point.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule, ItemListComponent],
})
export class TravelPointComponent {
  constructor(private plansService: PlansService) { }

  @Input() travelPoint: TravelPoint = { id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };
  @Input() updateRequests: TravelPointUpdateRequest[] = [];
  @Input() nodeNumber: number = 0;

  @Output() pointDeletedEvent = new EventEmitter<TravelPoint>();
  @Output() openEditRequestsModalEvent = new EventEmitter<TravelPointUpdateRequest[]>();
  @Output() openEditPointModalEvent = new EventEmitter<TravelPoint>();

  editedTravelPoint: TravelPoint = { id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };
  isPointDetailsModalOpen: boolean = false;
  isPointEditRequestsModalOpen: boolean = false;

  updatePoint() {
    this.travelPoint = { ...this.editedTravelPoint };
    this.plansService.updatePoint(this.travelPoint)
      .subscribe(
        (error) => {console.log(error)}
      );
  }

  triggerEditRequestsModal() {
    this.openEditRequestsModalEvent.emit(this.updateRequests);
  }

  triggerEditPointModal() {
    this.openEditPointModalEvent.emit(this.travelPoint);
  }

  deletePoint(point: TravelPoint) {
    this.pointDeletedEvent.emit(point);
  }

  acceptUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.acceptUpdateRequest(updateRequest.requestId.valueOf())
      .subscribe(
        (error) => {console.log(error)}
      );
  }

  rejectUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.rejectUpdateRequest(updateRequest.requestId.valueOf())
      .subscribe(
        (error) => {console.log(error)}
      );
  }

  openPointDetailsModal() {
    this.editedTravelPoint = { ...this.travelPoint };
    this.isPointDetailsModalOpen = true;
  }
  closePointDetailsModal() {
    this.isPointDetailsModalOpen = false;
  }
  openPointEditRequestsModal() {
    this.isPointEditRequestsModalOpen = true;
  }
  closePointEditRequestsModal() {
    this.isPointEditRequestsModalOpen = false;
  }
}
