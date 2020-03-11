import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CreationComponent} from './features/creation/creation.component';
import {LandingPageComponent} from './shared/landing-page/landing-page.component';
import {TestListComponent} from './features/test-list/test-list.component';
import {TestcontentComponent} from './features/test-list/testcontent/testcontent.component';
import {ErrorComponent} from './shared/error/error.component';
import { RformspikeComponent } from './features/rformspike/rformspike.component';


const routes: Routes = [
  {path: 'testcontent/:id', component: TestcontentComponent},
  {path: 'create', component: CreationComponent},
  {path: 'testList', component: TestListComponent},
  {path: 'error', component: ErrorComponent},
  {path: 'spike', component: RformspikeComponent},
  {path: '', component: TestListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
