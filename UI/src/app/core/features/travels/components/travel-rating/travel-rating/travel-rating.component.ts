import {Component, EventEmitter, Input, OnChanges, OnInit, Output} from '@angular/core';
import {NgClass, NgForOf} from "@angular/common";

type StarType = 'full' | 'half' | 'empty';

@Component({
  selector: 'app-travel-rating',
  standalone: true,
  imports: [
    NgForOf,
    NgClass
  ],
  templateUrl: './travel-rating.component.html',
  styleUrl: './travel-rating.component.css'
})
export class TravelRatingComponent implements OnInit, OnChanges {
  @Input() rating: number = 0;
  @Input() max: number = 5;
  @Input() readonly: boolean = false;
  @Output() rated = new EventEmitter<number>();

  hoverIndex: number = 0;
  stars: StarType[] = [];
  private rounded : number  = Math.round(this.rating * 2) / 2;

  ngOnInit() {
    this.generateStars();
  }

  ngOnChanges() {
    this.generateStars();
  }

  generateStars() {
    this.rounded = Math.round(this.rating * 2) / 2;
    this.stars = [];

    for (let i = 1; i <= this.max; i++) {
      if (this.rounded >= i) {
        this.stars.push('full');
      } else if (this.rounded >= i - 0.5) {
        this.stars.push('half');
      } else {
        this.stars.push('empty');
      }
    }
  }


  rate(index: number) {
    if (!this.readonly) {
      this.rating = index + 1;
      this.generateStars();
      this.rated.emit(this.rating);
    }
  }

  isFull(index: number): boolean {
    return index + 1 <= this.rounded;
  }

  isHalf(index: number): boolean {
    return index + 0.5 === this.rounded;
  }

  isEmpty(index: number): boolean {
    return !this.isFull(index) && !this.isHalf(index);
  }

  setHover(index: number) {
    this.hoverIndex = index;
  }

  clearHover() {
    this.hoverIndex = 0;
  }
}
