import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { CreateUserDetailComponent } from './create-user-detail/create-user-detail.component';
import { EditUserDetailComponent } from './edit-user-detail/edit-user-detail.component';
import { DeleteUserDetailComponent } from './delete-user-detail/delete-user-detail.component';
import { ViewUserDetailComponent } from './view-user-detail/view-user-detail.component';
import { AddUserStockDetailsComponent } from './add-user-stock-details/add-user-stock-details.component';
import { FetchUserDetailByIdComponent } from './fetch-user-detail-by-id/fetch-user-detail-by-id.component';
import { FetchedUserDetailComponent } from './fetched-user-detail/fetched-user-detail.component';
import { FilterUserDetailComponent } from './filter-user-detail/filter-user-detail.component';
import { TransactionComponent } from './transaction/transaction.component';
import { LoginComponent } from './login/login.component';
import { CreateBankDetailComponent } from './create-bank-detail/create-bank-detail.component';
import { DeleteBankDetailComponent } from './delete-bank-detail/delete-bank-detail.component';
import { ViewBankDetailComponent } from './view-bank-detail/view-bank-detail.component';
import { EditBankDetailComponent } from './edit-bank-detail/edit-bank-detail.component';
import { BankDetailComponent } from './bank-detail/bank-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http'
import { MyServiceService } from './my-service.service';
import { HomeComponent } from './home/home.component';
import { HomeContentComponent} from './home-content/home-content.component';
@NgModule({
  declarations: [
    AppComponent,
    UserDetailComponent,
    CreateUserDetailComponent,
    EditUserDetailComponent,
    DeleteUserDetailComponent,
    ViewUserDetailComponent,
    AddUserStockDetailsComponent,
    FetchUserDetailByIdComponent,
    FetchedUserDetailComponent,
    FilterUserDetailComponent,
    TransactionComponent,
    LoginComponent,
    CreateBankDetailComponent,
    DeleteBankDetailComponent,
    ViewBankDetailComponent,
    EditBankDetailComponent,
    BankDetailComponent,
    HomeComponent,
    HomeContentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,FormsModule,ReactiveFormsModule,HttpClientModule
  ],
  providers: [MyServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
