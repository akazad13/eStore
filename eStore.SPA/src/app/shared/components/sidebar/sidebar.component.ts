import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { IAuthApi } from 'src/app/api';
import { AuthService } from '../../services/auth.service';
import { Menu, NavService } from '../../services/nav.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public menuItems: Menu[] = [];
  public url: any;
  public fileurl: any;

  firstName$: Observable<string | null>;
  lastName$: Observable<string | null>;

  constructor(
    private router: Router,
    public navServices: NavService,
    private authApi: IAuthApi,
    public authService: AuthService
  ) {
    this.firstName$ = this.authApi.authUser$.pipe(
      map((x) => (x ? x.firstName : null))
    );
    this.lastName$ = this.authApi.authUser$.pipe(
      map((x) => (x ? x.lastName : null))
    );
    this.navServices.items.subscribe((menuItems) => {
      this.menuItems = menuItems;
      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          menuItems.filter((items) => {
            if (items.path === event.url) {
              this.setNavActive(items);
            }
            if (!items.children) {
              return false;
            }
            items.children.filter((subItems) => {
              if (subItems.path === event.url) {
                this.setNavActive(subItems);
              }
              if (!subItems.children) {
                return false;
              }
              subItems.children.filter((subSubItems) => {
                if (subSubItems.path === event.url) {
                  this.setNavActive(subSubItems);
                }
              });
              return true;
            });
            return true;
          });
        }
      });
    });
  }

  ngOnInit(): void {}

  // Active Nave state
  setNavActive(item: any): void {
    if (window.innerWidth < 500) {
      this.navServices.collapseSidebar = true;
    }
    this.menuItems.filter((menuItem) => {
      if (menuItem !== item) {
        menuItem.active = false;
      }
      if (menuItem.children && menuItem.children.includes(item)) {
        menuItem.active = true;
      }
      if (menuItem.children) {
        menuItem.children.filter((submenuItems) => {
          if (submenuItems.children && submenuItems.children.includes(item)) {
            menuItem.active = true;
            submenuItems.active = true;
          }
        });
      }
    });
  }

  // Click Toggle menu
  toggletNavActive(item: any): void {
    if (!item.active) {
      this.menuItems.forEach((a) => {
        if (this.menuItems.includes(item)) {
          a.active = false;
        }
        if (!a.children) {
          return false;
        }
        a.children.forEach((b) => {
          if (a.children != undefined && a.children.includes(item)) {
            b.active = false;
          }
        });
        return true;
      });
    }
    item.active = !item.active;
  }

  // Fileupload
  readUrl(event: any): void {
    if (event.target.files.length === 0) {
      return;
    }
    // Image upload validation
    const mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    // Image upload
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (events: any) => {
      this.url = reader.result;
    };
  }

  selectRole(role: string): void {
    localStorage.setItem('selectedRole', role);
    debugger;
    if (role === 'Host') {
      this.router.navigate(['/games/hosted']).then(() => {
        window.location.reload();
      });
    } else {
      this.router.navigate(['/games/join-game']).then(() => {
        window.location.reload();
      });
    }
  }
}
