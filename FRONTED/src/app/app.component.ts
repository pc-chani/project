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
  constructor(private userService: UserService, private cookieService: CookieService,public dialog: MatDialog) { }
  ngOnInit(): void {

    this.cookieValue = this.cookieService.get('userName');
    console.log(localStorage.getItem('user'));
   // if(localStorage.getItem('user')!=undefined)
    this.currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.setCurrentUser(this.currentUser)
    console.log(this.cookieValue);
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

}