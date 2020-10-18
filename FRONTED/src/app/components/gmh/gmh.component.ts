import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { User } from 'src/app/shared/models/User.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-gmh',
  templateUrl: './gmh.component.html',
  styleUrls: ['./gmh.component.css']
})
export class GMHComponent implements OnInit {
@Input('gmh') gmhs:GMH[];

  
  constructor(private userService: UserService) { }

  ngOnInit(): void {

  }

  

}
