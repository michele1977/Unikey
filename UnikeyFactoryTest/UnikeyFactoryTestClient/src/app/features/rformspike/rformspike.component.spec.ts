import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RformspikeComponent } from './rformspike.component';

describe('RformspikeComponent', () => {
  let component: RformspikeComponent;
  let fixture: ComponentFixture<RformspikeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RformspikeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RformspikeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
