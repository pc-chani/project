import { Component, OnInit } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';

declare var google;
@Component({
  selector: 'app-search-gmh',
  templateUrl: './search-gmh.component.html',
  template:'https://maps.googleapis.com/maps/api/js?key=AIzaSyC8jpgtKXljRdzM5zWFhqKbDnCknpsBnV8&libraries=places',
  styleUrls: ['./search-gmh.component.css'],

})
export class SearchGMHComponent implements OnInit {

  constructor(private gmhService:GmhService) { }
   map;
   infoWindow;

  ngOnInit(): void {
    this.initMap()
  }
  initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
      center: {lat: -34.397, lng: 150.644},
      zoom: 8
    });
    var pyrmont = new google.maps.LatLng(-33.8665433,151.1956316);

    var request = {
      location: pyrmont,
      radius: '500',
      type: ['restaurant']
    };
   var service = new google.maps.places.PlacesService(map);
  service.nearbySearch(request,this.callback);
}
 callback(results, status) {
  if (status == google.maps.places.PlacesServiceStatus.OK){
    console.log(results);
    
    }
  }
//search(){
//this.gmhService.search().subscribe(
//  res=> console.log(res)
//)
//}
}