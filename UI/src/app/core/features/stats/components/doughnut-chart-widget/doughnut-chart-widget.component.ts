import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {
  Chart,
  DoughnutController,
  ArcElement,
  Tooltip,
  Legend,
  ChartData,
  ChartDataset,
  ChartOptions
} from 'chart.js';
import {BaseChartDirective} from "ng2-charts";
import { DoughnutChartDataModel } from "../../models/stats.models";

@Component({
  selector: 'app-doughnut-chart-widget',
  templateUrl: './doughnut-chart-widget.component.html',
  styleUrls: ['./doughnut-chart-widget.component.css'],
  imports: [
    BaseChartDirective
  ],
  standalone: true
})
export class DoughnutChartWidgetComponent implements OnInit, OnChanges {
  @Input() data: { [label: string]: number } = {};
  @Input() title: string = '';

  ngOnInit() {
    this.initializeChart();
  }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data'] && this.data) {
      this.updateChartData(this.data);
    }
  }

  initializeChart()
  {
    Chart.register(DoughnutController, ArcElement, Tooltip, Legend);
  }

  doughnutChartType: 'doughnut' = 'doughnut';

  doughnutChartData: ChartData<'doughnut', number[], string> = {
    labels: [],
    datasets: []
  };

  chartOptions: ChartOptions<'doughnut'> = {
    responsive: true,
    plugins: {
      legend: { position: 'bottom' },
      tooltip: { enabled: true }
    }
  };

  private updateChartData(data: DoughnutChartDataModel): void {
    const labels = Object.keys(data);
    const values = Object.values(data);
    this.doughnutChartData = {
      labels,
      datasets: [
        {
          data: values,
          backgroundColor: this.generateColors(labels.length),
          hoverBackgroundColor: this.generateColors(labels.length),
        } as ChartDataset<'doughnut', number[]>
      ]
    };
  }

  private generateColors(count: number): string[] {
    const colors = [
      '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF',
      '#FF9F40', '#C9CBCF', '#FF6384', '#36A2EB', '#FFCE56'
    ];
    return Array(count).fill(null).map((_, i) => colors[i % colors.length]);
  }
}
