import { TestBed } from '@angular/core/testing';

import { TeamMembers } from './team-members';

describe('TeamMembers', () => {
  let service: TeamMembers;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TeamMembers);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
