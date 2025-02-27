import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css'],
})
export class ItemListComponent<T> {
  @Input() items: T[] = []; // Generic list of items
  @Input() displayProperty: keyof T | null = null; // The key to display in the list

  @Output() itemClick = new EventEmitter<T>(); // Emits when an item is clicked

  onItemClick(item: T) {
    this.itemClick.emit(item);
  }
}
