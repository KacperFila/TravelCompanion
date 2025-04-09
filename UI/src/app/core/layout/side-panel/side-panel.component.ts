import {Component, EventEmitter, Output} from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.css'],
  imports: [
    RouterLink,
    RouterLinkActive,
    NgIf
  ],
  standalone: true
})
export class SidePanelComponent {
  isSidebarExpanded = false;
  @Output() toggle = new EventEmitter<boolean>();

  togglePanel() {
    this.isSidebarExpanded = !this.isSidebarExpanded;
    this.toggle.emit(this.isSidebarExpanded);
  }
}
