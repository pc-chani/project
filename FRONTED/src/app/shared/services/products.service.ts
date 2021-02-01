import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/Product.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GMH } from '../models/Gmh.model';
import { Images, productToGmh } from '../models/productToGMH.model';
import { Lending } from '../models/Lending.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProductsForGMH(mygmh: GMH): Observable<productToGmh[]> {
    return this.http.post<productToGmh[]>(environment.url + 'products/GetProductsForGMH', mygmh);
  }
  getProduct(ptg: productToGmh): Observable<Product> {
    return this.http.post<Product>(environment.url + 'products/GetProduct', ptg);
  }
  edit(fd: FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/Edit', fd);
  }
  changeAmount(product: productToGmh): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/SaveChange', product);
  }
  addProduct(fd: FormData): Observable<boolean> {
    return this.http.post<boolean>(environment.url + 'products/PostImg', fd)
  }
  addPr(p: Product): Observable<number> {
    return this.http.post<number>(environment.url + 'products/AddProduct', p)
  }
  delete(p: productToGmh): Observable<boolean> {
    if(p!=null)
    return this.http.post<boolean>(environment.url + 'products/Delete', p);
  }
  getImage(p:productToGmh): Observable<Images[]> {
    return this.http.post<Images[]>(environment.url + 'products/GetImg',p);
  }
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.url + 'products/GetProducts');
  }
  getProductsAccordingToGmhCategory(myGmh: GMH): Observable<Product[]> {
    return this.http.post<Product[]>(environment.url + 'products/GetProductsAccordingToGmhCategory', myGmh);
  }
  getLendings(p: productToGmh): Observable<Lending[]> {
    return this.http.post<Lending[]>(environment.url + 'lendings/GetLendings', p);

  }
  addLending(l:Lending):Observable<boolean>{
    return this.http.post<boolean>(environment.url + 'lendings/AddLending', l);
  }
  setLending(l:Lending):Observable<boolean>{
    return this.http.post<boolean>(environment.url + 'lendings/SetLending', l);
  }
  removeLending(l:Lending):Observable<boolean>{
    return this.http.post<boolean>(environment.url + 'lendings/DeleteLending', l);
  }
}
