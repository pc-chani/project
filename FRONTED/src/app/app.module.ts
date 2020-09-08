import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
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
    OneGmhComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
