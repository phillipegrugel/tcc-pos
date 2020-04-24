import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PoModule } from '@portinari/portinari-ui';
import { ProfissionalModule } from './profissional/profissional.module'
import { PacienteModule } from './paciente/paciente.module';
import { RemedioModule } from './remedio/remedio.module';
import { LoginModule } from './login/login.module';
import { AuthGuard } from './shared/auth.guard';
import { AuthInterceptor } from './shared/auth.interceptor';
import { AuthService } from './shared/auth.service';
import { LoaderModule } from './shared/Components/loader/loader.module';
import { LoaderInterceptor } from './shared/loader.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    LoginModule,
    LoaderModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },
      { path: 'profissional', loadChildren: './profissional/profissional.module#ProfissionalModule', canActivate: [AuthGuard] },
      { path: 'paciente', loadChildren: './paciente/paciente.module#PacienteModule', canActivate: [AuthGuard] },
      { path: 'remedio', loadChildren: './remedio/remedio.module#RemedioModule', canActivate: [AuthGuard] },
      { path: 'consulta', loadChildren: './consulta/consulta.module#ConsultaModule', canActivate: [AuthGuard] },
      { path: 'exame', loadChildren: './exame/exame.module#ExameModule', canActivate: [AuthGuard] },
      { path: 'consulta-rapida', loadChildren: './consulta-rapida/consulta-rapida.module#ConsultaRapidaModule', canActivate: [AuthGuard] }
    ]),
    PoModule,
    RouterModule.forRoot([])
  ],
  providers: [
    AuthService,
    AuthGuard,
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
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
