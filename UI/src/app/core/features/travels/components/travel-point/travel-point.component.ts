import {
  AfterViewInit,
  Component,
  ElementRef,
  Input, OnDestroy, OnInit,
  ViewChild
} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {TravelPointDto} from "../../models/travel.models";

@Component({
  selector: 'app-travel-point',
  templateUrl: './travel-point.component.html',
  styleUrls: ['./travel-point.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class TravelPointComponent implements OnInit,AfterViewInit, OnDestroy {

  @ViewChild('circle') circleElement!: ElementRef;

  @Input() travelPoint: TravelPointDto = { id: '', placeName: '', totalCost: 0, travelOrderNumber: 0 }
  @Input() nodeNumber: number = 0;
  @Input() showLineBefore: boolean = false;
  @Input() showLineAfter: boolean = false;

  ngOnInit() {
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
}
