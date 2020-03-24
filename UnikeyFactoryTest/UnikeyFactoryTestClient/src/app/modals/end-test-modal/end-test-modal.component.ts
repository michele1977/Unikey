import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {Router} from '@angular/router';
import {ExTest} from '../../models/ex-test';

@Component({
  selector: 'app-end-test-modal',
  templateUrl: './end-test-modal.component.html',
  styleUrls: ['./end-test-modal.component.css']
})
export class EndTestModalComponent implements OnInit {

  @Input() exTest: ExTest;

  constructor(
    public activeModal: NgbActiveModal,
    public router: Router
  ) { }

  ngOnInit(): void {
  }

  endPage() {
    this.activeModal.close('yes closed');
  }
}
