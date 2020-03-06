import { Injectable } from '@angular/core';
import {faInfo} from '@fortawesome/free-solid-svg-icons';
import {faTrash} from '@fortawesome/free-solid-svg-icons/faTrash';
import {faList} from '@fortawesome/free-solid-svg-icons/faList';
import {faMailBulk} from '@fortawesome/free-solid-svg-icons/faMailBulk';
import {faPlus} from '@fortawesome/free-solid-svg-icons/faPlus';

@Injectable({
  providedIn: 'root'
})
export class IconsService {
  faPlus = faPlus;
  faInfo = faInfo;
  faTrash = faTrash;
  faList = faList;
  faMailForward = faMailBulk;
  constructor() { }

}
