import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchUserDetailByIdComponent } from './fetch-user-detail-by-id.component';

describe('FetchUserDetailByIdComponent', () => {
  let component: FetchUserDetailByIdComponent;
  let fixture: ComponentFixture<FetchUserDetailByIdComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FetchUserDetailByIdComponent]
    });
    fixture = TestBed.createComponent(FetchUserDetailByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
