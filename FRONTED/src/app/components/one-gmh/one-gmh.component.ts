import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { productToGmh } from 'src/app/shared/models/productToGMH.model';
import { ProductsService } from 'src/app/shared/services/products.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCalendarCellCssClasses, MatDatepicker } from '@angular/material/datepicker';
import { Product } from 'src/app/shared/models/Product.model';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { Lending } from 'src/app/shared/models/Lending.model';
'use strict';
export var currentProduct: productToGmh
@Component({
  selector: 'app-one-gmh',
  templateUrl: './one-gmh.component.html',
  styleUrls: ['./one-gmh.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class OneGmhComponent implements OnInit {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  myGmh: GMH;
  products: productToGmh[];
  show = false
  newProduct = false
 
  newPForm: FormGroup
  editPForm: FormGroup

  imageSrc;
  urls = [];
  selectedDate;
  dates: Array<string> = [];
  da: Array<Date> = []
  ps: Array<Product>
  filteredPs: Observable<Product[]>;
  formData: FormData = new FormData();
  startDate; endDate;
  constructor(private router: Router, private route: ActivatedRoute,
    private gmhService: GmhService, private productsServices: ProductsService,
    private http: HttpClient, private sanitizer: DomSanitizer) {
    ;
  }
  ngOnInit(): void {
    this.editPForm = new FormGroup({
      Name: new FormControl(),
      FreeDescription: new FormControl(),
      IsDisposable: new FormControl(),
      SecurityDepositAmount: new FormControl(),
      Status: new FormControl(),
    })
    this.newPForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      ProductCodeToGMH: new FormControl(),
      ProductCode: new FormControl(),
      GmhCode: new FormControl(),
      Amount: new FormControl(),
      FreeDescription: new FormControl(),
      IsDisposable: new FormControl({ checked: false }, Validators.required),
      SecurityDepositAmount: new FormControl(),
      Status: new FormControl(),
    })
    const GmhCode = this.route.snapshot.paramMap.get('id');
    this.myGmh = this.gmhService.getOneGmh(parseInt(GmhCode));
    this.setProducts();
    this.productsServices.getProductsAccordingToGmhCategory(this.myGmh).subscribe(
      res => {
        this.ps = res,
          this.filter();
      },
      err => console.log(err),
    );
  }
  get CurrentProduct(){
    return currentProduct;
  }
  setProducts() {
    this.productsServices.getProductsForGMH(this.myGmh).subscribe(
      res => {
        this.products = res;
        console.log(res);

        this.products.forEach(p => {
          this.productsServices.getProduct(p).subscribe(
            res => { p.Name = res.Productname; },
            err => { console.log(err); }
          )
          this.productsServices.getLendings(p).subscribe(
            res => {
              p.Lendings = res;
              res.forEach(r => this.da.push(r.LendingDate, r.ReturnDate))
            }

          )
          this.productsServices.getImage(p).subscribe(
            res => {
              p.Images = new Array<string>();
              res.forEach(r => p.Images.push('https://localhost:44360/' + 'image/' + r.Path));
            }
          )
        })
      },
    );
  }
  filter() {
    this.filteredPs = this.newPForm.controls.Name.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.Productname),
        map(name => name ? this._filter(name) : this.ps.slice())
      );
  }
  private _filter(name: string): Product[] {
    const filterValue = name.toLowerCase();
    return this.ps.filter(c => c.Productname.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(c: Product): string {
    return c && c.Productname ? c.Productname : '';
  }
  deleteProduct(p) {
    //console.log(p);
    if (confirm("Are you sure to delete")) {
      this.productsServices.delete(p).subscribe(
        res => {
          console.log(res)
          if (res) this.products.splice(this.products.indexOf(p), 1);
        }
      )
    }
  }
  close() {
    this.show = false;
  }
  showdetails(p) {
    console.log(p);

    this.show = true;
    currentProduct = p;
    this.editPForm.controls.Name.setValue(p.Name);
    this.editPForm.controls.FreeDescription.setValue(p.FreeDescription);
    this.editPForm.controls.IsDisposable.setValue(p.IsDisposable);
    this.editPForm.controls.Status.setValue(p.Status);
    this.editPForm.controls.SecurityDepositAmount.setValue(p.SecurityDepositAmount);
    // if(currentProduct.Images!=undefined)
    //currentProduct.Images.forEach(i => {
    //this.myImages.push(i.Path)
    //})
  }
  add() {
    this.newProduct = true
  }
  changeAmount(p, v) {
    currentProduct = p;
    currentProduct.Amount = v;
  }
  saveChange(p) {
    currentProduct = p;
    if (!p.IsDisposable) {
      let l = new Lending();
      l.LendingDate = this.startDate;
      l.ReturnDate = this.endDate;
      l.UserCode = this.myGmh.UserCode;
      l.ProductCode = p.ProductCodeToGMH;
      this.productsServices.addLending(l).subscribe(
        res => console.log(res)

      )
    }
    else {
      this.productsServices.changeAmount(p).subscribe(
        res => console.log(res)

      )
    }
    this.setProducts()
  }
  edit() {
    let p = new productToGmh();
    p.ProductCodeToGMH = currentProduct.ProductCodeToGMH
    p.ProductCode = 2002;//צריך להחליט מה עושים
    p.FreeDescription = this.editPForm.controls.FreeDescription.value;
    p.IsDisposable = this.editPForm.controls.IsDisposable.value;
    if (p.IsDisposable == null) p.IsDisposable = false;
    p.SecurityDepositAmount = this.editPForm.controls.SecurityDepositAmount.value;
    p.Status = this.editPForm.controls.Status.value;
    this.formData.append('product', JSON.stringify(p));
    this.productsServices.edit(this.formData).subscribe(
      res => {
        console.log(res)
        if (res) {
          alert('השינויים נשמרו בהצלחה');
          this.setProducts();
          this.urls = new Array<string>();
          this.formData = new FormData();

        }
      }
    )
    this.show = false;
    this.editPForm.reset()
  }
  setProduct() {
    let p = new productToGmh();
    if (this.ps.includes(this.newPForm.controls.Name.value)) {
      p.ProductCode = this.newPForm.controls.Name.value.ProductCode
      this.addProduct(p)
    }
    else {
      let pr = new Product();
      pr.Productname = this.newPForm.controls.Name.value;
      pr.CategoryCode = this.myGmh.CategoryCode;
      this.productsServices.addPr(pr).subscribe(
        res => {
          p.ProductCode = res;
          console.log(res);
          this.addProduct(p);
        }
      )
    }
  }
  addProduct(p) {
    p.Amount = this.newPForm.controls.Amount.value;
    p.GmhCode = this.myGmh.GmhCode;
    p.FreeDescription = this.newPForm.controls.FreeDescription.value;
    p.IsDisposable = this.newPForm.controls.IsDisposable.value;
    if (p.IsDisposable == null) p.IsDisposable = false;
    p.SecurityDepositAmount = this.newPForm.controls.SecurityDepositAmount.value;
    p.Status = this.newPForm.controls.Status.value;
    //console.log(p);
    this.formData.append('product', JSON.stringify(p));
    this.productsServices.addProduct(this.formData).subscribe(
      res => {
        if (res) {
          alert('נוסף בהצלחה');
          this.setProducts();
          this.urls = new Array<string>();
          this.formData = new FormData();
          this.newPForm.reset();
        }
        else alert('try again')
        //     console.log(res)
      }

    )
    this.newProduct = false;
  }
  handleFileInput(etf) {
    let i = 0;
    for (const file of etf) {
      this.formData.append('Image' + i, file);
      i++;
    }
    console.log(etf);
    if (etf && etf[0]) {
      for (const f of etf) {
        const file = f;
        const reader = new FileReader();
        reader.onload = e => this.urls.push(reader.result);
        reader.readAsDataURL(file);
      }
      console.log(this.urls);
    }
  }
  onSelect(event) {
    this.selectedDate = event;
  }
  dateClass = (d: Date): MatCalendarCellCssClasses => {
    let d1 = d.toDateString().slice(4, 15);
    let dates: string[] = new Array<string>()
    if (currentProduct.Lendings != undefined)
      currentProduct.Lendings.forEach(l => {
        dates.push(new Date(l.LendingDate).toDateString().slice(4, 15), new Date(l.ReturnDate).toDateString().slice(4, 15));
      });
    if (dates != undefined)
      for (let i = 0; i < dates.length; i += 2)
        if (new Date(d1) >= new Date(dates[i]) && new Date(d1) <= new Date(dates[i + 1])) {
          {
             return 'special-date'; console.log(1);
          }
        }
    return '';
  }
  setStartDate(d) {
    this.startDate = d;
    console.log(this.startDate, this.endDate);
  }
  setEndDate(d) {
    this.endDate = d;
    console.log(this.startDate, this.endDate);
  }
  rangeFilter(date: Date): boolean {
    let d1 = date.toDateString().slice(4, 15);
    let dates: string[] = new Array<string>()
    console.log(currentProduct);

 currentProduct.Lendings.forEach(l => {
   dates.push(new Date(l.LendingDate).toDateString().slice(4, 15), new Date(l.ReturnDate).toDateString().slice(4, 15));
 });
 if (dates != undefined)
   for (let i = 0; i < dates.length; i += 2)
     if (new Date(d1) >= new Date(dates[i]) && new Date(d1) <= new Date(dates[i + 1])) {
       return false;
     }
 //  console.log(d);  

    return true
  }
  myP(p) {
    //console.log(p);  
    currentProduct = p;
  }
 
}
