import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchGMHComponent } from './search-gmh.component';

describe('SearchGMHComponent', () => {
  let component: SearchGMHComponent;
  let fixture: ComponentFixture<SearchGMHComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchGMHComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchGMHComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
