import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import * as jsPDF from 'jspdf';
import {Test} from '../../models/test';

// import StringBuilder from 'string-builder';

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
  let builder = '';
  builder += this.inputTest.Title + '\r\r';

  for (const question of this.inputTest.Questions) {
    builder += question.Text + '\r';
    let i = 1;
    for (const answer of question.Answers) {
    builder += i + '. ' + answer.Text + '\r';
    i++;
    }
    builder += '\r';
  }
  console.log(builder);
  return builder;
}

downloadPDF() {
    const doc = new jsPDF('p', 'mm', 'a4');
    const paragraph = this.builderPdf();
    doc.text(paragraph, 20 , 20 , {maxWidth: 170, align: 'justify'});
    doc.save('Andrea.pdf');
    this.activeModal.dismiss('closeDownload');
  }
}
