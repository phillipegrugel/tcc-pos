import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExameListComponent } from './exame-list/exame-list.component';
import { ExameRoutingModule } from './exame-routing.module';
import { PoModule } from '@portinari/portinari-ui';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../shared/auth.interceptor';
import { FormsModule } from '@angular/forms';
import { ExameResultadoComponent } from './exame-resultado/exame-resultado.component';
import { LoaderInterceptor } from '../shared/loader.interceptor';
import { LoaderModule } from '../shared/Components/loader/loader.module';



@NgModule({
  declarations: [ExameListComponent, ExameResultadoComponent],
  imports: [
    CommonModule,
    PoModule,
    FormsModule,
    ExameRoutingModule,
    LoaderModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ]
})
export class ExameModule { }
