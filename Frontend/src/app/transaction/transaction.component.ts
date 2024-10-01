import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { dateOfBirthValidator } from '../dateOfBirthValidator';


@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  rForm: FormGroup;

  constructor(private fb: FormBuilder, private myService : MyServiceService, private router : Router) {
    this.rForm = this.fb.group({
      UserName: ['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]{3,}$/)]],
      DateOfBirth: ['', [Validators.required, dateOfBirthValidator()]],
      RoleId: ['', Validators.required],
      NationalityId: ['', Validators.required],
      GenderId: ['', Validators.required],
      MaritalStatusId: ['', Validators.required],
      AddressLine1: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9\s,]{8,}$/)]],
      CityId: ['', Validators.required],
      StateId: ['', Validators.required],
      CountryId: ['', Validators.required],
      MobileNumber: ['', Validators.pattern(/^[789]\d{9}$/)],
      Occupation: ['', Validators.pattern(/^[a-zA-Z\s]{3,}$/)],
      Email: ['', [Validators.email, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)]],
      AnnualIncome: ['', Validators.pattern(/^\d{6,}(?:\.\d{1,2})?$/)],
      BalanceAmount: ['', Validators.pattern(/^\d{4,}(?:\.\d{1,2})?$/)],
      BankName: ['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]{3,}$/)]],
      Ifsc: ['', [Validators.required, Validators.pattern(/^\w{4}\d{7}$/)]],
      Mcir: ['', Validators.required],
      AccountNumber: ['', [Validators.required, Validators.pattern(/^\d{6,}$/)]],
      AccountBalance: ['', [Validators.required, Validators.pattern(/^\d{4,}$/)]]
    });
  }

  roles : any[] = [];
  nationalities : any[] = [];
  genders : any[] = [];
  maritalStatuses : any[] = [];
  cities : any[] = [];
  states : any[] = [];
  countries : any[] = [];
  ngOnInit(): void {

    this.myService.roles().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.roles = response;
        this.rForm.patchValue({
          RoleId: response.length > 0 ? response[0].roleId : null
        });
      }
    )

    this.myService.nationalities().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.nationalities = response;
        this.rForm.patchValue({
          NationalityId: response.length > 0 ? response[0].nationalityId : null
        });
      }
    )

    this.myService.genders().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.genders = response;
        this.rForm.patchValue({
          GenderId: response.length > 0 ? response[0].genderId : null
        });
      }
    )

    this.myService.maritalStatuses().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.maritalStatuses = response;
        this.rForm.patchValue({
          MaritalStatusId: response.length > 0 ? response[0].maritalStatusId: null
        });
      }
    )

    this.myService.cities().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.cities = response;
        this.rForm.patchValue({
          CityId : response.length > 0 ? response[0].cityId : null
        });
      }
    )

    this.myService.states().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.states = response;
        this.rForm.patchValue({
          StateId : response.length > 0 ? response[0].stateId : null
        });
      }
    )

    this.myService.countries().subscribe(
      (response : any[])=>{
        // console.log(response);
        this.countries = response;
        this.rForm.patchValue({
          CountryId: response.length > 0 ? response[0].countryId : null
        });
      }
    )
  }
  getData() {
    this.myService.addBankDetail(this.rForm.value).subscribe(
      (response: any) => {
        const userDetail = {
          UserName: this.rForm.value.UserName ?? '',
          DateOfBirth: this.rForm.value.DateOfBirth ?? '',
          RoleId: parseInt(this.rForm.value.RoleId ?? '0'),
          NationalityId: parseInt(this.rForm.value.NationalityId ?? '0'),
          GenderId: parseInt(this.rForm.value.GenderId ?? '0'),
          MaritalStatusId: parseInt(this.rForm.value.MaritalStatusId ?? '0'),
          AddressLine1: this.rForm.value.AddressLine1 ?? '',
          CityId: parseInt(this.rForm.value.CityId ?? '0'),
          StateId: parseInt(this.rForm.value.StateId ?? '0'),
          CountryId: parseInt(this.rForm.value.CountryId ?? '0'),
          MobileNumber: this.rForm.value.MobileNumber ?? '',
          Occupation: this.rForm.value.Occupation ?? '',
          Email: this.rForm.value.Email ?? '',
          AnnualIncome: parseInt(this.rForm.value.AnnualIncome ?? '0'),
          BalanceAmount: parseInt(this.rForm.value.BalanceAmount ?? '0'),
          BankDetailsId:response.id , // Assuming this value is not changed
        };
        console.log(userDetail.BankDetailsId);
        console.log('Bank Detail added successfully:', response);

        // Move the code to add user details here
        this.myService.addUserDetail(userDetail).subscribe(
          (userResponse: any) => {
            console.log('User Detail added successfully:', userResponse);
            this.router.navigate(['/userDetail']);
          },
          error => {
            console.error('Failed to add User Detail:', error);
            // Handle error scenario
          }
        );
      },
      error => {
        console.error('Failed to add user:', error);
        // Handle error scenario
      }
    );
  }

  // Individual getters for each form control
  get UserName() {
    return this.rForm.get('UserName');
  }

  get DateOfBirth() {
    return this.rForm.get('DateOfBirth');
  }

  get RoleId() {
    return this.rForm.get('RoleId');
  }

  get NationalityId() {
    return this.rForm.get('NationalityId');
  }

  get GenderId() {
    return this.rForm.get('GenderId');
  }

  get MaritalStatusId() {
    return this.rForm.get('MaritalStatusId');
  }

  get AddressLine1() {
    return this.rForm.get('AddressLine1');
  }

  get CityId() {
    return this.rForm.get('CityId');
  }

  get StateId() {
    return this.rForm.get('StateId');
  }

  get CountryId() {
    return this.rForm.get('CountryId');
  }

  get MobileNumber() {
    return this.rForm.get('MobileNumber');
  }

  get Occupation() {
    return this.rForm.get('Occupation');
  }

  get Email() {
    return this.rForm.get('Email');
  }

  get AnnualIncome() {
    return this.rForm.get('AnnualIncome');
  }

  get BalanceAmount() {
    return this.rForm.get('BalanceAmount');
  }

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
}
