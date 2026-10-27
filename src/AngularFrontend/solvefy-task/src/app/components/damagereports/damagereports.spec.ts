import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Damagereports } from './damagereports';

describe('Damagereports', () => {
  let component: Damagereports;
  let fixture: ComponentFixture<Damagereports>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Damagereports],
    }).compileComponents();

    fixture = TestBed.createComponent(Damagereports);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
