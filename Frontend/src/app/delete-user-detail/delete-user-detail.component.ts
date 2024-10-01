import { Component,OnInit } from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user-detail',
  templateUrl: './delete-user-detail.component.html',
  styleUrls: ['./delete-user-detail.component.css']
})

export class DeleteUserDetailComponent implements OnInit {
  userId : any;
  userDetail : any;
  constructor(private myService : MyServiceService, private router : Router) {}
  ngOnInit(): void {
    this.userId = this.myService.receiveUserId();
    console.log(this.userId)
    this.myService.fetchUserDetailById(this.userId).subscribe(
      (response : any)=>{
        console.log(response);
        this.userDetail = response
      }
    )
  }

  deleteUserDetail(){
    console.log(this.userDetail.BankDetailsId)
    this.myService.deleteUserDetails(this.userId).subscribe(
      (response:any) => {
        console.log('User Detail deleted successfully:', response);
        this.myService.deleteBankDetails(this.userDetail.BankDetailsId).subscribe(
          (response:any) => {
            console.log('Bank Detail deleted successfully:', response);
            this.myService.sendTransactionStatus('Deleted');
            this.router.navigate(['/userDetail'])
          },
          error => {
            console.error('Failed to delete Bank Detail:', error);
            this.myService.sendTransactionStatus('Not Deleted');
            this.router.navigate(['/userDetail'])
            // Handle error scenario
          }
        );
      },
      error => {
        console.error('Failed to delete User Detail:', error);
        this.myService.sendTransactionStatus('Not Deleted');
        this.router.navigate(['/userDetail'])
        // Handle error scenario
      }
    );

  }

}
