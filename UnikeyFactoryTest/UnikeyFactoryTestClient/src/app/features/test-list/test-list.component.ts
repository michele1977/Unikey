import {Component, HostListener, Inject} from '@angular/core';
import {Router} from '@angular/router';
import {Test} from '../../models/test';
import {IconsService} from '../../services/icons.service';
import {TestListService} from '../../services/test-list.service';
import {TestDetailsModalComponent} from '../../modals/test-details-modal/test-details-modal.component';
import {NgbModal, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';
import {LoaderService} from '../../services/loader.service';
import {EmailModalComponent} from '../../shared/email-modal/email-modal.component';
import {DOCUMENT} from '@angular/common';
import {WINDOW} from '../../services/window-ref.service';
import {TestService} from '../../services/test.service';
import {ExTestListService} from '../../services/ex-test-list.service';
import {HttpErrorResponse} from '@angular/common/http';


@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css'],
})
export class TestListComponent {
  areThereModifies = false;
  showDeleteError = false;
  pageNum = 1;
  pageSize = 10;
  textFilter = '';
  tests: Test[];
  numberOfTest: number;
  modalOptions: NgbModalOptions;
  temptest: Test;
  errorFetch = false;
  error: HttpErrorResponse;
  errorsList: string[];

  isEmpty = true;

  constructor(private router: Router, public icons: IconsService, private testService: TestListService,
              private modalService: NgbModal, private loader: LoaderService, private service: TestService,
              @Inject(DOCUMENT) private document: Document, @Inject(WINDOW) private window, private exTestService: ExTestListService) {
    loader.publish('show');
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter).then(data => {
      this.numberOfTest = data[0].NumberOfTest;
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
    this.testService.getTests(this.pageNum, this.pageSize, this.textFilter)
      .then(data => {
        this.loader.publish('hide');
        this.numberOfTest = data[0].NumberOfTest;
        this.tests = data;
       }, error => {
         this.loader.publish('hide');
         this.errorFetch = true;
         this.error = error;
         this.errorsList = [];
         const modelstate = error.error.ModelState;
         for (const key in modelstate) {
            if (modelstate.hasOwnProperty(key)) {
              const val = modelstate[key];
              this.errorsList.push(val);
            }
          }
       });
    this.loader.publish('hide');
  }

  closeErrorAlert() {
    this.showDeleteError = false;
  }

  showContent(id: number) {
    this.router.navigateByUrl('testcontent/' + id).then();
  }

  deleteTest(test: Test) {
    this.temptest = test;
    this.areThereModifies = true;
  }

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
      if (this.tests.length < this.numberOfTest) {
        this.loader.publish('show');
        let nextPage = this.pageNum.valueOf();
        nextPage++;
        this.testService.getTests(nextPage, this.pageSize, this.textFilter).then(data => {
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

  copytoClipboard(url: string) {
    console.log('linkcopiato');
    const box = document.createElement('textarea');
    box.value = url;
    document.body.appendChild(box);
    box.select();
    document.execCommand('copy');
    document.body.removeChild(box);
    alert('Copied!');
  }

  undo() {
    this.areThereModifies = false;
    this.showDeleteError = false;
  }

  saveChanges() {
    this.service.deleteTest(this.temptest.Id).then(() => {
      const index = this.tests.findIndex((d) => d.Id === this.temptest.Id);
      this.tests.splice(index, 1);
      this.areThereModifies = false;
    }, error => {
      this.showDeleteError = true;
    });
  }
}
