import { TestBed } from '@angular/core/testing';

import { MyEquipment } from './my-equipment';

describe('MyEquipment', () => {
  let service: MyEquipment;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyEquipment);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
