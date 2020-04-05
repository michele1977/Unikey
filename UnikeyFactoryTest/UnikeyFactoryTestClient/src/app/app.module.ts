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
import { SeeExTestComponent } from './features/see-ex-test/see-ex-test.component';
import {LoaderService} from './services/loader.service';
import { EmailModalComponent } from './shared/email-modal/email-modal.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {InterceptorService} from './services/interceptor.service';
import {LogoutModule} from './shared/logout/logout.module';
import { ExTestListComponent } from './features/ex-test-list/ex-test-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import {WINDOW_PROVIDERS} from './services/window-ref.service';
import { BeginTestComponent } from './features/fruition/begin-test/begin-test.component';
import { FruitionLandingPageComponent } from './shared/fruition-landing-page/fruition-landing-page.component';
import { CreatePDFModalComponent } from './modals/create-pdf-modal/create-pdf-modal.component';
import { FruitionTestComponent } from './features/fruition-test/fruition-test.component';
import { EndTestModalComponent } from './modals/end-test-modal/end-test-modal.component';
import {SideBarModule} from './core/side-bar/side-bar.module';
import {SideBarService} from './services/side-bar.service';
import {CircleProgressOptions, NgCircleProgressModule} from 'ng-circle-progress';
import {CommonModule} from '@angular/common';
import {StatisticPageComponent} from './features/statistic-page/statistic-page.component';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { TryComponent } from './core/try/try.component';
import { OrderModule } from 'ngx-order-pipe';
import { ModifyTestModalComponent } from './modals/modify-test-modal/modify-test-modal.component';
import { UserModalComponent } from './modals/user-modal/user-modal.component';

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
    FruitionTestComponent,
    EndTestModalComponent,
    FruitionLandingPageComponent,
    BeginTestComponent,
    EmailModalComponent,
    CreatePDFModalComponent,
    StatisticPageComponent,
    NotfoundComponent,
    TryComponent,
    ModifyTestModalComponent,
    UserModalComponent
  ],

  imports: [
    BrowserModule,
    CommonModule,
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
    MatButtonModule,
    SideBarModule,
    NgCircleProgressModule,
    OrderModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
    LoaderService,
    WINDOW_PROVIDERS,
    SideBarService,
    CircleProgressOptions
  ],
  exports: [
  ],
  bootstrap: [AppComponent],
  entryComponents: [TestDetailsModalComponent]
})
export class AppModule { }
