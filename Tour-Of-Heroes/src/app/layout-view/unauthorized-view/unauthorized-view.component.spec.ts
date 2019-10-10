import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorizedViewComponent } from './unauthorized-view.component';

describe('UnauthorizedViewComponent', () => {
  let component: UnauthorizedViewComponent;
  let fixture: ComponentFixture<UnauthorizedViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnauthorizedViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnauthorizedViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
