import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit, OnChanges,
  SimpleChanges
} from '@angular/core';
import {
  TravelPoint, TravelPointUpdateRequest,
} from '../../models/plan.models';
import {PlansService} from '../../services/plans.service';
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
export class TravelPointComponent implements OnChanges {
  constructor(private plansService: PlansService) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('Current state of travel point: ', JSON.stringify(this.travelPoint));

    this.getTravelPointEditRequests(this.travelPoint.id)
  }

  @Input() travelPoint: TravelPoint = {id: '', placeName: '', totalCost: 0};
  @Input() nodeNumber: number = 0;

  @Output() pointDeletedEvent = new EventEmitter<TravelPoint>();

  isPointDetailsModalOpen: boolean = false;
  isPointEditRequestsModalOpen: boolean = false;
  pointEditRequests: TravelPointUpdateRequest[] = [];

  deletePoint(point: TravelPoint) {
    this.pointDeletedEvent.emit(point);
  }

  updatePoint(travelPoint: TravelPoint) {
    console.log("travelPoint", travelPoint);
    this.plansService.updatePoint(travelPoint)
      .subscribe(
        {
          next: () => console.log("TRIGGERED")
        }
      );
  }

  getTravelPointEditRequests(travelPointId: string) {
    this.plansService.getTravelPointEditRequests(travelPointId)
      .subscribe(
        {
          next: (response) => {
            this.pointEditRequests = response;
            console.log(`getTravelPointEditRequests for TravelPointId: ${travelPointId}`, response);
            },
          error: (err) => {
            console.error("Error fetching data:", err);
          }
  })

}
  openPointDetailsModal() {
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
