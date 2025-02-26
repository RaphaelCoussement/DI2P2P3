import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environments'
import { Application } from './models/application';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  private apiUrl = `${environment.apiBaseUrl}/application`;

  constructor(private http: HttpClient) { }

  private getHeaders() {
    return new HttpHeaders({
      'x-api-key': environment.apiKey
    });
  }

  getApps(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  getAppById(id: number) {
    return this.http.get<Application>(`${this.apiUrl}/${id}`);
  }
  

  addApp(app: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, app);
  }

  deleteApp(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}

