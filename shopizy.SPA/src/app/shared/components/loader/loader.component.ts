import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {
  public show = true;
  constructor() {
    setTimeout(() => {
      this.show = false;
    }, 1500);
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {}

}
