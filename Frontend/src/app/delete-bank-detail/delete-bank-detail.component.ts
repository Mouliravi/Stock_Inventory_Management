import { Component,OnInit } from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-bank-detail',
  templateUrl: './delete-bank-detail.component.html',
  styleUrls: ['./delete-bank-detail.component.css']
})
export class DeleteBankDetailComponent implements OnInit {
  bankDetailsId : any;
  bankDetail : any;
  constructor(private myService : MyServiceService, private router : Router) {}
  ngOnInit(): void {
    this.bankDetailsId = this.myService.receiveBankDetailsId();
    console.log(this.bankDetailsId)
    this.myService.fetchBankDetailById(this.bankDetailsId).subscribe(
      (response : any)=>{
        console.log(response);
        this.bankDetail = response
      }
    )
  }

  deleteBankDetail(){
    console.log(this.bankDetail)
    this.myService.deleteBankDetails(this.bankDetailsId).subscribe(
      (response:any) => {
        console.log('Bank Detail deleted successfully:', response);
        this.router.navigate(['/bankDetail'])
      },
      error => {
        console.error('Failed to delete Bank Detail:', error);
        // Handle error scenario
      }
    );
  }

}
