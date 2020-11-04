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
  currentProduct: productToGmh
  newPForm: FormGroup
  imageSrc;
  urls = [];
  selectedDate;
  dates: Array<string> = [];
  ps: Array<Product>
  filteredPs: Observable<Product[]>;
  formData: FormData = new FormData();
  constructor(private router: Router, private route: ActivatedRoute,
    private gmhService: GmhService, private productsServices: ProductsService,
    private http: HttpClient, private sanitizer:DomSanitizer) {
    ;
  }
  ngOnInit(): void {
    this.newPForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      ProductCodeToGMH: new FormControl(),
      ProductCode: new FormControl(),
      GmhCode: new FormControl(),
      Amount: new FormControl(),
      FreeDescription: new FormControl(),
      IsDisposable: new FormControl(),
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
          this.productsServices.getLendigs(p).subscribe(
            res => { p.Lendings = res; }
          )
          this.productsServices.getImage(p).subscribe(
            res => {
              p.Images=new Array<string>();
              res.forEach(r => p.Images.push(r.Path));
            }
          )
        })
      },
    );
  }
  photo(url) { 
   return this.sanitizer.bypassSecurityTrustResourceUrl(url); 
}
  filter() {
    this.filteredPs = this.newPForm.controls.Name.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.ProductName),
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
    console.log(p);
    this.productsServices.delete(p).subscribe(
      res => {
        console.log(res)
        if (res) this.products.splice(this.products.indexOf(p), 1);
      }
    )
  }
  close() {
    this.show = false;
  }
  showdetails(p) {
    this.show = true;
    this.currentProduct = p;
    // if(this.currentProduct.Images!=undefined)
    //this.currentProduct.Images.forEach(i => {
    //this.myImages.push(i.Path)
    //})
  }
  add() {
    this.newProduct = true
  }
  saveChange() {
    this.productsServices.saveChange(this.currentProduct).subscribe(
      res => console.log(res)
    )
    this.show = false;
  }
  addProduct() {
    let p = new productToGmh();
    p.Amount = this.newPForm.controls.Amount.value;
    p.GmhCode = this.myGmh.GmhCode;
    p.ProductCode = 2002;//צריך להחליט מה עושים
    p.FreeDescription = this.newPForm.controls.FreeDescription.value;
    p.IsDisposable = this.newPForm.controls.IsDisposable.value;
    if (p.IsDisposable == null) p.IsDisposable = false;
    p.SecurityDepositAmount = this.newPForm.controls.SecurityDepositAmount.value;
    p.Status = this.newPForm.controls.Status.value;
    this.formData.append('product', JSON.stringify(p));
    this.productsServices.addProduct(this.formData).subscribe(
      res => {
        if (res) {
          alert('נוסף בהצלחה');
          this.setProducts();
          this.urls = new Array<string>();
          this.newPForm.reset();
        }
        else alert('try again')
        console.log(res)
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
    this.currentProduct.Lendings.forEach(l => {
      dates.push(new Date(l.LendingDate).toDateString().slice(4, 15), new Date(l.ReturnDate).toDateString().slice(4, 15));
    });
    if (dates != undefined)
      for (let i = 0; i < dates.length; i += 2)
        if (new Date(d1) >= new Date(dates[i]) && new Date(d1) <= new Date(dates[i + 1])) {
          return 'special-date';
        }
    return '';
  }
  dateFilter(p, d) {
    this.currentProduct = p;
    let d1 = d.toDateString().slice(4, 15);
    let dates: string[] = new Array<string>()
    // console.log(this.currentProduct);

    this.currentProduct.Lendings.forEach(l => {
      dates.push(new Date(l.LendingDate).toDateString().slice(4, 15), new Date(l.ReturnDate).toDateString().slice(4, 15));
    });
    if (dates != undefined)
      for (let i = 0; i < dates.length; i += 2)
        if (new Date(d1) >= new Date(dates[i]) && new Date(d1) <= new Date(dates[i + 1])) {
          return false;
        }
    return true
  }
}
