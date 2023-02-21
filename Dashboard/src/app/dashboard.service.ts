import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private dataUrl = 'https://localhost:7092/api/Distance/';

  constructor(private http: HttpClient) { }

  getDistanceTravelledByProvince(): Observable<ProvinceAPIResponse> {
    return this.http.get<ProvinceAPIResponse>(this.dataUrl + "by-province");
  }

  getDistanceTravelledBySite(provinceId: number): Observable<SiteAPIResponse> {
    return this.http.get<SiteAPIResponse>(this.dataUrl + "by-site/" + provinceId);
  }

  getDistanceTravelledByDriver(siteId: number): Observable<DriverAPIResponse> {
    return this.http.get<DriverAPIResponse>(this.dataUrl + "by-driver/"+ siteId);
  }
}

interface ProvinceAPIResponse {
  message: string;
  success: boolean;
  result : {
    provinceId: number;
    provinceName: string;
    totalDistanceTravelled: number;
  }[]
}

interface SiteAPIResponse {
  message: string;
  success: boolean;
  result : {
    siteId: number;
    siteName: string;
    totalDistanceTravelled: number;
  }[]
}

interface DriverAPIResponse {
  message: string;
  success: boolean;
  result : {
    driverId: number;
    driverName: string;
    totalDistanceTravelled: number;
  }[]
}
