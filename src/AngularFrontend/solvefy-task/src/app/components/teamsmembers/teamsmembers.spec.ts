import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Teamsmembers } from './teamsmembers';

describe('Teamsmembers', () => {
  let component: Teamsmembers;
  let fixture: ComponentFixture<Teamsmembers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Teamsmembers],
    }).compileComponents();

    fixture = TestBed.createComponent(Teamsmembers);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
