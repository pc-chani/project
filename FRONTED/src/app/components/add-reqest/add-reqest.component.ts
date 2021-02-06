import { Component, OnInit } from '@angular/core';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { ProductsService } from 'src/app/shared/services/products.service';
import { Product } from 'src/app/shared/models/Product.model';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { RequestForLoan } from 'src/app/shared/models/RequestForLoan.model';
import { ReqestService } from 'src/app/shared/services/reqest.service';


@Component({
  selector: 'app-add-reqest',
  templateUrl: './add-reqest.component.html',
  styleUrls: ['./add-reqest.component.css']
})
export class AddReqestComponent implements OnInit {
  myGmh:GMH
  addReqestForm:FormGroup
  filteredProducts: Observable<Product[]>;
  products: Array<Product>
  myReqest:RequestForLoan
  constructor(private route:ActivatedRoute,private reqestService:ReqestService, private gmhService:GmhService,private productsService:ProductsService) { }

  ngOnInit(): void {
    const GmhCode = this.route.snapshot.paramMap.get('id');
    this.myGmh= this.gmhService.getOneGmh(parseInt(GmhCode))
   console.log(this.myGmh)
    this.addReqestForm = new FormGroup({
      comments: new FormControl('', Validators.required),
      textSearch: new FormControl(''),
  });

    this.productsService.getProductsAccordingToGmhCategory(this.myGmh).subscribe(
      res => {
        this.products = res,
        console.log(this.products)
          this.filter()     },
      err => console.log(err),
    );}

  filter() {
    this.filteredProducts = this.addReqestForm.controls.textSearch.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.ProductName),
        map(name => name ? this._filter(name) : this.products.slice())
      );
  }

  private _filter(name: string): Product[] {
    const filterValue = name.toLowerCase();
    return this.products.filter(c => c.Productname.toLowerCase().indexOf(filterValue) === 0);
  }
 
  displayFn(c: Product): string {
    return c && c.Productname ? c.Productname : '';
  }

  addReqest(){
    this.myReqest=new RequestForLoan()
    this.myReqest.UserCode=this.myGmh.UserCode
    console.log(this.addReqestForm.controls.textSearch.value)
    this.myReqest.ProductCode=this.addReqestForm.controls.textSearch.value.ProductCode
    //this.myReqest.RequestDate=
    this.myReqest.comment=this.addReqestForm.controls.comments.value
    console.log(this.myReqest)
    this.reqestService.addReqest(this.myReqest).subscribe(
      res => {
        console.log(res)
        if (res) {
        // this.getMyGmhim();
        // this.closeNew();
        // this.getCategoryGmh();
        // this.gmhForm.reset()
          alert('נוסף בהצלחה')
        }
        else {
          alert('error, try again')
        }
      }
    ) }
    

}
