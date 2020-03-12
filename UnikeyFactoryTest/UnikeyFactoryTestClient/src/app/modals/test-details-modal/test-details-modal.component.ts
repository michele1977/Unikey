import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {Test} from '../../models/test';
import {ExTestService} from '../../services/exTest.service';
import {ExTest} from '../../models/ex-test';
import {IconsService} from '../../services/icons.service';
import * as moment from 'moment';

@Component({
  selector: 'app-test-details-modal',
  templateUrl: './test-details-modal.component.html',
  styleUrls: ['./test-details-modal.component.css']
})
export class TestDetailsModalComponent implements OnInit {

  @Input() myModalTest: Test;
  @Input() myFakeExTests: ExTest[];

  exTests: ExTest[];
  textFilter: '';

  constructor(public activeModal: NgbActiveModal, private service: ExTestService, public icons: IconsService) { }


  ngOnInit(): void {
/*    this.service.getExTestByTestId(this.myModalTest.Id).subscribe((value => {
      this.exTests = value;
    }));*/
  }

}
