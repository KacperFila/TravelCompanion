import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-number-widget',
  standalone: true,
  imports: [],
  templateUrl: './number-widget.component.html',
  styleUrl: './number-widget.component.css'
})
export class NumberWidgetComponent {
  @Input() title: string = '';
  @Input() number: number = 0;
}
