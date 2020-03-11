import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import {SeeExTestComponent} from './features/see-ex-test/see-ex-test.component';


const routes: Routes = [
  {path: 'seeExTest/:id', component: SeeExTestComponent},
  {path: 'testcontent/:id', component: TestcontentComponent},
  {path: 'create', component: CreationComponent},
  {path: 'testList', component: TestListComponent},
  {path: 'error', component: ErrorComponent},
  {path: '', component: LandingPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
