import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import {SeeExTestComponent} from './features/see-ex-test/see-ex-test.component';
import { ExTestListComponent } from './features/ex-test-list/ex-test-list.component';
import {LoginComponent} from './features/login/login.component';
import {AuthenticationService} from './services/authentication.service';
<<<<<<< HEAD
import { BeginTestComponent } from './features/fruition/begin-test/begin-test.component';
import { FruitionLandingPageComponent } from './shared/fruition-landing-page/fruition-landing-page.component';
=======
import {FruitionTestComponent} from './features/fruition-test/fruition-test.component';
import {EndTestComponent} from './features/fruition-test/end-test/end-test.component';
>>>>>>> 5603ff6f26cb1797d448e22536fab7cf0674f538


const routes: Routes = [
  {path: 'fruitiontest', component: FruitionTestComponent},
  {path: 'seeExTest/:id', component: SeeExTestComponent},
  {path: 'testcontent/:id', component: TestcontentComponent},
  {path: 'create', component: CreationComponent},
  {path: 'testList', component: TestListComponent},
  {path: 'extestList', component: ExTestListComponent},
  {path: 'endTest', component: EndTestComponent},
  {path: 'error', component: ErrorComponent},
  {path: 'beginTest', component: FruitionLandingPageComponent},
  {path: '', component: LandingPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
