import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestDetailsModalComponent } from './test-details-modal.component';

describe('TestDetailsModalComponent', () => {
  let component: TestDetailsModalComponent;
  let fixture: ComponentFixture<TestDetailsModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestDetailsModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
