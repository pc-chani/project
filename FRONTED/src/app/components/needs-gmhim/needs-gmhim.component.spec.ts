import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NeedsGMHimComponent } from './needs-gmhim.component';

describe('NeedsGMHimComponent', () => {
  let component: NeedsGMHimComponent;
  let fixture: ComponentFixture<NeedsGMHimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NeedsGMHimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NeedsGMHimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
