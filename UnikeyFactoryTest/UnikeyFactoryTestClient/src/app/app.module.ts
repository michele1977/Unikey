import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LandingPageModule} from './shared/landing-page/landing-page.module';
import {FormsModule} from '@angular/forms';
import { CreationComponent } from './features/creation/creation.component';
import {QuestionListModule} from './features/creation/question-list/question-list.module';
import { TestListComponent } from './features/test-list/test-list.component';
import {Ng2SearchPipeModule} from 'ng2-search-filter';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import { LoginComponent } from './features/login/login.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {InterceptorService} from './services/interceptor.service';


@NgModule({
  declarations: [
    AppComponent,
    TestListComponent,
    CreationComponent,
    TestcontentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LandingPageModule,
    FormsModule,
    QuestionListModule,
    Ng2SearchPipeModule,
    FontAwesomeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
