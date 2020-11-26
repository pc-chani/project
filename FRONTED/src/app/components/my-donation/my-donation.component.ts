import { Component, OnInit } from '@angular/core';
import { donation } from 'src/app/shared/models/Donations.model';

@Component({
  selector: 'app-my-donation',
  templateUrl: './my-donation.component.html',
  styleUrls: ['./my-donation.component.css']
})

export class myDonationComponent implements OnInit {
  myDonation:donation
  constructor() { }

  ngOnInit(): void {
  }
}


