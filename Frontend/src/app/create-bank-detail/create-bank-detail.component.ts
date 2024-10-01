import { IBankDetail } from './../IBankDetail';
import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-bank-detail',
  templateUrl: './create-bank-detail.component.html',
  styleUrls: ['./create-bank-detail.component.css']
})
export class CreateBankDetailComponent implements OnInit {
  rForm = new FormGroup({
    BankName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z\\s]{3,}$')]),
    Ifsc: new FormControl('', [Validators.required, Validators.pattern('^[A-Z]{4}0\\d{6}$')]),
    Mcir: new FormControl('', [Validators.required, Validators.pattern('^\\d{11}$')]),
    AccountNumber: new FormControl('', [Validators.required, Validators.pattern('^\\d{11}$')]),
    AccountBalance: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{4,7}$')])
  });

  // Individual getters for each form control
  get BankName() {
    return this.rForm.get('BankName');
  }

  get Ifsc() {
    return this.rForm.get('Ifsc');
  }

  get Mcir() {
    return this.rForm.get('Mcir');
  }

  get AccountNumber() {
    return this.rForm.get('AccountNumber');
  }

  get AccountBalance() {
    return this.rForm.get('AccountBalance');
  }

  constructor(private myService : MyServiceService, private router : Router) {}
  userDetail : any;
  ngOnInit(): void {
    this.userDetail = this.myService.receiveUserDetail();
  }



  // getData() {

  //   this.myService.addBankDetail(this.rForm.value).subscribe(
  //     (response:any) => {
  //       this.userDetail.BankDetailsId = response.id;
  //       console.log(this.userDetail.BankDetailsId)
  //       console.log('Bank Detail added successfully:', response);

  //       this.router.navigate(['/userDetail'])
  //     },
  //     error => {
  //       console.error('Failed to add user:', error);
  //       // Handle error scenario
  //     }
  //   );
  //   console.log(this.userDetail)
  //   this.myService.addUserDetail(this.userDetail).subscribe(
  //     (response:any) => {
  //       console.log(this.userDetail.BankDetailsId)
  //       console.log('User Detail added successfully:', response);
  //       this.router.navigate(['/userDetail'])
  //     },
  //     error => {
  //       console.error('Failed to add User Detail:', error);
  //       // Handle error scenario
  //     }
  //   );
  // }
  getData() {
    this.myService.addBankDetail(this.rForm.value).subscribe(
      (response: any) => {
        this.userDetail.BankDetailsId = response.id;
        console.log(this.userDetail.BankDetailsId);
        console.log('Bank Detail added successfully:', response);

        // Move the code to add user details here
        this.myService.addUserDetail(this.userDetail).subscribe(
          (userResponse: any) => {
            console.log('User Detail added successfully:', userResponse);
            this.myService.sendTransactionStatus('Added');
            this.router.navigate(['/userDetail']);
          },
          error => {
            console.error('Failed to add User Detail:', error);
            this.myService.sendTransactionStatus('Not Added');
            this.router.navigate(['/userDetail']);
            // Handle error scenario
          }
        );
      },
      error => {
        console.error('Failed to add user:', error);
        this.myService.sendTransactionStatus('Not Added');
        this.router.navigate(['/userDetail']);
        // Handle error scenario
      }
    );
  }

}
