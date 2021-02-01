import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { donation } from '../models/Donations.model';

@Injectable({
  providedIn: 'root'
})
export class DonationService {
  constructor(private http: HttpClient) { }
  filterDonations(fd:FormData): Observable<donation[]> {
    return this.http.post<donation[]>(environment.url + 'donation/filterDonations',fd);
  }
  getDonations(): Observable<donation[]> {
    return this.http.get<donation[]>(environment.url + 'donation/getDonations');
  }
  addDonation(fd:FormData): Observable<number> {   
    return this.http.post<number>(environment.url + 'donation/AddDonation',fd);
  }
  removeDonation(d:donation): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'donation/DeleteDonation',d);
  }
  getDonation(code: number) {
    return this.http.post<donation>(environment.url + 'donation/getDonation',code);
  }
  savechanges(fd:FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'donation/savechanges',fd);
  }
}
