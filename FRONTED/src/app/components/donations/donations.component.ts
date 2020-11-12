import { Component, OnInit } from '@angular/core';
import { donation } from 'src/app/shared/models/Donations.model';
import { DonationService } from 'src/app/shared/services/donation.service';

@Component({
  selector: 'app-donations',
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent implements OnInit {
url='https://localhost:44360/' + 'image/';
  constructor(private donationService: DonationService) { }
  donations: donation[]
  ngOnInit(): void {
    this.donationService.getDonations().subscribe(
      res => {this.donations = res
     this.donations.forEach(d=> d.Picture=this.url+d.Picture) }
    )
  }

}
