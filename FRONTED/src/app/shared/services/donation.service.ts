import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoryGMH } from '../models/CategoryGMH.model';
import { donation } from '../models/Donations.model';

@Injectable({
  providedIn: 'root'
})
export class DonationService {

  constructor(private http: HttpClient) { }
  getDonationsAcorddingToCaategory(c:CategoryGMH): Observable<donation[]> {
    return this.http.post<donation[]>(environment.url + 'donation/GetDonationsAcorddingToCatrogoty',c);
  }
  getDonationsAcorddingToAdress(adress:string): Observable<donation[]> {
    return this.http.post<donation[]>(environment.url + 'donation/GetDonationsAcorddingToAdress',adress);
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
}
