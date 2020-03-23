import {Component} from '@angular/core';
import {SideBarService} from '../../services/side-bar.service';


@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent {
  constructor(public nav: SideBarService) {}
   // public isButtonVisible = true;
   // Toggle() {
   //   this.isButtonVisible = false;
   // }
}
