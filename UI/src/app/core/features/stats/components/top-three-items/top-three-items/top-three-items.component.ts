import { Component, Input } from '@angular/core';
import { NgForOf, NgIf } from "@angular/common";

@Component({
  selector: 'app-top-three-items',
  templateUrl: './top-three-items.component.html',
  standalone: true,
  imports: [
    NgIf,
    NgForOf
  ],
  styleUrls: ['./top-three-items.component.css']
})
export class TopThreeItemsComponent {
  @Input() items: { label: string, itemCount: number }[] = [];
  @Input() componentLabel: string = '';
  @Input() itemUnit: string = '';
  @Input() itemUnitPlural: string = '';

  getRankIcon(index: number): string {
    return ['🥇', '🥈', '🥉'][index] || `${index + 1}.`;
  }
}
