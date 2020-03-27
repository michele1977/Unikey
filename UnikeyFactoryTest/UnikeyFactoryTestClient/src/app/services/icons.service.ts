import { Injectable } from '@angular/core';
import {faArrowLeft, faInfo, faBackward, faSignInAlt, faCheck, faSignOutAlt} from '@fortawesome/free-solid-svg-icons';
import {faTrash} from '@fortawesome/free-solid-svg-icons/faTrash';
import {faList} from '@fortawesome/free-solid-svg-icons/faList';
import {faMailBulk} from '@fortawesome/free-solid-svg-icons/faMailBulk';
import {faPlus} from '@fortawesome/free-solid-svg-icons/faPlus';
import {faAngleDoubleLeft} from '@fortawesome/free-solid-svg-icons/faAngleDoubleLeft';
import {faArrowRight} from '@fortawesome/free-solid-svg-icons/faArrowRight';
import {faAngleDoubleRight} from '@fortawesome/free-solid-svg-icons/faAngleDoubleRight';
import {faPencilAlt} from '@fortawesome/free-solid-svg-icons/faPencilAlt';
import {faWindowClose} from '@fortawesome/free-solid-svg-icons/faWindowClose';
import {faFilePdf} from '@fortawesome/free-solid-svg-icons/faFilePdf';
import {faCopy} from '@fortawesome/free-solid-svg-icons';

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
  faPencilAlt = faPencilAlt;
  faBackward = faBackward;
  faSignInAlt = faSignInAlt;
  faCheck = faCheck;
  faWindowClose = faWindowClose;
  faCopy = faCopy;
  faLogout = faSignOutAlt;
  faFilePdf = faFilePdf;
  constructor() { }

}
