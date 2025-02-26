import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environments'
import { Password } from './models/password';
import { PasswordDetails } from './models/passwordDetails';

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

  getPasswordById(id: number): Observable<PasswordDetails> {
    return this.http.get<PasswordDetails>(`${this.apiUrl}/${id}`);
  }
  

  addPassword(password: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, password);
  }

  deletePassword(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}

