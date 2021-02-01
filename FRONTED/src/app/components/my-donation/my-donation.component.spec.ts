import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDonationComponent } from './my-donation.component';

describe('DeleteDonationComponent', () => {
  let component: DeleteDonationComponent;
  let fixture: ComponentFixture<DeleteDonationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteDonationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDonationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
