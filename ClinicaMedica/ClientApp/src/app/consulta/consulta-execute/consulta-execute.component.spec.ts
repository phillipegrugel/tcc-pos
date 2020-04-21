import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaExecuteComponent } from './consulta-execute.component';

describe('ConsultaExecuteComponent', () => {
  let component: ConsultaExecuteComponent;
  let fixture: ComponentFixture<ConsultaExecuteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaExecuteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaExecuteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
