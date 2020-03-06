import { Injectable } from '@angular/core';
import {faArrowLeft, faInfo} from '@fortawesome/free-solid-svg-icons';
import {faTrash} from '@fortawesome/free-solid-svg-icons/faTrash';
import {faList} from '@fortawesome/free-solid-svg-icons/faList';
import {faMailBulk} from '@fortawesome/free-solid-svg-icons/faMailBulk';
import {faPlus} from '@fortawesome/free-solid-svg-icons/faPlus';
import {faAngleDoubleLeft} from '@fortawesome/free-solid-svg-icons/faAngleDoubleLeft';
import {faArrowRight} from '@fortawesome/free-solid-svg-icons/faArrowRight';
import {faAngleDoubleRight} from '@fortawesome/free-solid-svg-icons/faAngleDoubleRight';

@Injectable({
  providedIn: 'root'
})
export class IconsService {
  faPlus = faPlus;
  faInfo = faInfo;
  faTrash = faTrash;
  faList = faList;
  faMailForward = faMailBulk;
  faAngleDoubleLeft = faAngleDoubleLeft;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  faAngleDoubleRight = faAngleDoubleRight;
  constructor() { }

}