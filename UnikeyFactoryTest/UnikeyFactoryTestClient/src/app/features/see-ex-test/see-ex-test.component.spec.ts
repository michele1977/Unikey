import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeExTestComponent } from './see-ex-test.component';

describe('SeeExTestComponent', () => {
  let component: SeeExTestComponent;
  let fixture: ComponentFixture<SeeExTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeeExTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeeExTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
