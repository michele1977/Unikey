import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {SubscribeService} from './services/subscribe.service';
import {SubscribeModule} from './features/subscribe/subscribe.module';
import {LandingPageModule} from './shared/landing-page/landing-page.module';
import {FormsModule} from '@angular/forms';
import { CreationComponent } from './features/creation/creation.component';
import {QuestionListModule} from './features/creation/question-list/question-list.module';
import {QuestionFormModule} from './features/creation/question-form/question-form.module';

@NgModule({
  declarations: [
    AppComponent,
    CreationComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        LandingPageModule,
        FormsModule,
        QuestionListModule,
        QuestionFormModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
