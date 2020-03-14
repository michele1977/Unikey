import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExTestListComponent } from './ex-test-list.component';

describe('ExTestListComponent', () => {
  let component: ExTestListComponent;
  let fixture: ComponentFixture<ExTestListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExTestListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExTestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
