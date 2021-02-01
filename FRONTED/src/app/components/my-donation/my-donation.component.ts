import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { donation } from 'src/app/shared/models/Donations.model';
import { DonationService } from 'src/app/shared/services/donation.service';
import { MatIconModule } from '@angular/material/icon'
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Address } from 'ngx-google-places-autocomplete/objects/address';

@Component({
  selector: 'app-my-donation',
  templateUrl: './my-donation.component.html',
  styleUrls: ['./my-donation.component.css']
})

export class myDonationComponent implements OnInit {
  myDonation: donation
  edit: boolean = false
  editDForm: FormGroup
  adress: string;
  formData: FormData=new FormData();
  url: string | ArrayBuffer;
  constructor(private route: ActivatedRoute, private donationService: DonationService) { }

  ngOnInit(): void {
    const donationCode = this.route.snapshot.paramMap.get('id');
    this.donationService.getDonation(parseInt(donationCode)).subscribe(
      res => this.myDonation = res
    )
    this.editDForm = new FormGroup({
      donationName: new FormControl(Validators.required),
      // Category: number
      // MasterCategory: number
      Description: new FormControl(),
      donorEmail: new FormControl(Validators.email),
      Adress: new FormControl(),
      Phone: new FormControl(Validators.pattern('[0-9]{9}')),
    }
    )
  }
  handleDestinationChange(a: Address) {
    this.adress = a.formatted_address;
    // console.log(a)
  }
  toDelete() {
    if (confirm("Are you sure to delete")) {
      this.donationService.removeDonation(this.myDonation).subscribe(
        res => {
          console.log(res)
        }
      )
    }
  }
  toEdit() {
    this.edit = true
    this.editDForm.controls.donationName.setValue(this.myDonation.donationName)
    this.editDForm.controls.Description.setValue(this.myDonation.Description)
    this.editDForm.controls.Adress.setValue(this.myDonation.Adress)
    this.editDForm.controls.Phone.setValue(this.myDonation.Phone)

  }
  saveChanges(){
    let d=new donation();
    d.donationName = this.editDForm.controls.donationName.value;
    d.Description=this.editDForm.controls.Description.value;
    d.donationCode=this.myDonation.donationCode
    this.formData.append('donation', JSON.stringify(d))
    this.donationService.savechanges(this.formData).subscribe(
      res => console.log(res)   
    )
  }
  handleFileInput(etf) {
    this.formData.append('Image', etf[0]);
    console.log(etf);
    if (etf && etf[0]) {
      for (const f of etf) {
        const file = f;
        const reader = new FileReader();
        reader.onload = e => this.url = reader.result;
        reader.readAsDataURL(file);
      }
      //  console.log(this.url);
    }
  }
}


