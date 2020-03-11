import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestDetailsModalComponent } from './test-details-modal.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {RouterModule} from '@angular/router';
import {Ng2SearchPipeModule} from 'ng2-search-filter';



@NgModule({
  declarations: [TestDetailsModalComponent],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule,
    Ng2SearchPipeModule,
  ]
})
export class TestDetailsModalModule { }
