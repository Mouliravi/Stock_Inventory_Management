import { Component ,OnInit ,OnDestroy} from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { IBankDetail } from '../IBankDetail';
@Component({
  selector: 'app-view-bank-detail',
  templateUrl: './view-bank-detail.component.html',
  styleUrls: ['./view-bank-detail.component.css']
})
export class ViewBankDetailComponent implements OnInit , OnDestroy {
  bankDetailsId : any;
  bankDetail : any;
  userDetails : any[] = [];
  userDetail : any;
  status!: string;
  constructor(private myService : MyServiceService){}
  ngOnDestroy(): void {
    this.status = '';
  }
  ngOnInit(): void {
    this.bankDetailsId = this.myService.receiveBankDetailsId();
    this.myService.fetchBankDetailById(this.bankDetailsId).subscribe(
      (response : any)=>{
        this.bankDetail = response;
        console.log(response);
      }
    )
    this.myService.fetchUserDetails().subscribe(
      (response : any[])=>{
        this.userDetails = response;
        //console.log(response)
      }
    )

    for(let user of this.userDetails){
      // if(user.BankDetailsId == this.bankDetailsId){
      //   this.userDetail = user;
      // }
      console.log(user)
    }

    this.status = this.myService.receiveTransactionStatus();

  }

}
