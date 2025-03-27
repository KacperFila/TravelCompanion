import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  standalone: true,
  styleUrls: ['./modal.component.css'],
  imports: [CommonModule],
})
export class ModalComponent {
  @Input() title: string = 'Modal Title';
  @Input() isModalOpen: boolean = false;
  @Output() close = new EventEmitter<void>();

  closeModal(): void {
    this.close.emit();
  }
}
