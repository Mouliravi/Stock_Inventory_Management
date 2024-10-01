import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
import { HomeComponent } from './home/home.component';
import { HomeContentComponent } from './home-content/home-content.component';


const routes: Routes = [
  {path:'',component:HomeComponent,children:[ {path:'userDetail',component:UserDetailComponent},
  {path:'createUserDetail',component:CreateUserDetailComponent},
  {path:'editUserDetail',component:EditUserDetailComponent},
  {path:'deleteUserDetail',component:DeleteUserDetailComponent},
  {path:'viewUserDetail',component:ViewUserDetailComponent},
  {path:'addUserStockDetails',component:AddUserStockDetailsComponent},
  {path:'userDetail/fetchById',component:FetchUserDetailByIdComponent},
  {path:'fetchedUserDetail',component:FetchedUserDetailComponent},
  {path:'userDetail/filter',component:FilterUserDetailComponent},
  {path:'transaction',component:TransactionComponent},
  {path:'createBankDetail',component:CreateBankDetailComponent},
  {path:'deleteBankDetail',component:DeleteBankDetailComponent},
  {path:'viewBankDetail',component:ViewBankDetailComponent},
  {path:'editBankDetail',component:EditBankDetailComponent},
  {path:'bankDetail',component:BankDetailComponent},
  {path:'',component:HomeContentComponent}]},
  {path:'login',component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
