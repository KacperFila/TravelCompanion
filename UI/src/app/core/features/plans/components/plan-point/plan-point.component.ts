import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input, OnDestroy, OnInit,
  Output, ViewChild
} from '@angular/core';
import {
  TravelPoint, TravelPointUpdateRequest,
} from '../../models/plan.models';
import {PlansService} from '../../services/plans/plans.service';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {PlansSignalRService} from "../../services/plans/plans-signalR.service";

@Component({
  selector: 'app-plan-point',
  templateUrl: './plan-point.component.html',
  styleUrls: ['./plan-point.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class PlanPointComponent implements OnInit,AfterViewInit, OnDestroy {
  constructor(private plansService: PlansService, private plansSignalRService: PlansSignalRService) { }

  @ViewChild('circle') circleElement!: ElementRef;

  @Input() travelPoint: TravelPoint = { id: '', placeName: '', totalCost: 0, travelPlanOrderNumber: 0 };
  @Input() nodeNumber: number = 0;
  @Input() showLineBefore: boolean = false;
  @Input() showLineAfter: boolean = false;
  @Input() updateRequests: TravelPointUpdateRequest[] = [];

  @Output() pointDeletedEvent = new EventEmitter<TravelPoint>();
  @Output() openEditRequestsModalEvent = new EventEmitter<TravelPointUpdateRequest[]>();
  @Output() openEditPointModalEvent = new EventEmitter<TravelPoint>();

  ngOnInit() {
    this.plansService.getTravelPointEditRequests(this.travelPoint.id)
      .subscribe((response) => {
        this.updateRequests = response;
      })
  }

  ngAfterViewInit() {
    this.setCircleDimensions();
    window.addEventListener('resize', this.setCircleDimensions.bind(this));
  }

  ngOnDestroy() {
    window.removeEventListener('resize', this.setCircleDimensions.bind(this));
  }

  private setCircleDimensions() {
    const circle = this.circleElement.nativeElement;
    const width = circle.offsetWidth;
    circle.style.height = `${width}px`;
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
}
