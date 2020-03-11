import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {LoginComponent} from './features/login/login.component';


const routes: Routes = [
  {path: 'testcontent', component: TestcontentComponent},
  {path: 'create', component: CreationComponent},
  {path: 'testList', component: TestListComponent},
  {path: 'ciao', redirectTo: 'http://localhost:999/api/User/TestAction'},
  {path: '', component: LandingPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
