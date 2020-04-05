import {Component, Input, OnInit} from '@angular/core';
import {Test} from '../../models/test';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {TestService} from '../../services/test.service';

@Component({
  selector: 'app-modify-test-modal',
  templateUrl: './modify-test-modal.component.html',
  styleUrls: ['./modify-test-modal.component.css']
})
export class ModifyTestModalComponent {
  @Input() inputTest: Test;
  constructor(public activeModal: NgbActiveModal, private test: TestService) {}
  title = '';
   modify(form) {
     this.inputTest.Title = form.value.title;
     this.test.updateTest(this.inputTest).then(this.activeModal.close);
   }
}
