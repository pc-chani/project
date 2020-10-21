import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { productToGmh } from 'src/app/shared/models/productToGMH.model';
import { ProductsService } from 'src/app/shared/services/products.service';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCalendarCellCssClasses, MatDatepicker } from '@angular/material/datepicker';
import { Product } from 'src/app/shared/models/Product.model';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { FileDetector } from 'protractor';


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
  lendings = [new Date(2020, 10, 14)]
  dates: Array<string> = [];
  ps = Array<Product>()
  filteredPs: Observable<Product[]>;
  formData: FormData
  constructor(private router: Router, private route: ActivatedRoute,
    private gmhService: GmhService, private productsServices: ProductsService,
    private http: HttpClient) {
    ;

  }

  ngOnInit(): void {
    this.lendings.forEach(l => {
      this.dates.push(l.toDateString().slice(4, 15))
    });
    this.newPForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      ProductCodeToGMH: new FormControl(),
      ProductCode: new FormControl(),
      GmhCode: new FormControl(),
      //Picture: new FormControl(),
      Amount: new FormControl(),
      FreeDescription: new FormControl(),
      IsDisposable: new FormControl(),
      SecurityDepositAmount: new FormControl(),
      Status: new FormControl(),
    })
    const GmhCode = this.route.snapshot.paramMap.get('id');
    this.myGmh = this.gmhService.getOneGmh(parseInt(GmhCode));
    this.productsServices.getProducts(this.myGmh).subscribe(
      res => {
        this.products = res; console.log(res);
        this.products.forEach(p => {
          this.productsServices.getProduct(p).subscribe(
            res => { p.Name = res.Productname; console.log(res); },
            err => { console.log(err); }
          )
        });
      },
      err => { console.log(err); }
    )
    this.filteredPs = this.newPForm.controls.Name.valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.CategoryName),
        map(name => name ? this._filter(name) : this.ps.slice())
      );
  }
  private _filter(name: string): Product[] {
    const filterValue = name.toLowerCase();

    return this.ps.filter(p => p.Productname.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(p: Product): string {
    return p && p.Productname ? p.Productname : '';
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
    p.ProductCode = 2002;
    p.FreeDescription = this.newPForm.controls.FreeDescription.value;
    p.IsDisposable = this.newPForm.controls.IsDisposable.value;
    p.Name = this.newPForm.controls.Name.value;
    p.SecurityDepositAmount = this.newPForm.controls.SecurityDepositAmount.value;
    p.Status = this.newPForm.controls.Status.value;
    // p.Picture = this.newPForm.controls.Picture.value;
   // p.Picture = new FormData()
    
   // p.Picture=this.formData
   this.formData.append('product', JSON.stringify(p));
    this.productsServices.addProduct(this.formData).subscribe(
      res => {
        if (res) {
          alert('נוסף בהצלחה');
          this.products.push(p);
          this.imageSrc = '';
          this.newPForm.reset();
        }
        else alert('try again')
        console.log(res)
      }
    )
    this.newProduct = false;
  }
  handleFileInput(etf) {
    
    this.formData = new FormData();
    for (const file of etf) {
      this.formData.append('Image', file, file.name);
    }
    //  this.formData.append('Image', etf[0], etf[0].name);
    console.log(etf);
    if (etf && etf[0]) {
      for (const f of etf) {
      const file = f;
      const reader = new FileReader();
      reader.onload = e => this.urls.push( reader.result);
      reader.readAsDataURL(file);
      }
      console.log(this.urls);    
     // this.upload()
    }
  }
 // upload() {
   // this.productsServices.postImage(this.formData).subscribe(
     // res => console.log(res)
    //);
  //}
  onSelect(event) {
    this.selectedDate = event;
  }
  dateClass = (d: Date): MatCalendarCellCssClasses => {
    return (d.getDate() === 1) ? 'special-date' : '';
  }
  dateFilter = (d: Date) => {
    let d1 = d.toDateString().slice(4, 15)
    return (this.dates.indexOf(d1) == -1)
  }

}
