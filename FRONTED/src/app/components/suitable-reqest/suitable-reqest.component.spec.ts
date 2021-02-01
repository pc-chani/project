import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuitableReqestComponent } from './suitable-reqest.component';

describe('SuitableReqestComponent', () => {
  let component: SuitableReqestComponent;
  let fixture: ComponentFixture<SuitableReqestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuitableReqestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuitableReqestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
