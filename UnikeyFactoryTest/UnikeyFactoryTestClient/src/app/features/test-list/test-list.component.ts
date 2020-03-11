import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import * as moment from 'moment';
import {IconsService} from '../../services/icons.service';
import {TestListService} from '../../services/test-list.service';
import {TestList} from '../../models/test-list';


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
    .pointer {
      cursor: pointer;
    }
  `]
})
export class TestListComponent {

  showDeleteError = false;
  pageNum = 1;
  pageSize = 10;
  textFilter = '';
  atLast = false;
  tests: TestList;
  pages = 0;
  options: any[] = [10, 20, 40, 50, 60];
  showEmailModal = false;
  constructor(private router: Router, public icons: IconsService, private testService: TestListService) {
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
    });

  }
  loadCreatePage() {
    this.router.navigateByUrl('create');
  }
  search(form) {
    this.textFilter = form.value.textFilter;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
    });
  }
  resizePage(event: any) {
    this.pageSize = event.target.value;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      if (this.atLast === true) {
        this.pageNum = this.pages;
      }
    });
  }

  closeErrorAlert() {}

  showContent() {}

  deleteTest() {}

  testDetails() {}

  sendMail() {}

  NextPage() {

    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.pageNum += 1;
    });
    if (this.pageNum === this.pages) {
      this.atLast = true;
    }
  }

  lastPage() {
    this.pageNum = this.pages;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.pageNum = this.pages;
      this.atLast = true;
    });
  }

  firstPage() {
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
      this.pageNum = 1;
    });
  }

  previousPage() {
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
      this.pageNum -= 1;
    });
  }
}
