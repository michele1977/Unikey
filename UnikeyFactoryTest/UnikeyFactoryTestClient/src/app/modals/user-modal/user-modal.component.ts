import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-user-modal',
  templateUrl: './user-modal.component.html',
  styleUrls: ['./user-modal.component.css']
})
export class UserModalComponent implements OnInit {
  @Input() inputUser: string;
  modifycontent = false;
  username = '';
  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  modify(form) {
    this.inputUser = form.value.username;
    this.modifycontent = false;
  }
}
