import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import * as moment from 'moment';
import {IconsService} from '../../services/icons.service';
import {TestListService} from '../../services/test-list.service';
import {TestList} from '../../models/test-list';
import {TestDetailsModalComponent} from '../../modals/test-details-modal/test-details-modal.component';
import {NgbModal, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';


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
    modalOptions: NgbModalOptions;
    constructor(private router: Router, public icons: IconsService, private testService: TestListService, private modalService: NgbModal) {
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
    });

  
  
    for (let i = 0; i <= 10; i++) {
      const myTest: Test = {
        Date: moment().format('DD/MM/YY H:mm'),
        Title: 'title ' + i,
        Id: i,
        Questions: null
      };
      this.tests.push(myTest);
      this.modalOptions = {
        backdrop: 'static',
        backdropClass: 'customBackdrop'
      };
    }
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

  testDetails(test: Test) {
    const modalRef = this.modalService.open(TestDetailsModalComponent);
    modalRef.componentInstance.myModalTest = test;
  }

  sendMail() {}

  NextPage() {
    this.pageNum += 1;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
    });
    if (this.pageNum === this.pages) {
      this.atLast = true;
    }
  }

  lastPage() {
    this.testService.getTests(this.pages, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.pageNum = this.pages;
      this.atLast = true;
    });
  }

  firstPage() {
    this.testService.getTests(1, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
      this.pageNum = 1;
    });
  }

  previousPage() {
    this.pageNum -= 1;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
    });
  }
}
