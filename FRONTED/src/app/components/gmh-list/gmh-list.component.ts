import { Component, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Complaint } from 'src/app/shared/models/Complaint.model';
import { GMH } from 'src/app/shared/models/Gmh.model';
import { GmhService } from 'src/app/shared/services/gmh.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

//import {MDCDialog} from '@material/dialog';
//export interface DialogData {
//  donationCode: string;
//  myDonation: Complaint;
//}
@Component({
  selector: 'app-gmh-list',
  templateUrl: './gmh-list.component.html',
  styleUrls: ['./gmh-list.component.css'],
})
export class GmhListComponent implements OnInit {
 // @Input() gmhsArr:GMH[];
  gmhListForm:FormGroup
  gmhs:GMH[];
newComplaint:Complaint;
d=false;
dataSource; 

@ViewChild(MatPaginator) paginator: MatPaginator;
displayedColumns: string[] = ['position', 'name','a'];
  constructor(public gmhService: GmhService,private router:Router,public dialog: MatDialog,public route:ActivatedRoute) { }

  ngOnInit(): void { 
 
    //this.gmhs=this.route.snapshot.paramMap.get('arr');   
    this.gmhListForm = new FormGroup({
      complainText: new FormControl(''),
    });
    console.log(this.gmhService.gmhsSearch)
    this.dataSource  = new MatTableDataSource<GMH>(this.gmhService.gmhsSearch);
    console.log(this.dataSource)
  }



r(GmhCode:number){
  this.router.navigate(['gmh',GmhCode])
}
openDialog(event){
  this.d=true;
event.stopPropagation();
}
//openDialog(event)
//: void {
//  this.d=true;
//  event.stopPropagation();
//  const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
//    width: '250px',height:'300px',
//    data: { code: this.donationCode, donation: this.myDonation }
//  });
//
//  dialogRef.afterClosed().subscribe(result => {
//    console.log('The dialog was closed');
//    this.myDonation = result;
//  });
//}
stop(event){
event.stopPropagation();
this.d=false;
}

alert2(gmhCode)
{
 this.newComplaint=new Complaint();
 this.newComplaint.gmhCode=gmhCode;
 this.newComplaint.date= Date.now().toString();
 this.newComplaint.text=this.gmhListForm.controls.complainText.value;
 //this.newComplaint.fingerPrint=getBrowserFingerprint(); 
 this.d=false;
console.log(this.newComplaint)
 
}
ngAfterViewInit() {
  this.dataSource.paginator = this.paginator;
}
}



//@Component({
//  selector: 'app-dialog',
//  templateUrl: 'complaintDialog.component.html',
//})
//export class DialogOverviewExampleDialog {
//
//  constructor(
//    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
//    @Inject(MAT_DIALOG_DATA) public data:DialogData) {}
//
//  onNoClick(): void {
//    this.dialogRef.close();
//  }
//  onClick(){
//  
//}
//
//}