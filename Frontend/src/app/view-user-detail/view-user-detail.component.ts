import { Component,OnInit } from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-user-detail',
  templateUrl: './view-user-detail.component.html',
  styleUrls: ['./view-user-detail.component.css']
})
export class ViewUserDetailComponent implements OnInit  {
  userId : any;
  userDetail : any;
  ordersDetail : any[] = [];
  status : string = '';
  ngOnInit(): void {
    this.userId = this.myService.receiveUserId();
    this.myService.userOrderDetail(this.userId).subscribe(
      (response : any[])=>{
        this.ordersDetail = response
        console.log(response)
      }
    )
    this.myService.fetchUserDetailById(this.userId).subscribe(
      (response : any)=>{
        this.userDetail = response
        console.log(response)
      }
    )
    this.status = this.myService.receiveTransactionStatus();
  }
  constructor(private myService: MyServiceService, private router : Router){}
  addStockDetail(userId : any) {
    console.log(userId)
    this.myService.sendUserId(userId)
    this.router.navigate(['/addUserStockDetails']);
  }
}
