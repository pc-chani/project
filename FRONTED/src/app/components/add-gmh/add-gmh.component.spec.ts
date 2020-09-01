import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGMHComponent } from './add-gmh.component';

describe('AddGMHComponent', () => {
  let component: AddGMHComponent;
  let fixture: ComponentFixture<AddGMHComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddGMHComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGMHComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
