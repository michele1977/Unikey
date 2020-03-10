import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import * as moment from 'moment';
import {IconsService} from '../../services/icons.service';


@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styles: [`
    .main-div {
      display: flex;
    }
    .title {
      float: left;
    }
    .create-test-a-div {
      margin-left: 20px;
    }
    .create-test-a{
      float: left;
    }
  `]
})
export class TestListComponent {

  showDeleteError = false;
  pageSize = 10;
  textFilter = '';
  tests: Test[] = [];
  showEmailModal = false;
  constructor(private router: Router, public icons: IconsService) {
    for (let i = 0; i <= 10; i++) {
      const myTest: Test = {
        Date: moment().format('DD/MM/YY H:mm'),
        Title: 'title ' + i,
        Id: i,
        Questions: null
      };
      this.tests.push(myTest);
    }
  }
  loadCreatePage() {
    this.router.navigateByUrl('create');
  }
  search(form) {
    this.textFilter = form.value.textFilter;
  }
  resizePage() {}

  closeErrorAlert() {}

  showContent() {}

  deleteTest() {}

  testDetails() {}

  sendMail() {}
}
