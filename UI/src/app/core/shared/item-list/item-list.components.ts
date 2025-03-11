import {Component, Input, Output, EventEmitter, TemplateRef} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css'],
})
export class ItemListComponent<T> {
  @Input() items: T[] = [];
  @Input() displayProperty: keyof T | null = null;
  @Input() buttonTemplate!: TemplateRef<any>;

  @Output() itemClick = new EventEmitter<T>();
  @Output() deleteItem = new EventEmitter<T>();

  onItemClick(item: T) {
    this.itemClick.emit(item);
  }
}
