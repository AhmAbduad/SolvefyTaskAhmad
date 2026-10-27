import { TestBed } from '@angular/core/testing';

import { EquipmentHistory } from './equipment-history';

describe('EquipmentHistory', () => {
  let service: EquipmentHistory;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EquipmentHistory);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
