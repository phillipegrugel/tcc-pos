import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaRapidaComponent } from './consulta-rapida.component';

describe('ConsultaRapidaComponent', () => {
  let component: ConsultaRapidaComponent;
  let fixture: ComponentFixture<ConsultaRapidaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaRapidaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaRapidaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
