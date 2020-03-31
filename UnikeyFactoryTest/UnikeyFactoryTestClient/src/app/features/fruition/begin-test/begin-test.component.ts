import {Component, OnInit, Output, EventEmitter, OnDestroy} from '@angular/core';
import { TestSubject } from 'src/app/models/testSubject';
import {SideBarService} from '../../../services/side-bar.service';

@Component({
  selector: 'app-begin-test',
  templateUrl: './begin-test.component.html',
  styleUrls: ['./begin-test.component.css']
})
export class BeginTestComponent implements OnInit {
  subject: TestSubject;
  @Output() subjectEvent = new EventEmitter();

  constructor(public nav: SideBarService) { }

  ngOnInit(): void {
    this.nav.hide();
  }

  toggleFirstQuestion(subject: TestSubject) {
    this.subject = subject;
    const stringifiedSubject = JSON.stringify(this.subject);
    this.subjectEvent.emit(stringifiedSubject);
  }
}
