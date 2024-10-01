import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBankDetailComponent } from './edit-bank-detail.component';

describe('EditBankDetailComponent', () => {
  let component: EditBankDetailComponent;
  let fixture: ComponentFixture<EditBankDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBankDetailComponent]
    });
    fixture = TestBed.createComponent(EditBankDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
