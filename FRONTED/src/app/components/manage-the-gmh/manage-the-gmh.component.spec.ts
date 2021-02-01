import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTheGMHComponent } from './manage-the-gmh.component';

describe('ManageTheGMHComponent', () => {
  let component: ManageTheGMHComponent;
  let fixture: ComponentFixture<ManageTheGMHComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTheGMHComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTheGMHComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
