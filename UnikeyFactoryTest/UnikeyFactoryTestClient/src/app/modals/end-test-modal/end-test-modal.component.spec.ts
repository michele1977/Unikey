import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EndTestModalComponent } from './end-test-modal.component';

describe('EndTestModalComponent', () => {
  let component: EndTestModalComponent;
  let fixture: ComponentFixture<EndTestModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EndTestModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EndTestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
