import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { CategoriesService } from 'src/app/shared/services/categories.service';
import { GmhService } from 'src/app/shared/services/gmh.service';

@Component({
  selector: 'app-needs-gmhim',
  templateUrl: './needs-gmhim.component.html',
  styleUrls: ['./needs-gmhim.component.css']
})
export class NeedsGMHimComponent implements OnInit {
  needsGmhim;
  categories: CategoryGMH[]
  filteredCategories: Observable<CategoryGMH[]>
  tatCategories: CategoryGMH[]
  filteredTatCategories: Observable<CategoryGMH[]>
  categoriesControl = new FormControl();
  tatcategoriesControl = new FormControl();
  adress;
  displayedColumns = ["category", "adress"]
  constructor(private gmhService: GmhService, private categoriesService: CategoriesService) { }

  ngOnInit(): void {
    this.gmhService.getNeedsGmhim().subscribe(
      res => {
        this.needsGmhim = res;
        this.needsGmhim.forEach(ng => {
          this.categoriesService.getCategoryName(ng.category).subscribe(
            res => ng.categoryName = res
          )
        });
     //   console.log(this.needsGmhim);
      }
    )
    this.getCategoryGmh()
  }
  getCategoryGmh() {
    this.gmhService.getCategoryGmach().subscribe(res => {
      this.categories = res// console.log(res);
      this.filteredCategories = this.categoriesControl.valueChanges
        .pipe(
          startWith(''),
          map(value => typeof value === 'string' ? value : value.categoryName),
          map(name => name ? this._filter(name) : this.categories.slice())
        );
    }
      ,
      err => { console.log(err); }
    );
  }
  private _filter(name: string): CategoryGMH[] {
    const filterValue = name.toLowerCase();
    return this.categories.filter(c => c.CategoryName.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(c: CategoryGMH): string {
    return c && c.CategoryName ? c.CategoryName : '';
  }
  getTatCategoriesForGmh(c) {
    //console.log(this.gmhForm.controls["newTatCategory"].disabled);
    this.gmhService.getCategoriesForGmach(c.option.value).subscribe(res => {
      this.tatCategories = res;
      console.log(res),
        this.filteredTatCategories = this.tatcategoriesControl.valueChanges
          .pipe(
            startWith(''),
            map(value => typeof value === 'string' ? value : value.CategoryName),
            map(name => name ? this._filter(name) : this.tatCategories.slice())
          );
      err => { console.log(err); }
    });
  }
  handleDestinationChange(a: Address) {
    //console.log(a);
    
    console.log(a.vicinity);
    
   
    this.adress = a.formatted_address;
  }
  getCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        console.log(position.coords.latitude,position.coords.longitude);
        
        this.adress = (position.coords.latitude+ " "+ position.coords.longitude).toString();
        console.log(this.adress);

      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }

  }
  filterNeedsGmhim() {
    let fd = new FormData()
    console.log(this.categoriesControl.value,this.tatcategoriesControl.value);
    if (this.categoriesControl.value == null || this.categoriesControl.value == "") fd.append('category', "0")
    else fd.append('category', this.categoriesControl.value.CategoryCode)
    if (this.tatcategoriesControl.value == null || this.tatcategoriesControl.value == "") fd.append('tatcategory', "0")
    else fd.append('tatcategory', this.tatcategoriesControl.value.CategoryCode)
    console.log(this.adress);
    
    fd.append('adress', this.adress)
    this.gmhService.filterNeedsGmhim(fd).subscribe(
      res => {
        this.needsGmhim = res
        this.needsGmhim.forEach(ng => {
          this.categoriesService.getCategoryName(ng.category).subscribe(
            res => ng.categoryName = res
          )
        });
      }
    )
    //this.adress = ""
  }

}
