import {Component, HostListener, Inject} from '@angular/core';
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
import {DOCUMENT} from '@angular/common';
import {WINDOW} from '../../services/window-ref.service';


@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styles: [`
    .title {
      float: left;
      margin-right: 3%;
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
  tests: Test[];
  modalOptions: NgbModalOptions;
  constructor(private router: Router, public icons: IconsService, private testService: TestListService,
              private modalService: NgbModal, private loader: LoaderService,
              @Inject(DOCUMENT) private document: Document, @Inject(WINDOW) private window) {
    loader.publish('show');
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).subscribe(data => {
      this.tests = data;
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
      this.tests = data;
      this.loader.publish('hide');
    }, error => {
      this.loader.publish('hide');
      alert('Ooops something went wrong');
    });
  }

  closeErrorAlert() {}

  showContent(id: number) {
      this.loader.publish('show');
      this.router.navigateByUrl('testcontent/' + id).then();
  }

  deleteTest() {}

  testDetails(test: Test) {
    const modalRef = this.modalService.open(TestDetailsModalComponent);
    modalRef.componentInstance.myModalTest = test;
  }

  showEmailModalMethod(id: number) {
    const modal = this.modalService.open(EmailModalComponent);
    modal.componentInstance.testId = id;
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const pos = document.documentElement.scrollTop + document.documentElement.offsetHeight;
    const max = document.documentElement.scrollHeight;
    if (pos >= max - 1 && pos <= max) {
      this.loader.publish('show');
      let nextPage = this.pageNum.valueOf();
      nextPage++;
      this.testService.getTests(nextPage, this.pageSize, this.textFilter).subscribe(data => {
        data.forEach(value => {
          this.tests.push(value);
        });
        this.pageNum += 1;
        this.window.scrollTo(0, max + 1);
        this.loader.publish('hide');
      }, error => {
        this.loader.publish('hide');
        alert('Ooops something went wrong');
      });
    }
  }
}

