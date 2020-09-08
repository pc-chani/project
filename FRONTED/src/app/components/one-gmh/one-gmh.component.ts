import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';

@Component({
  selector: 'app-one-gmh',
  templateUrl: './one-gmh.component.html',
  styleUrls: ['./one-gmh.component.css']
})
export class OneGmhComponent implements OnInit {
myGmh:GMH
  constructor(private router:Router,private route:ActivatedRoute,private gmhService:GmhService) { }

  ngOnInit(): void {
    const GmhCode = this.route.snapshot.paramMap.get('id');
this.myGmh=this.gmhService.getOneGmh(parseInt( GmhCode));
  }

}
