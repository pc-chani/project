import { Component, Inject } from '@angular/core';
import { User } from './shared/models/User.model';
import { UserService } from './shared/services/user.service';
import { CookieService } from 'ngx-cookie-service'
import { donation } from './shared/models/Donations.model';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GMH } from './shared/models/Gmh.model';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { GmhService } from './shared/services/gmh.service';
export interface DialogData {
  donationCode: string;
  myDonation: donation;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  show = false;
  currentUser: User ;
  cookieValue;
  donationCode: number
  myDonation: donation
  //gmhsArr:GMH[];

  constructor(private gmhService:GmhService,private route:Router, private cookieService: CookieService,public dialog: MatDialog,public userService: UserService) { }
  ngOnInit(): void {

    this.cookieValue = this.cookieService.get('userName');
    console.log(localStorage.getItem('user'));
   // if(localStorage.getItem('user')!=undefined)
   console.log(Object.keys(localStorage.getItem('user')).length);
   
   if(Object.keys(localStorage.getItem('user')).length!=2)
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.setCurrentUser(this.currentUser)
    console.log(this.cookieValue);
    this.currentUser = this.userService.CurrentUser;
    console.log(this.currentUser);
  }
  message() {
    this.show = true
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(dialog, {
      width: '250px',height:'300px',
      data: { code: this.donationCode, donation: this.myDonation }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.myDonation = result;
    });
  }
  exit(){
    this.currentUser=undefined;
    this.userService.setCurrentUser(undefined);
    localStorage.setItem('user', JSON.stringify(new User()));
    this.cookieValue = undefined

  }
  getGmahim(){   
    this.gmhService.getAllGmhs().subscribe(res => {
      this.gmhService.gmhsSearch = res;
    this.route.navigate(['/gmhList'])   }, 
      err => { console.log(err); }
    );
    }
}
@Component({
  selector: 'app-dialog',
  templateUrl: 'dialog.component.html',
})
export class dialog {

  constructor(
    public dialogRef: MatDialogRef<dialog>,
    @Inject(MAT_DIALOG_DATA) public data:DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
 //onGetGmhs(gmhsArr:GMH[]){
 //  this.cars.push({

 //  })
 // }


}