import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBankDetailComponent } from './delete-bank-detail.component';

describe('DeleteBankDetailComponent', () => {
  let component: DeleteBankDetailComponent;
  let fixture: ComponentFixture<DeleteBankDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteBankDetailComponent]
    });
    fixture = TestBed.createComponent(DeleteBankDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
