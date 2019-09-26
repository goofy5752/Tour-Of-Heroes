import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHeroMainComponent } from './add-hero-main.component';

describe('AddHeroMainComponent', () => {
  let component: AddHeroMainComponent;
  let fixture: ComponentFixture<AddHeroMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddHeroMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddHeroMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
