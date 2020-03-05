import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page.component';
import {SubscribeModule} from '../../features/subscribe/subscribe.module';



@NgModule({
    declarations: [LandingPageComponent],
    exports: [
        LandingPageComponent
    ],
    imports: [
        CommonModule,
        SubscribeModule
    ]
})
export class LandingPageModule { }
