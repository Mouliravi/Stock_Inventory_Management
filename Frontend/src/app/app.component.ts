import { Component,OnInit } from '@angular/core';
import { MyServiceService } from './my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title(title: any) {
    throw new Error('Method not implemented.');
  }
  noOfUserDetails : any;
  noOfBankDetails : any;
  noOfProviders : any;
  noOfBrokers : any;
  noOfStocksPurchased : any;
  constructor(private myService : MyServiceService, private router : Router ){}
  ngOnInit(): void {
    this.myService.fetchUserDetails().subscribe(
      (response:any[])=>{
        this.noOfUserDetails = response.length;
        console.log(this.noOfUserDetails)
      }
    )
    this.myService.fetchBankDetails().subscribe(
      (response:any[])=>{
        this.noOfBankDetails = response.length;
        console.log(this.noOfBankDetails)
      }
    )
    this.myService.providers().subscribe(
      (response:any[])=>{
        this.noOfProviders = response.length;
        console.log(this.noOfProviders)
      }
    )
    this.myService.brokers().subscribe(
      (response:any[])=>{
        this.noOfBrokers = response.length;
        console.log(this.noOfBrokers)
      }
    )
    this.myService.stocks().subscribe(
      (response:any[])=>{
        this.noOfStocksPurchased = response.length
        console.log(this.noOfStocksPurchased)
      }
    )
  }
}
