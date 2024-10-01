import { Component } from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { IUserDetail } from '../IUserDetail';

@Component({
  selector: 'app-fetch-user-detail-by-id',
  templateUrl: './fetch-user-detail-by-id.component.html',
  styleUrls: ['./fetch-user-detail-by-id.component.css']
})
export class FetchUserDetailByIdComponent {
  userId:any;
  code : any;
  userDetail! : any;
  constructor(private myService : MyServiceService ,private router : Router) { }
  sendUserId(event: Event): void {
    event.preventDefault(); // Prevent default form submission behavior
    this.myService.sendUserId(this.userId);
    this.myService.fetchUserDetailById(this.userId).subscribe(
      (response : any)=>{
        this.userDetail = response;
        console.log(this.userDetail);
        if(this.userDetail==null)
      {
      console.log(this.userDetail);
      this.code = 1;
      console.log(this.code);
      }
      else
      this.router.navigate(['/fetchedUserDetail']);
      }
    )

  }
}
