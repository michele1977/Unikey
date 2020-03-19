import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {TestService} from '../../services/test.service';
import { saveAs } from 'file-saver';
import {Test} from '../../models/test';
import {LoaderService} from '../../services/loader.service';

@Component({
  selector: 'app-create-pdf-modal',
  templateUrl: './create-pdf-modal.component.html',
  styleUrls: ['./create-pdf-modal.component.css']
})
export class CreatePDFModalComponent implements OnInit {
  @Input() inputTest: Test;

  fileUrl;

  constructor(
    public activeModal: NgbActiveModal,
    public testService: TestService,
    private loader: LoaderService
  ) { }

  ngOnInit(): void {
  }

  downloadPDF() {
    this.loader.publish('show');
    this.testService.downloadPdf(this.inputTest.Id).subscribe(value => {
      saveAs(value, this.inputTest.Title + '.pdf');
      this.loader.publish('hide');
      this.activeModal.close();
    });
  }
}
