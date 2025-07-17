import {
  AfterViewInit,
  Component,
  ElementRef, EventEmitter,
  Input, OnDestroy, OnInit, Output,
  ViewChild
} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {TravelPointDto} from "../../models/travel.models";
import {TravelsService} from "../../services/travels/travels.service";

@Component({
  selector: 'app-travel-point',
  templateUrl: './travel-point.component.html',
  styleUrls: ['./travel-point.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class TravelPointComponent implements AfterViewInit, OnDestroy {

  constructor(private travelsService: TravelsService) {
  }

  @ViewChild('circle') circleElement!: ElementRef;

  @Input() travelPoint: TravelPointDto = {} as TravelPointDto;
  @Input() nodeNumber: number = 0;
  @Input() showLineBefore: boolean = false;
  @Input() showLineAfter: boolean = false;
  @Input() isActive: boolean = false;

  @Output() openReceiptsModalEvent = new EventEmitter<{ id: string, title: string }>();

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

  protected markPointAsVisited()
  {
    this.travelsService.markPointAsVisited(this.travelPoint.id).subscribe();
    this.travelPoint.isVisited = true;
  }

  protected markPointAsUnvisited()
  {
    this.travelsService.markPointAsUnvisited(this.travelPoint.id).subscribe();
    this.travelPoint.isVisited = false;
  }
}
