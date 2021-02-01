import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferDonationComponent } from './offer-donation.component';

describe('OfferDonationComponent', () => {
  let component: OfferDonationComponent;
  let fixture: ComponentFixture<OfferDonationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OfferDonationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OfferDonationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
