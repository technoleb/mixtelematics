import { Component, OnInit  } from '@angular/core';
import * as Highcharts from 'highcharts';
import { DashboardService } from './dashboard.service';

@Component({
  selector: 'app-root',
   templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions!: Highcharts.Options ;
  currentChart = 1;
  provianceID = 0;
  siteID = 0;
  backbutton = false;
  chartData: Highcharts.SeriesOptionsType[] = [];

  constructor(private dashboardService: DashboardService) { }
  
  ngOnInit(): void {
    this.GetProvinceData()
  }

  GetProvinceData() {
    this.currentChart = 1;
    this.dashboardService.getDistanceTravelledByProvince().subscribe(data => {
      if (data.success) {
        this.chartOptions = {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Distance Travelled By Province'
          },
          xAxis: {
            categories: data.result.map(d => d.provinceId.toString() + " - " + d.provinceName),
          },
          yAxis: {
            title: {
              text: 'Distance Travelled'
            }
          },
          series: [
            {
              type: 'column',
              name: 'Provinces',
              data: data.result.map(d => d.totalDistanceTravelled),
              events: { click: (event) => this.onChartClick(event) },
            }
          ]
        };
      }
      else
      {
        alert(data.message);
      }
    });
  }

  GetSitesData() {
    this.currentChart = 2;

    this.dashboardService.getDistanceTravelledBySite(this.provianceID).subscribe(data => {
      if (data.success) {
        this.chartOptions = {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Distance Travelled By Site'
          },
          xAxis: {
            categories: data.result.map(d => d.siteId.toString() + " - " + d.siteName),
          },
          yAxis: {
            title: {
              text: 'Distance Travelled'
            }
          },
          series: [
            {
              type: 'column',
              name: 'Sites',
              data: data.result.map(d => d.totalDistanceTravelled),
              events: { click: (event) => this.onChartClick(event) },
            }
          ]
        };
      }
      else
      {
        alert(data.message);
      }
    });
  }

  GetDriverData() {
    this.currentChart = 3;
    this.dashboardService.getDistanceTravelledByDriver(this.siteID).subscribe(data => {
      if (data.success) {
        this.chartOptions = {
          chart: {
            type: 'column'
          },
          title: {
            text: 'Distance Travelled By Driver'
          },
          xAxis: {
            categories: data.result.map(d => d.driverId.toString() + " - " + d.driverName),
          },
          yAxis: {
            title: {
              text: 'Distance Travelled'
            }
          },
          series: [
            {
              type: 'column',
              name: 'Drivers',
              data: data.result.map(d => d.totalDistanceTravelled),
            }
          ]
        };
      }
      else
      {
        alert(data.message);
      }
    });
  }

  backClick() {
    if(this.currentChart == 3)
    {
      this.GetSitesData();
    }
    else if(this.currentChart == 2)
    {
      this.GetProvinceData();
    }
  }

  onChartClick(event: Highcharts.SeriesClickEventObject){
    var category:string = event.point.category.toString(); 
    var series:string = event.point.series.name.toString();
    
    console.log( category.split(' - ',2)[0]);

    if(series.toLowerCase() == "sites")
    {
      this.siteID = +category.split(' - ',2)[0];
      this.GetDriverData();
    }
    else if(series.toLowerCase() == "provinces")
    {
      this.provianceID = +category.split(' - ',2)[0];
      this.GetSitesData();
    }
  }
}
