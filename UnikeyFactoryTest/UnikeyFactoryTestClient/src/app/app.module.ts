import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {SubscribeService} from './services/subscribe.service';
import {SubscribeModule} from './core/subscribe/subscribe.module';
import {LandingPageModule} from './shared/landing-page/landing-page.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LandingPageModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
