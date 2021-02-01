import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RequestForLoan } from '../models/RequestForLoan.model';


@Injectable({
  providedIn: 'root'
})
export class ReqestService {

  constructor(private http: HttpClient) { }
  addReqest(newReqest:RequestForLoan): Observable<number> {
    console.log(newReqest)   
    return this.http.post<number>(environment.url + 'reqest/addReqest',newReqest);
  }
}
