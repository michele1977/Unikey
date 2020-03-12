import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import {SeeExTestComponent} from './features/see-ex-test/see-ex-test.component';
import {LoginComponent} from './features/login/login.component';
import {AuthenticationService} from './services/authentication.service';


const routes: Routes = [
  {path: 'seeExTest/:id', component: SeeExTestComponent, canActivate: [AuthenticationService]},
  {path: 'testcontent/:id', component: TestcontentComponent, canActivate: [AuthenticationService]},
  {path: 'create', component: CreationComponent, canActivate: [AuthenticationService]},
  {path: 'testList', component: TestListComponent, canActivate: [AuthenticationService]},
  {path: '', component: LandingPageComponent},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
