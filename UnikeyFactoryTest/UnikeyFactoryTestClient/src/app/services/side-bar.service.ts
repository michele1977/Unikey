import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SideBarService {
  visible: boolean;

  constructor() { this.visible = true; }

  hide() { this.visible = false; }

  show() { this.visible = true; }

}
