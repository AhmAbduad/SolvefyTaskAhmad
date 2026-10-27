import { TestBed } from '@angular/core/testing';

import { DamageReport } from './damage-report';

describe('DamageReport', () => {
  let service: DamageReport;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DamageReport);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
