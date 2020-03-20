import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FruitionTestComponent } from './fruition-test.component';

describe('FruitionTestComponent', () => {
  let component: FruitionTestComponent;
  let fixture: ComponentFixture<FruitionTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FruitionTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FruitionTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
