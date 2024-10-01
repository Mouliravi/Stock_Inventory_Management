import { Component , OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { IBankDetail } from '../IBankDetail';
@Component({
  selector: 'app-edit-bank-detail',
  templateUrl: './edit-bank-detail.component.html',
  styleUrls: ['./edit-bank-detail.component.css']
})
export class EditBankDetailComponent implements OnInit {
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
  bankDetailsId : any;
  bankDetail! : any;
  ngOnInit(): void {
   this.bankDetailsId = this.myService.receiveBankDetailsId();
   console.log(this.bankDetailsId)
   this.myService.fetchBankDetailById(this.bankDetailsId).subscribe(
    (response : any)=> {
      console.log(response);
      this.bankDetail = response;
      console.log(this.bankDetail.bankName);
      this.rForm.patchValue({
        BankName: this.bankDetail.BankName,
        Ifsc : this.bankDetail.Ifsc,
        Mcir : this.bankDetail.Mcir,
        AccountNumber : response.AccountNumber,
        AccountBalance : this.bankDetail.AccountBalance
      });
    }
  )
  }

  getData() {
    // console.log(this.rForm.value);
    // this.bankDetail.BankName = this.rForm.value.BankName ?? '';
    // this.bankDetail.Ifsc = this.rForm.value.Ifsc ?? '';
    // this.bankDetail.Mcir = parseInt(this.rForm.value.Mcir ?? '0');
    // this.bankDetail.AccountNumber = parseInt(this.rForm.value.AccountNumber ?? '0');
    // this.bankDetail.AccountBalance = parseInt(this.rForm.value.AccountBalance ?? '0');
    // console.log(this.bankDetail)
    const updatedBankDetail = {
      BankDetailsId: this.bankDetail.BankDetailsId,
      BankName: this.rForm.value.BankName ?? '',
      Ifsc: this.rForm.value.Ifsc ?? '',
      Mcir: parseInt(this.rForm.value.Mcir ?? '0'),
      AccountNumber: parseInt(this.rForm.value.AccountNumber ?? '0'),
      AccountBalance: parseInt(this.rForm.value.AccountBalance ?? '0'),
      CreatedAt: this.bankDetail.CreatedAt, // Assuming you don't want to change these values
      UpdatedAt: this.bankDetail.UpdatedAt
    };
    console.log(updatedBankDetail)
    this.myService.editBankDetails(updatedBankDetail).subscribe(
      (response:any) => {
        console.log('Bank Detail edited successfully:', response);
        this.myService.sendBankDetailsId(updatedBankDetail.BankDetailsId)
        this.myService.sendTransactionStatus('Bank Details Edited');
        this.router.navigate(['/viewBankDetail'])
      },
      error => {
        console.error('Failed to edit Bank Detail:', error);
        this.myService.sendTransactionStatus('Bank Details not Edited');
        this.router.navigate(['/BankDetail'])
        // Handle error scenario
      }
    );
  }
}
