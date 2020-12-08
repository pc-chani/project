import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CategoryGMH } from 'src/app/shared/models/CategoryGMH.model';
import { donation } from 'src/app/shared/models/Donations.model';
import { DonationService } from 'src/app/shared/services/donation.service';
import { GmhService } from 'src/app/shared/services/gmh.service';

@Component({
  selector: 'app-donations',
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent implements OnInit {
  url = 'https://localhost:44360/image/';
  adress: string;
  categories: CategoryGMH[]
  filteredCategories: Observable<CategoryGMH[]>
  tatCategories: CategoryGMH[]
  filteredTatCategories: Observable<CategoryGMH[]>
  categoriesControl = new FormControl();
  tatcategoriesControl = new FormControl();

  constructor(private donationService: DonationService, private gmhService: GmhService) { }
  donations: donation[]
  ngOnInit(): void {
    this.donationService.getDonations().subscribe(
      res => {
        this.donations = res
        //.donations.forEach(d => d.Picture = this.url + d.Picture)
      }
    )
    this.getCategoryGmh()
  }
  handleDestinationChange(a: Address) {
    var value = a.address_components
    console.log(value)
    this.adress = a.formatted_address;
  }
  getCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        this.adress = (position.coords.latitude + " " + position.coords.longitude).toString();
      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }

  }
  getCategoryGmh() {
    this.gmhService.getCategoryGmach().subscribe(res => {
      this.categories = res, console.log(res);
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
  filterDonations() {
    let fd = new FormData()
   if (this.categoriesControl.value == null) fd.append('category', '0')
   else fd.append('category', this.categoriesControl.value.CategoryCode)
   if (this.tatcategoriesControl.value == null) fd.append('tatcategory', '0')
   else fd.append('tatcategory', this.tatcategoriesControl.value.CategoryCode)
    fd.append('adress', this.adress)
    this.donationService.filterDonations(fd).subscribe(
      res => this.donations = res
    )
  }
}
