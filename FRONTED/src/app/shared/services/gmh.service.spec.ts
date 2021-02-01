import { TestBed } from '@angular/core/testing';

import { GmhService } from './gmh.service';

describe('GmhService', () => {
  let service: GmhService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GmhService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
