import { Component,OnInit } from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { IUserDetail } from '../IUserDetail';

@Component({
  selector: 'app-fetched-user-detail',
  templateUrl: './fetched-user-detail.component.html',
  styleUrls: ['./fetched-user-detail.component.css']
})
export class FetchedUserDetailComponent implements OnInit {
  userId : any;
  userDetail : any;
  status : any;
  constructor(private myService : MyServiceService){}
  ngOnInit(): void {
    this.userId = this.myService.receiveUserId();
    this.status = this.myService.receiveTransactionStatus();
    this.myService.fetchUserDetailById(this.userId).subscribe(
      (response : any)=> {
        console.log(response);
        this.userDetail = response;
      }
    )
  }

}
// import { Component, OnInit } from '@angular/core';
// import { MyServiceService } from '../my-service.service';
// import { UserDetail } from '../user-detail.interface'; // Import the interface

// @Component({
//   selector: 'app-fetched-user-detail',
//   templateUrl: './fetched-user-detail.component.html',
//   styleUrls: ['./fetched-user-detail.component.css']
// })
// export class FetchedUserDetailComponent implements OnInit {
//   userId: any;
//   userDetail: UserDetail; // Use the interface here
//   constructor(private myService: MyServiceService) { }
//   ngOnInit(): void {
//     this.userId = this.myService.receiveUserId();
//     this.myService.fetchUserDetailById(this.userId).subscribe(
//       (response: UserDetail) => { // Use the interface for type checking
//         console.log(response);
//         this.userDetail = response;
//       }
//     )
//   }
// }
