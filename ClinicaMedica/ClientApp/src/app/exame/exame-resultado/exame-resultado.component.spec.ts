import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExameResultadoComponent } from './exame-resultado.component';

describe('ExameResultadoComponent', () => {
  let component: ExameResultadoComponent;
  let fixture: ComponentFixture<ExameResultadoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExameResultadoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExameResultadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
