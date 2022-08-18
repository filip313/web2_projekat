import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class GeocodingService {

  constructor(private http: HttpClient) { }

  getCoords(adresa:string):Observable<any>{

    return this.http.get<any>('https://api.geoapify.com/v1/geocode/search', {params: {
      text: adresa,
      format: 'json',
      apiKey: environment.geocodingKey
    }});
  }
}
