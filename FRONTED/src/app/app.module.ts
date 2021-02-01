import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MatPaginatorModule} from '@angular/material/paginator';
//import { dialog } from './components/dialog/dialog.component';

import { AppRoutingModule } from './app-routing.module';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { UserService } from './shared/services/user.service';
import { HomeComponent } from './components/home/home.component';
import { SearchGMHComponent } from './components/search-gmh/search-gmh.component';

import { DonationsComponent } from './components/donations/donations.component';
import { AddGMHComponent } from './components/add-gmh/add-gmh.component';
import { RegisterComponent } from './components/register/register.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { NeedsGMHimComponent } from './components/needs-gmhim/needs-gmhim.component';
import { OneGmhComponent } from './components/one-gmh/one-gmh.component';
import { GMHComponent } from './components/gmh/gmh.component';

import { GooglePlaceModule } from 'ngx-google-places-autocomplete';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, } from '@angular/material/core';
import { AddDonationComponent } from './components/add-donation/add-donation.component';



import { DatePipe } from '@angular/common';
import { EditOwnerDetailsComponent } from './components/edit-owner-details/edit-owner-details.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import {MatRadioModule} from '@angular/material/radio';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatTableModule} from '@angular/material/table';
import { AppComponent, dialog } from './app.component';
import { myDonationComponent } from './components/my-donation/my-donation.component';
import { AddReqestComponent } from './components/add-reqest/add-reqest.component';

import { SuitableReqestComponent } from './components/suitable-reqest/suitable-reqest.component';
import { OfferDonationComponent } from './components/offer-donation/offer-donation.component';
import { GmhListComponent } from './components/gmh-list/gmh-list.component';
import {TextFieldModule} from '@angular/cdk/text-field';
import { ManageTheGMHComponent } from './components/manage-the-gmh/manage-the-gmh.component';
import { NewGmhComponent } from './components/new-gmh/new-gmh.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SearchGMHComponent,
    ManageTheGMHComponent,
    DonationsComponent,
    AddGMHComponent,
    RegisterComponent,
    SignInComponent,
    NeedsGMHimComponent,
    GMHComponent,
    OneGmhComponent,
    AddDonationComponent,
    GmhListComponent,  
    EditOwnerDetailsComponent,
    myDonationComponent,
    dialog,
    AddReqestComponent,
    OfferDonationComponent,
    SuitableReqestComponent,
    NewGmhComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    GooglePlaceModule,
    MatAutocompleteModule,
    NoopAnimationsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule,
    MatInputModule,
    MatRadioModule,
    MatCheckboxModule,
    MatTableModule,
    TextFieldModule
  ],
  providers: [UserService, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
