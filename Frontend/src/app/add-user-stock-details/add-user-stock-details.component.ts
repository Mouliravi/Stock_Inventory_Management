import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-user-stock-details',
  templateUrl: './add-user-stock-details.component.html',
  styleUrls: ['./add-user-stock-details.component.css']
})

export class AddUserStockDetailsComponent implements OnInit{
  investmentTypes : any[] =[]
  providers : any[] =[]
  brokers : any[] = []
  userId : any
  rForm = new FormGroup({
    ProviderId: new FormControl('', Validators.required),
    BrokerId: new FormControl('', Validators.required),
    PurchasedQuantity: new FormControl('', Validators.required),
    InvestmentTypeId: new FormControl('', Validators.required)
  });

  // Individual getters for each form control
  get ProviderId() {
    return this.rForm.get('ProviderId');
  }

  get BrokerId() {
    return this.rForm.get('BrokerId');
  }

  get PurchasedQuantity() {
    return this.rForm.get('PurchasedQuantity');
  }

  get InvestmentTypeId() {
    return this.rForm.get('InvestmentTypeId');
  }

  constructor(private myService : MyServiceService, private router : Router) {}
  ngOnInit(): void {
    this.userId = this.myService.receiveUserId();

    this.myService.brokers().subscribe(
      (response : any[])=>{
        this.brokers = response
        console.log(response)
      }
    )
    this.myService.providers().subscribe(
      (response : any[])=>{
        this.providers = response
        console.log(response)
      }
    )
    this.myService.investmentTypes().subscribe(
      (response : any[])=>{
        this.investmentTypes = response
        console.log(response)
      }
    )
  }

  addStockDetails() {
    console.log(this.rForm.value);
    this.myService.addStockDetail(this.userId,this.rForm.value).subscribe(
      (Response: any) => {
        console.log('Stock Detail added successfully:', Response);
        this.myService.sendUserId(this.userId);
        this.myService.sendTransactionStatus('Stock Details Added');
        this.router.navigate(['/viewUserDetail']);
      },
      error => {
        console.error('Failed to add User Detail:', error);
        this.router.navigate(['/viewUserDetail']);
        // Handle error scenario
      }
    )
  }
}
