import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation.component';
import {SubscribeComponent} from './core/subscribe/subscribe.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';


const routes: Routes = [
  {path: 'create', component: CreationComponent},
  {path: '', component: LandingPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
