import { Component, OnInit } from '@angular/core';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { User } from 'src/app/shared/models/User.model';
import { UserService } from 'src/app/shared/services/user.service';
import { GMH } from 'src/app/shared/models/Gmh.model';

@Component({
  selector: 'app-manage-the-gmh',
  templateUrl: './manage-the-gmh.component.html',
  styleUrls: ['./manage-the-gmh.component.css']
})
export class ManageTheGMHComponent implements OnInit {
  currentUser: User
  myGmhim:GMH[]
  constructor(private gmhService: GmhService,private userService:UserService) { }

  ngOnInit(): void {
    this.currentUser=this.userService.currentUser;
    this.getMyGmhim();
    console.log(this.myGmhim)
  }
  getMyGmhim(){
    this.gmhService.getMyGmhim(this.userService.currentUser).subscribe(
      res => {this.myGmhim=res; console.log(res); },
      err => { console.log(err); }
     
    )}

}
