import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmitirReceitaComponent } from './emitir-receita.component';

describe('EmitirReceitaComponent', () => {
  let component: EmitirReceitaComponent;
  let fixture: ComponentFixture<EmitirReceitaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmitirReceitaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmitirReceitaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
