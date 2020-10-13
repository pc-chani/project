import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoryGMH } from '../models/CategoryGMH.model';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private http:HttpClient) { }
  
  getCategories():Observable<CategoryGMH[]> {
    return this.http.get<CategoryGMH[]>(environment.url+'categories/getCategories');
    }
}
