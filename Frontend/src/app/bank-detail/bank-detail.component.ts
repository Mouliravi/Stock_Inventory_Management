import { Component , OnInit} from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { IUserDetail } from '../IUserDetail';

@Component({
  selector: 'app-bank-detail',
  templateUrl: './bank-detail.component.html',
  styleUrls: ['./bank-detail.component.css']
})
export class BankDetailComponent implements OnInit {
  bankDetails : any[] = [];
  status! : string;
  constructor(private myService : MyServiceService, private router : Router) { }
  ngOnInit(): void {
    this.myService.fetchBankDetails().subscribe(
      (response : any[])=>{
        this.bankDetails = response;
        console.log(response)
      }
    )
    this.status = this.myService.receiveTransactionStatus();
  }

  editBankDetail(bankDetailsId : any){
    this.myService.sendBankDetailsId(bankDetailsId);
    this.router.navigate(['/editBankDetail']);
  }

  viewBankDetail(bankDetailsId : any){
    this.myService.sendBankDetailsId(bankDetailsId);
    this.router.navigate(['/viewBankDetail']);
  }

  deleteBankDetail(bankDetailsId : any){
    this.myService.sendBankDetailsId(bankDetailsId);
    this.router.navigate(['/deleteBankDetail']);
  }
}




