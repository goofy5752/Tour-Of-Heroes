import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroesMainContentComponent } from './heroes-main-content.component';

describe('HeroesMainContentComponent', () => {
  let component: HeroesMainContentComponent;
  let fixture: ComponentFixture<HeroesMainContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroesMainContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroesMainContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
