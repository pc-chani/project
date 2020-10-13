import { Component, OnInit } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { Address } from 'ngx-google-places-autocomplete/objects/address';
import { GooglePlaceDirective } from 'ngx-google-places-autocomplete';


@Component({
  selector: 'app-search-gmh',
  templateUrl: './search-gmh.component.html',
  styleUrls: ['./search-gmh.component.css'],

})
export class SearchGMHComponent implements OnInit {

  constructor(private gmhService: GmhService) { }

  ngOnInit(): void {
    this.getCurrentLocation()
  }

  handleDestinationChange(a: Address) {
    console.log(a)
  }
  getCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
      let origin=  position.coords.latitude + position.coords.longitude;
        console.log(position)
      });
    }
    else {
      alert("Geolocation is not supported by this browser.");
    }
  }
}