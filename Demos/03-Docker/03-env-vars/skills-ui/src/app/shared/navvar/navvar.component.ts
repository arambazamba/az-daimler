import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navvar',
  templateUrl: './navvar.component.html',
  styleUrls: ['./navvar.component.scss'],
})
export class NavvarComponent implements OnInit {
  constructor() {}

  navlinks = [
    { name: 'Home', lnk: '' },
    { name: 'Skills', lnk: '/skills' },
    { name: 'About', lnk: '/about' },
  ];

  ngOnInit(): void {}
}
