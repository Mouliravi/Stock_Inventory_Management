import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBankDetailComponent } from './create-bank-detail.component';

describe('CreateBankDetailComponent', () => {
  let component: CreateBankDetailComponent;
  let fixture: ComponentFixture<CreateBankDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBankDetailComponent]
    });
    fixture = TestBed.createComponent(CreateBankDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
