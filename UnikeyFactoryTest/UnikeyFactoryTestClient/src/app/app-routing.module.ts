import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import {SeeExTestComponent} from './features/see-ex-test/see-ex-test.component';
import { ExTestListComponent } from './features/ex-test-list/ex-test-list.component';
import { FruitionLandingPageComponent } from './shared/fruition-landing-page/fruition-landing-page.component';
import {FruitionTestComponent} from './features/fruition-test/fruition-test.component';

import {StatisticPageComponent} from './features/statistic-page/statistic-page.component';
import {AuthenticationService} from './services/authentication.service';

const routes: Routes = [
  {path: 'fruitiontest', component: FruitionTestComponent},
  {path: 'seeExTest/:id', component: SeeExTestComponent, canActivate: [AuthenticationService]},
  {path: 'testcontent/:id', component: TestcontentComponent, canActivate: [AuthenticationService]},
  {path: 'create', component: CreationComponent, canActivate: [AuthenticationService]},
  {path: 'testList', component: TestListComponent, canActivate: [AuthenticationService]},
  {path: 'extestList', component: ExTestListComponent, canActivate: [AuthenticationService]},
  {path: 'error', component: ErrorComponent},
  {path: 'beginTest', component: FruitionLandingPageComponent},
  {path: 'statistic/:id', component: StatisticPageComponent},
  {path: '', component: LandingPageComponent},
  {path: '**', component: ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

