import {Component, EventEmitter, Output} from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import * as moment from 'moment';
import {IconsService} from '../../services/icons.service';
import {TestListService} from '../../services/test-list.service';
import {TestList} from '../../models/test-list';
import {TestDetailsModalComponent} from '../../modals/test-details-modal/test-details-modal.component';
import {NgbModal, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';
import {ExTest} from '../../models/ex-test';
import {LoaderService} from '../../services/loader.service';
import {EmailModalComponent} from '../../shared/email-modal/email-modal.component';


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
  pageNum = 1;
  pageSize = 10;
  textFilter = '';
  atLast = false;
  tests: TestList;
  pages = 0;
  options: any[] = [10, 20, 40, 50, 60];
  modalOptions: NgbModalOptions;
  constructor(private router: Router, public icons: IconsService, private testService: TestListService, private modalService: NgbModal, private loader: LoaderService) {
    loader.publish('show');
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
  }

  loadCreatePage() {
    this.router.navigateByUrl('create');
  }
  search(form) {
    this.textFilter = form.value.textFilter;
    this.loader.publish('show');
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      if (this.atLast === true) {
        this.pageNum = this.pages;
      }
      this.loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }
  resizePage(event: any) {
    this.loader.publish('show');
    this.pageSize = event.target.value;
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      if (this.atLast === true) {
        this.pageNum = this.pages;
      }
      this.loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }

  closeErrorAlert() {}

  showContent(id: number) {
      this.router.navigateByUrl('testcontent/' + id).then();
  }

  deleteTest() {}

  testDetails(test: Test) {
    const modalRef = this.modalService.open(TestDetailsModalComponent);
    modalRef.componentInstance.myModalTest = test;
  }

  NextPage() {
    this.loader.publish('show');
    let nextPage = this.pageNum.valueOf();
    nextPage++;
    this.testService.getTests(nextPage, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.loader.publish('hide');
      this.pageNum += 1;
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
    if (this.pageNum === this.pages) {
      this.atLast = true;
    }
  }

  lastPage() {
    this.loader.publish('show');
    this.testService.getTests(this.pages, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.pageNum = this.pages;
      this.atLast = true;
      this.loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }

  firstPage() {
    this.loader.publish('show');
    this.testService.getTests(1, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
      this.pageNum = 1;
      this.loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }

  previousPage() {
    this.loader.publish('show');
    let prevPage = this.pageNum.valueOf();
    prevPage--;
    this.testService.getTests(prevPage, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data as TestList;
      this.pages = Math.ceil(data[0].NumberOfTest / this.pageSize);
      this.atLast = false;
      this.loader.publish('hide');
      this.pageNum -= 1;
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }
  showEmailModalMethod(id: number) {
    const modal = this.modalService.open(EmailModalComponent);
    modal.componentInstance.testId = id;
  }
}

