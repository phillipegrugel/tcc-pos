import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemedioFormComponent } from './remedio-form.component';

describe('RemedioFormComponent', () => {
  let component: RemedioFormComponent;
  let fixture: ComponentFixture<RemedioFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemedioFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemedioFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
