import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserStockDetailsComponent } from './add-user-stock-details.component';

describe('AddUserStockDetailsComponent', () => {
  let component: AddUserStockDetailsComponent;
  let fixture: ComponentFixture<AddUserStockDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddUserStockDetailsComponent]
    });
    fixture = TestBed.createComponent(AddUserStockDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
