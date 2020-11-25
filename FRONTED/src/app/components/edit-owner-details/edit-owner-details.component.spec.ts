import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOwnerDetailsComponent } from './edit-owner-details.component';

describe('EditOwnerDetailsComponent', () => {
  let component: EditOwnerDetailsComponent;
  let fixture: ComponentFixture<EditOwnerDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditOwnerDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditOwnerDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
