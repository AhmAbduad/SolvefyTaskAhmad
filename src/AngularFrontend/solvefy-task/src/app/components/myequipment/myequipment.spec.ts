import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Myequipment } from './myequipment';

describe('Myequipment', () => {
  let component: Myequipment;
  let fixture: ComponentFixture<Myequipment>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Myequipment],
    }).compileComponents();

    fixture = TestBed.createComponent(Myequipment);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
