import { Component, Inject } from '@angular/core';
import { User } from './shared/models/User.model';
import { UserService } from './shared/services/user.service';
import { CookieService } from 'ngx-cookie-service'
import { donation } from './shared/models/Donations.model';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

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
  currentUser: User = this.userService.CurrentUser;
  cookieValue;
  donationCode: number
  myDonation: donation
  constructor(public userService: UserService, private cookieService: CookieService,public dialog: MatDialog) { }
  ngOnInit(): void {

    this.cookieValue = this.cookieService.get('userName');
    
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.setCurrentUser(this.currentUser)
    console.log(this.cookieValue);
  // if(this.currentUser!=null)
   // this.currentUser.Name = this.cookieValue;
    console.log(this.currentUser);
  }
  message() {
    this.show = true
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '250px',height:'300px',
      data: { code: this.donationCode, donation: this.myDonation }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.myDonation = result;
    });
  }
}
@Component({
  selector: 'app-dialog',
  templateUrl: 'dialog.component.html',
})
export class DialogOverviewExampleDialog {

  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data:DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
  onClick(){
  
}

}