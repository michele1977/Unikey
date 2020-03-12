import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestContentEditComponent } from './testcontent-edit.component';

describe('TestcontentEditComponent', () => {
  let component: TestContentEditComponent;
  let fixture: ComponentFixture<TestContentEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestContentEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestContentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
