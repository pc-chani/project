import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewGmhComponent } from './new-gmh.component';

describe('NewGmhComponent', () => {
  let component: NewGmhComponent;
  let fixture: ComponentFixture<NewGmhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewGmhComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewGmhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
