import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddReqestComponent } from './add-reqest.component';

describe('AddReqestComponent', () => {
  let component: AddReqestComponent;
  let fixture: ComponentFixture<AddReqestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddReqestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddReqestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
