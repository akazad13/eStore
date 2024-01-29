import { Component, OnInit, HostListener } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';
import { NavService } from '../../../services/nav.service';

@Component({
  selector: 'app-content-layout',
  templateUrl: './content-layout.component.html',
  styleUrls: ['./content-layout.component.scss']
})
export class ContentLayoutComponent implements OnInit {
  public rightSideBar!: boolean;

  constructor(public navServices: NavService) {}

  ngOnInit(): void {}

  @HostListener('document:click', ['$event'])
  clickedOutside(event: any): void {
    // click outside Area perform following action
    var container = document.getElementById('outer-container');
    if (container != null) {
      container.onclick = (e) => {
        e.stopPropagation();
        if (e.target !== document.getElementById('search-outer')) {
          document
            .getElementsByTagName('body')[0]
            ?.classList.remove('offcanvas');
        }
        if (e.target !== document.getElementById('outer-container')) {
          document
            .getElementById('canvas-bookmark')
            ?.classList.remove('offcanvas-bookmark');
        }
      };
    }
  }

  public getRouterOutletState(outlet: any): void {
    return outlet.isActivated ? outlet.activatedRoute : '';
  }
}
