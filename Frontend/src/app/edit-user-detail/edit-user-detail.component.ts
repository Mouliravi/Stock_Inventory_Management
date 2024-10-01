import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { IUserDetail } from '../IUserDetail';
import { dateOfBirthValidator } from '../dateOfBirthValidator';

@Component({
  selector: 'app-edit-user-detail',
  templateUrl: './edit-user-detail.component.html',
  styleUrls: ['./edit-user-detail.component.css']
})
export class EditUserDetailComponent implements OnInit {
  rForm = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z\\s]{3,}$')]),
    DateOfBirth: new FormControl('', [Validators.required, dateOfBirthValidator()]),
    RoleId: new FormControl('', Validators.required),
    NationalityId: new FormControl('', Validators.required),
    GenderId: new FormControl('', Validators.required),
    MaritalStatusId: new FormControl('', Validators.required),
    AddressLine1: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z0-9\\s,]{8,}$')]),
    CityId: new FormControl('', Validators.required),
    StateId: new FormControl('', Validators.required),
    CountryId: new FormControl('', Validators.required),
    MobileNumber: new FormControl('', Validators.pattern('^[789]\\d{9}$')),
    Occupation: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z\\s]{3,}$')]),
    Email: new FormControl('', [Validators.email, Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$')]),
    AnnualIncome: new FormControl('', Validators.pattern('^\\d{6,}(?:\\.\\d{1,2})?$')),
    BalanceAmount: new FormControl('', Validators.pattern('^\\d{4,}(?:\\.\\d{1,2})?$'))
  });

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

  constructor(private myService : MyServiceService,private router : Router) {}
  userId : any;
  userDetail: any;
  roles : any[] = [];
  selectedRole: any;
  nationalities : any[] = [];
  selectedNationality: any = '';
  genders : any[] = [];
  selectedGender : any;
  maritalStatuses : any[] = [];
  selectedMaritalStatus : any;
  cities : any[] = [];
  selectedCity : any;
  states : any[] = [];
  selectedState : any;
  countries : any[] = [];
  selectedCountry : any = '';
  ngOnInit(): void {
    this.userId = this.myService.receiveUserId();
    this.myService.fetchUserDetailById(this.userId).subscribe(
      (response : any)=> {
        console.log(response);
        this.userDetail = response;
        console.log(this.userDetail.Country.CountryId);
        this.rForm.patchValue({
          UserName: this.userDetail.UserName,
          DateOfBirth: this.userDetail.DateOfBirth,
          RoleId: this.userDetail.Role.RoleId.toString(),
          NationalityId: this.userDetail.Nationality.NationalityId.toString(),
          GenderId: this.userDetail.Gender.GenderId.toString(),
          MaritalStatusId: this.userDetail.MaritalStatus.MaritalStatusId.toString(),
          AddressLine1: this.userDetail.AddressLine1,
          CityId: this.userDetail.City.CityId.toString(),
          StateId: this.userDetail.State.StateId.toString(),
          CountryId: this.userDetail.Country.CountryId.toString(),
          MobileNumber: this.userDetail.MobileNumber,
          Occupation: this.userDetail.Occupation,
          Email: this.userDetail.Email,
          AnnualIncome: this.userDetail.AnnualIncome.toString(),
          BalanceAmount: this.userDetail.BalanceAmount.toString()
        });
      }
    )
    this.myService.roles().subscribe(
      (response : any[])=>{
        console.log(response);
        this.roles = response;
      }
    )

    this.myService.nationalities().subscribe(
      (response : any[])=>{
        console.log(response);
        this.nationalities = response;
      }
    )


    this.myService.genders().subscribe(
      (response : any[])=>{
        console.log(response);
        this.genders = response;
      }
    )


    this.myService.maritalStatuses().subscribe(
      (response : any[])=>{
        console.log(response);
        this.maritalStatuses = response;
      }
    )


    this.myService.cities().subscribe(
      (response : any[])=>{
        console.log(response);
        this.cities = response;
      }
    )


    this.myService.states().subscribe(
      (response : any[])=>{
        console.log(response);
        this.states = response;
      }
    )


    this.myService.countries().subscribe(
      (response : any[])=>{
        console.log(response);
        this.countries = response;
      }
    )


  }

  getData() {
    console.log(this.rForm.value)
    const updatedUserDetail = {
      UserId: this.userDetail.UserId,
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
      BankDetailsId: this.userDetail.BankDetailsId, // Assuming this value is not changed
      CreatedAt: this.userDetail.CreatedAt, // Assuming you don't want to change these values
      //UpdatedAt: this.userDetail.UpdatedAt
    };
    console.log(updatedUserDetail)
    this.myService.editUserDetails(updatedUserDetail, this.userId).subscribe(
      (response:any) => {
        console.log('User Detail edited successfully:', response);
        this.myService.sendUserId(this.userId);
        this.myService.sendTransactionStatus('Edited');
        this.router.navigate(['/fetchedUserDetail'])
      },
      error => {
        console.error('Failed to delete Bank Detail:', error);
        this.myService.sendTransactionStatus('Not Edited');
        // Handle error scenario
      }
    )
  }
}
