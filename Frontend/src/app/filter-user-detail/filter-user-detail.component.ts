import { Component, OnInit } from '@angular/core';
import { MyServiceService } from '../my-service.service';

@Component({
  selector: 'app-filter-user-detail',
  templateUrl: './filter-user-detail.component.html',
  styleUrls: ['./filter-user-detail.component.css']
})
export class FilterUserDetailComponent implements OnInit {
  showAnnualIncomeForm: boolean = false;
  showAgeForm: boolean = false;
  showStateForm: boolean = false;
  showGenderForm: boolean = false;
  showMaritalStatusForm: boolean = false;

  annualIncome: any;
  age: any;
  selectedState = 1;
  selectedGender = 1;
  selectedMaritalStatus = 1;

  state:any;
  maritalStatus:any;
  gender:any;

  filteredByAnnualIncome : any[] = []
  filteredByAge : any[] = []
  filteredByState : any[] = []
  filteredByGender : any[] = []
  filteredByMaritalStatus : any[] = []

  genders: any[] = [];
  maritalStatuses: any[] = [];
  states: any[] = [];

  constructor(private myService: MyServiceService) { }

  ngOnInit(): void {
    this.myService.genders().subscribe(
      (response: any[]) => {
        console.log(response)
        this.genders = response;
      }
    );

    this.myService.maritalStatuses().subscribe(
      (response: any[]) => {
        console.log(response)
        this.maritalStatuses = response;
      }
    );

    this.myService.states().subscribe(
      (response: any[]) => {
        console.log(response)
        this.states = response;
      }
    );
  }

  showForm(option: string) {
    // Reset all form display states
    this.showAnnualIncomeForm = false;
    this.showAgeForm = false;
    this.showStateForm = false;
    this.showGenderForm = false;
    this.showMaritalStatusForm = false;

    this.filteredByAnnualIncome = [];
    this.filteredByAge = [];
    this.filteredByState = [];
    this.filteredByGender = [];
    this.filteredByMaritalStatus = [];

    // Show the form corresponding to the clicked option
    switch (option) {
      case 'annualIncome':
        this.showAnnualIncomeForm = true;
        break;
      case 'age':
        this.showAgeForm = true;
        break;
      case 'state':
        this.showStateForm = true;
        break;
      case 'gender':
        this.showGenderForm = true;
        break;
      case 'maritalStatus':
        this.showMaritalStatusForm = true;
        break;
      default:
        break;
    }
  }

  getStateNameById(stateId: number): string {
    const state = this.states.find(s => s.stateId === stateId);
    return state ? state.state : 'Unknown';
  }


  filterUsers(option: string, inputData: any) {
    console.log('Filter option:', option);
    console.log('Input data:', inputData);

    // Process the filtered data based on the selected option and input data
    switch (option) {
      case 'annualIncome':
        // Call service method to filter users by annual income
        this.myService.filterByAnnualIncome(inputData).subscribe(
          (response : any)=>{
            console.log(response)
            this.filteredByAnnualIncome = response
          }
        )
        console.log('Filtering by annual income:', inputData);
        break;
      case 'age':
        // Call service method to filter users by age
        this.myService.filterByAge(inputData).subscribe(
          (response : any)=>{
            console.log(response)
            this.filteredByAge = response
          }
        )
        console.log('Filtering by age:', inputData);
        break;
      case 'state':
        // Call service method to filter users by state
        this.myService.filterByState(inputData).subscribe(
          (response : any)=>{
            console.log(response)
            this.filteredByState = response
            this.state = response[0].State.State
          }
        )
        console.log('Filtering by state:', inputData);
        break;
      case 'gender':
        // Call service method to filter users by gender
        this.myService.filterByGender(inputData).subscribe(
          (response : any)=>{
            console.log(response)
            this.filteredByGender = response
            this.gender=response[0].Gender.Gender
          }
        )
        console.log('Filtering by gender:', inputData);
        break;
      case 'maritalStatus':
        // Call service method to filter users by marital status
        this.myService.filterByMaritalStatus(inputData).subscribe(
          (response : any)=>{
            console.log(response)
            this.filteredByMaritalStatus = response
            this.maritalStatus = response[0].MaritalStatus.MaritalStatus
          }
        )
        console.log('Filtering by marital status:', inputData);
        break;
      default:
        break;
    }

    // Reset form display states after filtering
    this.showAnnualIncomeForm = false;
    this.showAgeForm = false;
    this.showStateForm = false;
    this.showGenderForm = false;
    this.showMaritalStatusForm = false;
  }
}
