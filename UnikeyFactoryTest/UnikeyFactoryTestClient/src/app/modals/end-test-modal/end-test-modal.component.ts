import { Component, OnInit } from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {Router} from '@angular/router';

@Component({
  selector: 'app-end-test-modal',
  templateUrl: './end-test-modal.component.html',
  styleUrls: ['./end-test-modal.component.css']
})
export class EndTestModalComponent implements OnInit {

  constructor(
    public activeModal: NgbActiveModal,
    public router: Router
  ) { }

  ngOnInit(): void {
  }

  endPage(){
    this.router.navigateByUrl('endTest');
    this.activeModal.close('Close endTestModal');
  }
}
