import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AnswerState} from '../../shared/enums/answer-state';
import {ExTest} from '../../models/ex-test';
import {AdministratedTestState} from '../../shared/enums/administrated-test-state';
import {EndTestModalComponent} from '../../modals/end-test-modal/end-test-modal.component';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-fruition-test',
  templateUrl: './fruition-test.component.html',
  styleUrls: ['./fruition-test.component.css']
})
export class FruitionTestComponent implements OnInit {

  @Input() exTest: ExTest;
  @Output() saveTest = new EventEmitter();

  i = 0;
  isLast = false;
  isFirst = true;

  constructor( private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  next() {
    if (this.i === this.exTest.AdministratedQuestions.length - 2) {
      this.isLast = true;
    }
    this.i++;
    if (this.i !== 0) {
      this.isFirst = false;
    }
  }

  end() {
    const modalRef = this.modalService.open(EndTestModalComponent);
    modalRef.componentInstance.exTest = this.exTest;
    modalRef.result.then((result) => {
      if (result == 'yes closed') {
        this.saveTest.emit(this.exTest);
      }
    });
  }

  previous() {
    if (this.i === 1) {
      this.isFirst = true;
    }
    this.i--;
    if (this.i < this.exTest.AdministratedQuestions.length - 1) {
      this.isLast = false;
    }
  }

  change(index: number) {
    this.exTest.AdministratedQuestions[this.i].AdministratedAnswers[index].isSelected = !this.exTest.AdministratedQuestions[this.i].AdministratedAnswers[index].isSelected;
    return this.i;
  }
}
