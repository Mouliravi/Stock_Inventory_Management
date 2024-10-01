import { Component, OnInit } from '@angular/core';
import { IUserDetail } from '../IUserDetail';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  userDetails : any[] = [];
  states : any[] = [];
  status : any;

  constructor(private myService : MyServiceService, private router : Router) { }

  ngOnInit(): void {
    this.myService.fetchUserDetails().subscribe(
      (response : any[] )=>{
        this.userDetails = response;
        console.log(this.userDetails);
      }
    )

    this.myService.states().subscribe(
      (response : any[])=>{
        console.log(response);
        this.states = response;
      }
    )

    this.status = this.myService.receiveTransactionStatus();
  }

  getStateName(stateId: number): string {
    const state = this.states.find(state => state.stateId === stateId);
    return state ? state.state : '';
  }
  editUserDetail(userId : any){
    this.myService.sendUserId(userId);
    this.router.navigate(['/editUserDetail']);
  }
  viewUserDetail(userId : any){
    this.myService.sendUserId(userId);
    this.router.navigate(['/viewUserDetail']);
  }
  deleteUserDetail(userId : any){
    this.myService.sendUserId(userId);
    this.router.navigate(['/deleteUserDetail']);
  }

}
