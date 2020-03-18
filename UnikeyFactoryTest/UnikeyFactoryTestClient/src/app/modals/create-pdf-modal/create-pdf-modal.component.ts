import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import * as jsPDF from 'jspdf';
import {Test} from '../../models/test';

import StringBuilder from 'string-builder';

@Component({
  selector: 'app-create-pdf-modal',
  templateUrl: './create-pdf-modal.component.html',
  styleUrls: ['./create-pdf-modal.component.css']
})
export class CreatePDFModalComponent implements OnInit {
  @Input() inputTest: Test;

  constructor(
    public activeModal: NgbActiveModal
  ) { }

  ngOnInit(): void {
  }

  builderPdf(): string {
    const builder = new StringBuilder();
    builder.append(this.inputTest.Title + '\n\n');

    for (const question of this.inputTest.Questions) {
      builder.append(question.Text + '\n');
      for (const answer of question.Answers) {
        builder.append( '◻️' + answer.Text + '\n');
      }
      builder.append('\n');
    }

    return builder.toString();
  }

  downloadPDF() {
    const doc = new jsPDF();
    doc.text('ciao', 10, 10);
    doc.save();

    this.activeModal.dismiss('closeDownload');
  }
}
