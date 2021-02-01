import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GmhListComponent } from './gmh-list.component';

describe('GmhListComponent', () => {
  let component: GmhListComponent;
  let fixture: ComponentFixture<GmhListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GmhListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GmhListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
