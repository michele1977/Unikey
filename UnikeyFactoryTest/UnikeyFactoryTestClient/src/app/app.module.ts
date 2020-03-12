import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LandingPageModule} from './shared/landing-page/landing-page.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
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


@NgModule({
  declarations: [
    AppComponent,
    TestListComponent,
    CreationComponent,
    TestcontentComponent,
    ErrorComponent,
    TestContentEditComponent,
    TestDetailsModalComponent,
    SeeExTestComponent
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
    NgbModule
  ],
  providers: [LoaderService],
  bootstrap: [AppComponent],
  entryComponents: [TestDetailsModalComponent]
})
export class AppModule { }
