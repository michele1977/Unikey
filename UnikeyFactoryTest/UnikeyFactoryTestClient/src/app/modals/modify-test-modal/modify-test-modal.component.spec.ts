import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyTestModalComponent } from './modify-test-modal.component';

describe('ModifyTestModalComponent', () => {
  let component: ModifyTestModalComponent;
  let fixture: ComponentFixture<ModifyTestModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModifyTestModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyTestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
