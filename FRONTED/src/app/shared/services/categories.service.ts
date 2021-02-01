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
  addCategory(c: CategoryGMH):Observable<number> {
    return this.http.post<number>(environment.url+'categories/addCategory',c);   
  }
  getCategories():Observable<CategoryGMH[]> {
    return this.http.get<CategoryGMH[]>(environment.url+'categories/getCategories');
    }
    getCategoryName(category: number):Observable<string> {
      return this.http.post<string>(environment.url+'categories/getCategoryName',category);

    } 
}
