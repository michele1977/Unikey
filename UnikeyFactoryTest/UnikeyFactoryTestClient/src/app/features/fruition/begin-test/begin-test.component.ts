import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { TestSubject } from 'src/app/models/testSubject';

@Component({
  selector: 'app-begin-test',
  templateUrl: './begin-test.component.html',
  styleUrls: ['./begin-test.component.css']
})
export class BeginTestComponent implements OnInit {
  subject: TestSubject;
  @Output() subjectEvent = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  toggleFirstQuestion(subject: TestSubject) {
    this.subject = subject;
    const stringifiedSubject = JSON.stringify(this.subject);
    this.subjectEvent.emit(stringifiedSubject);
  }
}