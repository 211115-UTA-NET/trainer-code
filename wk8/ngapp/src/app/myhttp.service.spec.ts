import { TestBed } from '@angular/core/testing';

import { MyhttpService } from './myhttp.service';

describe('MyhttpService', () => {
  let service: MyhttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyhttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
