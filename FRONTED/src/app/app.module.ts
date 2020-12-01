import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, dialog } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { UserService } from './shared/services/user.service';
import { HomeComponent } from './components/home/home.component';
import { SearchGMHComponent } from './components/search-gmh/search-gmh.component';
import { ManageTheGMHComponent } from './components/manage-the-gmh/manage-the-gmh.component';
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
import { myDonationComponent } from './components/my-donation/my-donation.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import {MatRadioModule} from '@angular/material/radio';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatTableModule} from '@angular/material/table';

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
    EditOwnerDetailsComponent,
    myDonationComponent,
    dialog
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
    MatInputModule,
    MatRadioModule,
    MatCheckboxModule,
    MatTableModule,
  ],
  providers: [UserService, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
