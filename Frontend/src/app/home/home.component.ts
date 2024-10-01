import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

interface RouteMapping {
  [url: string]: { home: boolean; userDetail: boolean; bankDetail: boolean };
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  routeMapping: RouteMapping = {
    '/': { home: true, userDetail: false, bankDetail: false },
    '/userDetail': { home: false, userDetail: true, bankDetail: false },
    '/createUserDetail': { home: false, userDetail: true, bankDetail: false },
    '/deleteUserDetail': { home: false, userDetail: true, bankDetail: false },
    '/viewUserDetail': { home: false, userDetail: true, bankDetail: false },
    '/addUserStockDetails': { home: false, userDetail: true, bankDetail: false },
    '/userDetail/fetchById': { home: false, userDetail: true, bankDetail: false },
    '/fetchedUserDetail': { home: false, userDetail: true, bankDetail: false },
    '/userDetail/filter': { home: false, userDetail: true, bankDetail: false },
    '/transaction': { home: false, userDetail: true, bankDetail: false },
    '/createBankDetail': { home: false, userDetail: false, bankDetail: true },
    '/deleteBankDetail': { home: false, userDetail: false, bankDetail: true },
    '/viewBankDetail': { home: false, userDetail: false, bankDetail: true },
    '/editBankDetail': { home: false, userDetail: false, bankDetail: true },
    '/bankDetail': { home: false, userDetail: false, bankDetail: true },
  };

  home: boolean = false;
  userDetail: boolean = false;
  bankDetail: boolean = false;

  constructor(private router: Router) {}

  ngOnInit() {
    // Initialize component state based on current URL
    this.onRouteChange(this.router.url);

    // Subscribe to router events for future route changes
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.onRouteChange(event.url);
      });
  }

  onRouteChange(url: string) {
    const routeState = this.routeMapping[url];
    if (routeState) {
      this.home = routeState.home;
      this.userDetail = routeState.userDetail;
      this.bankDetail = routeState.bankDetail;
    }
  }
}
