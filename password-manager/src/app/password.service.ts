import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environments'

@Injectable({
  providedIn: 'root'
})
export class PasswordService {

  private apiUrl = `${environment.apiBaseUrl}/password`;

  constructor(private http: HttpClient) { }

  private getHeaders() {
    return new HttpHeaders({
      'x-api-key': environment.apiKey
    });
  }

  getPasswords(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  addPassword(password: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, password, { headers: this.getHeaders() });
  }

  deletePassword(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}

