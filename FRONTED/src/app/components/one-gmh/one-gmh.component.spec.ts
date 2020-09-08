import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OneGmhComponent } from './one-gmh.component';

describe('OneGmhComponent', () => {
  let component: OneGmhComponent;
  let fixture: ComponentFixture<OneGmhComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OneGmhComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OneGmhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
