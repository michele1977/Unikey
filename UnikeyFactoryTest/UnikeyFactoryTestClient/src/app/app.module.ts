import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LandingPageModule} from './shared/landing-page/landing-page.module';
import {FormsModule} from '@angular/forms';
import { CreationComponent } from './features/creation/creation.component';
import {QuestionListModule} from './features/creation/question-list/question-list.module';

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
    QuestionListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
