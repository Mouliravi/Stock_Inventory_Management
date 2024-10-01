import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchedUserDetailComponent } from './fetched-user-detail.component';

describe('FetchedUserDetailComponent', () => {
  let component: FetchedUserDetailComponent;
  let fixture: ComponentFixture<FetchedUserDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FetchedUserDetailComponent]
    });
    fixture = TestBed.createComponent(FetchedUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
