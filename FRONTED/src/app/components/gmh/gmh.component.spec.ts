import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GMHComponent } from './gmh.component';

describe('GMHComponent', () => {
  let component: GMHComponent;
  let fixture: ComponentFixture<GMHComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GMHComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GMHComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
