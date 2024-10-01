import { Component , OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { IUserDetail } from '../IUserDetail';
import { dateOfBirthValidator } from '../dateOfBirthValidator';

@Component({
  selector: 'app-create-user-detail',
  templateUrl: './create-user-detail.component.html',
  styleUrls: ['./create-user-detail.component.css']
})

export class CreateUserDetailComponent implements OnInit {
  rForm = new FormGroup({
    UserName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z\\s]{3,}$')]),
    DateOfBirth: new FormControl('', [Validators.required, this.validateDOB]),
    RoleId: new FormControl('', Validators.required),
    NationalityId: new FormControl('', Validators.required),
    GenderId: new FormControl('', Validators.required),
    MaritalStatusId: new FormControl('', Validators.required),
    AddressLine1: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z0-9\\s,]{8,}$')]),
    CityId: new FormControl('', Validators.required),
    StateId: new FormControl('', Validators.required),
    CountryId: new FormControl('', Validators.required),
    MobileNumber: new FormControl('', [ Validators.required, Validators.pattern('^[789]\\d{9}$')] ),
    Occupation: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z\\s]{3,}$')]),
    Email: new FormControl('', [Validators.required , Validators.email, Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$')]),
    AnnualIncome: new FormControl('', [Validators.required, Validators.pattern('^\\d{6,}(?:\\.\\d{1,2})?$')]),
    BalanceAmount: new FormControl('', [Validators.required , Validators.pattern('^\\d{4,}(?:\\.\\d{1,2})?$')])
  });

  validateDOB(control:AbstractControl){
    const selectedDate = new Date(control.value);
    const today = new Date();
    const minAge = new Date(today.getFullYear()-18,today.getMonth(),today.getDate());
    return selectedDate <= minAge? null : {minAge:true};
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

  constructor(private myService : MyServiceService, private router : Router) {}
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
    console.log(this.rForm.value)
     // console.log(requestData);
     this.myService.sendUserDetail(this.rForm.value);
     this.router.navigate(['/createBankDetail']);
    }
}
