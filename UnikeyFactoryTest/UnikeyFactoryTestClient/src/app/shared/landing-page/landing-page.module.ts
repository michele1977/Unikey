import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page.component';
import {SubscribeModule} from '../../features/subscribe/subscribe.module';
import {AppModule} from '../../app.module';
import {LoginComponent} from '../../features/login/login.component';
import {LoginModule} from '../../features/login/login.module';



@NgModule({
    declarations: [LandingPageComponent],
    exports: [
        LandingPageComponent
    ],
    imports: [
        CommonModule,
        SubscribeModule,
        LoginModule
    ]
})
export class LandingPageModule { }
