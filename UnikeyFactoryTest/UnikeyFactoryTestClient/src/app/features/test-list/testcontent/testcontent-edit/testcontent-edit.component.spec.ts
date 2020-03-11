import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestcontentEditComponent } from './testcontent-edit.component';

describe('TestcontentEditComponent', () => {
  let component: TestcontentEditComponent;
  let fixture: ComponentFixture<TestcontentEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestcontentEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestcontentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
