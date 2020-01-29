import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemedioListComponent } from './remedio-list.component';

describe('RemedioListComponent', () => {
  let component: RemedioListComponent;
  let fixture: ComponentFixture<RemedioListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemedioListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemedioListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
