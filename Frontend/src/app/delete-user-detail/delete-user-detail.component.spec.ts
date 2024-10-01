import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteUserDetailComponent } from './delete-user-detail.component';

describe('DeleteUserDetailComponent', () => {
  let component: DeleteUserDetailComponent;
  let fixture: ComponentFixture<DeleteUserDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteUserDetailComponent]
    });
    fixture = TestBed.createComponent(DeleteUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
