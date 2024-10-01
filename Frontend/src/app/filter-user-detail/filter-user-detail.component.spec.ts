import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterUserDetailComponent } from './filter-user-detail.component';

describe('FilterUserDetailComponent', () => {
  let component: FilterUserDetailComponent;
  let fixture: ComponentFixture<FilterUserDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FilterUserDetailComponent]
    });
    fixture = TestBed.createComponent(FilterUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
