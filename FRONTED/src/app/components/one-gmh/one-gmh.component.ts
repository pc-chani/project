import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { productToGmh } from 'src/app/shared/models/productToGMH.model';
import { ProductsService } from 'src/app/shared/services/products.service';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCalendarCellCssClasses, MatDatepicker } from '@angular/material/datepicker';


@Component({
  selector: 'app-one-gmh',
  templateUrl: './one-gmh.component.html',
  styleUrls: ['./one-gmh.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class OneGmhComponent implements OnInit {
  @ViewChild(MatDatepicker) datepicker: MatDatepicker<Date>;
  myGmh: GMH
  products: productToGmh[];
  show = false
  newProduct = false
  currentProduct: productToGmh
  newPForm: FormGroup
  imageSrc;
  selectedDate;
  lendings=[new Date (2020,10,14)]
  dates:Array<string>=[];
  constructor(private router: Router, private route: ActivatedRoute, private gmhService: GmhService, private productsServices: ProductsService) { }

  ngOnInit(): void {
   
    this.lendings.forEach(l=>{
    this.dates.push(l.toDateString().slice(4,15))
  });
    this.newPForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      ProductCodeToGMH: new FormControl(),
      ProductCode: new FormControl(),
      GmhCode: new FormControl(),
      Picture: new FormArray([]),
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
    p.Picture = this.newPForm.controls.Picture.value;
    this.productsServices.addProduct(p).subscribe(
      res => {
        if (res) alert('נוסף בהצלחה');
        else alert('try again')
        console.log(res)
      }
    )

    this.newProduct = false;

  }
  handleFileInput(etf) {
    console.log(etf);
    if (etf && etf[0]) {
      const file = etf[0];
      const reader = new FileReader();
      reader.onload = e => this.imageSrc = reader.result;
      reader.readAsDataURL(file);

    }
  }
  onSelect(event) {
    this.selectedDate = event;
  }

  dateClass = (d: Date): MatCalendarCellCssClasses => {

    return (d.getDate() === 1) ? 'special-date' : '';

  }
  dateFilter = (d: Date) =>{  
    let d1= d.toDateString().slice(4,15)
    return (this.dates.indexOf(d1)==-1) 
  }
}
