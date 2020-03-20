import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LandingPageModule} from './shared/landing-page/landing-page.module';
import {ReactiveFormsModule, FormsModule} from '@angular/forms';
import { CreationComponent } from './features/creation/creation.component';
import {QuestionListModule} from './features/creation/question-list/question-list.module';
import {QuestionFormModule} from './features/creation/question-form/question-form.module';
import { TestListComponent } from './features/test-list/test-list.component';
import {Ng2SearchPipeModule} from 'ng2-search-filter';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import { TestContentEditComponent } from './features/test-list/testcontent/testcontent-edit/testcontent-edit.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {TestDetailsModalComponent} from './modals/test-details-modal/test-details-modal.component';
import {RouterModule} from '@angular/router';
import { SeeExTestComponent } from './features/see-ex-test/see-ex-test.component';
import {LoaderService} from './services/loader.service';
import { EmailModalComponent } from './shared/email-modal/email-modal.component';
import { LoginComponent } from './features/login/login.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {InterceptorService} from './services/interceptor.service';
import { LogoutComponent } from './shared/logout/logout.component';
import {LogoutModule} from './shared/logout/logout.module';
import { ExTestListComponent } from './features/ex-test-list/ex-test-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import {WINDOW_PROVIDERS} from './services/window-ref.service';
import { BeginTestComponent } from './features/fruition/begin-test/begin-test.component';
import { FruitionLandingPageComponent } from './shared/fruition-landing-page/fruition-landing-page.component';
import { CreatePDFModalComponent } from './modals/create-pdf-modal/create-pdf-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    TestListComponent,
    CreationComponent,
    TestcontentComponent,
    ErrorComponent,
    TestContentEditComponent,
    TestDetailsModalComponent,
    SeeExTestComponent,
    ExTestListComponent,
    EmailModalComponent,
    BeginTestComponent,
    FruitionLandingPageComponent,
    BeginTestComponent
    EmailModalComponent,
    CreatePDFModalComponent
  ],

  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    LandingPageModule,
    FormsModule,
    QuestionListModule,
    Ng2SearchPipeModule,
    FontAwesomeModule,
    QuestionFormModule,
    NgbModule,
    LogoutModule,
    MatDialogModule,
    MatButtonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
    LoaderService,
    WINDOW_PROVIDERS
  ],
  exports: [
  ],
  bootstrap: [AppComponent],
  entryComponents: [TestDetailsModalComponent]
})
export class AppModule { }
