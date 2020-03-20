import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FruitionLandingPageComponent } from './fruition-landing-page.component';

describe('FruitionLandingPageComponent', () => {
  let component: FruitionLandingPageComponent;
  let fixture: ComponentFixture<FruitionLandingPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FruitionLandingPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FruitionLandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
