import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit
} from '@angular/core';
import {
  TravelPoint, TravelPointUpdateRequest,
} from '../../models/plan.models';
import {PlansService} from '../../services/plans.service';
import {CommonModule} from '@angular/common';
import {ModalComponent} from '../../../../shared/modal/modal.component';
import {FormsModule} from '@angular/forms';
import {ItemListComponent} from "../../../../shared/item-list/item-list.components";
import {SignalRService} from "../../../../shared/services/signalr.service";

@Component({
  selector: 'app-travel-point',
  templateUrl: './travel-point.component.html',
  styleUrls: ['./travel-point.component.css'],
  standalone: true,
  imports: [CommonModule, ModalComponent, FormsModule, ItemListComponent],
})
export class TravelPointComponent implements OnInit{
  constructor(private plansService: PlansService, private signalRService: SignalRService) {
  }

  ngOnInit(): void {
    this.getTravelPointEditRequests(this.travelPoint.id)

    this.signalRService.startConnection();
    this.signalRService.listenForUpdates((travelPointUpdateRequests: TravelPointUpdateRequest[]) => {
    this.pointEditRequests = travelPointUpdateRequests;
    });
  }

  @Input() travelPoint: TravelPoint = {id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };
  @Input() nodeNumber: number = 0;
  @Output() pointDeletedEvent = new EventEmitter<TravelPoint>();

  editedTravelPoint: TravelPoint = {id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };
  pointEditRequests: TravelPointUpdateRequest[] = [];
  isPointDetailsModalOpen: boolean = false;
  isPointEditRequestsModalOpen: boolean = false;

  deletePoint(point: TravelPoint) {
    this.pointDeletedEvent.emit(point);
  }

  updatePoint() {
    this.travelPoint = { ...this.editedTravelPoint };
    this.plansService.updatePoint(this.travelPoint)
      .subscribe(
        {
          next: ()=> {}
        }
      );
  }

  getTravelPointEditRequests(travelPointId: string) {
    this.plansService.getTravelPointEditRequests(travelPointId)
      .subscribe(
        {
          next: (response) => {
            this.pointEditRequests = response;
            },
          error: (err) => {
            console.error("Error fetching data:", err);
          }
  })

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

  AcceptUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.acceptUpdateRequest(updateRequest.requestId.value)
      .subscribe(
        (response) => {
        console.log(response);
      },
        (error) => {console.log(error)});
  }

  RejectUpdateRequest(updateRequest: TravelPointUpdateRequest)
  {
    this.plansService.rejectUpdateRequest(updateRequest.requestId.value)
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {console.log(error)});
  }
}
