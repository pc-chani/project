import { TestBed } from '@angular/core/testing';

import { ReqestService } from './reqest.service';

describe('ReqestService', () => {
  let service: ReqestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReqestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
