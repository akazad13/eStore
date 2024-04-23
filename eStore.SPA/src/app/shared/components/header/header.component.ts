import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Inject,
  OnDestroy
} from '@angular/core';
import { NavService, Menu } from '../../services/nav.service';
import { DOCUMENT } from '@angular/common';
import { Subject, firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { IAuthApi, IUserApi } from 'src/app/api';
import { TranslateService } from '@ngx-translate/core';
import { handleError } from '../../functions/error-handler';
import { AuthService } from '../../services/auth.service';

const body = document.getElementsByTagName('body')[0];

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  public menuItems!: Menu[];
  public items!: Menu[];
  public searchResult = false;
  public searchResultEmpty = false;
  public openNav = false;
  public rightSidebar = false;
  public text!: string;
  public elem: any;
  public isOpenMobile = false;
  @Output() rightSidebarEvent = new EventEmitter<boolean>();
  private destroy$: Subject<void> = new Subject<void>();

  lang: string = "en";

  constructor(
    public navServices: NavService,
    @Inject(DOCUMENT) private document: any,
    private authApi: IAuthApi,
    private userApi: IUserApi,
    private router: Router,
    private translate: TranslateService,
    private authService: AuthService,
  ) {
    this.lang = this.authService.getCurrentUserLocale() || "en"; 
    translate.setDefaultLang(this.lang);
  }

  ngOnDestroy(): void {
    this.removeFix();
    this.destroy$.next();
    this.destroy$.complete();
  }

  rightSideBar(): void {
    this.rightSidebar = !this.rightSidebar;
    this.rightSidebarEvent.emit(this.rightSidebar);
  }

  collapseSidebar(): void {
    this.navServices.collapseSidebar = !this.navServices.collapseSidebar;
  }

  openMobileNav(): void {
    this.openNav = !this.openNav;
  }
  public async changeLanguage(lang : string) {
    this.translate.use(lang);
    this.lang = lang;
    try {
      const data = await firstValueFrom(
        this.userApi.Update(this.authService.getCurrentUserId(), lang)
      );
      this.authService.setCurrentUserLocale(lang);
    } catch (error) {
      handleError(null, error);
    }
  }

  searchTerm(term: any): any {
    term ? this.addFix() : this.removeFix();
    if (!term) {
      return (this.menuItems = []);
    }
    const items: Menu[] = [];
    term = term.toLowerCase();
    this.items.filter((menuItems) => {
      if (menuItems == undefined) return false;
      if (
        menuItems.title != undefined &&
        menuItems.title.toLowerCase().includes(term) &&
        menuItems.type === 'link'
      ) {
        items.push(menuItems);
      }
      if (!menuItems.children) {
        return false;
      }
      menuItems.children.filter((subItems) => {
        if (subItems == undefined) return false;
        if (
          !!subItems.title &&
          subItems.title.toLowerCase().includes(term) &&
          subItems.type === 'link'
        ) {
          subItems.icon = menuItems.icon;
          items.push(subItems);
        }
        if (!subItems.children) {
          return false;
        }
        subItems.children.filter((suSubItems) => {
          if (
            !!suSubItems.title &&
            suSubItems.title.toLowerCase().includes(term)
          ) {
            suSubItems.icon = menuItems.icon;
            items.push(suSubItems);
          }
        });
        return true;
      });
      this.checkSearchResultEmpty(items);
      this.menuItems = items;
      return true;
    });
  }

  checkSearchResultEmpty(items: Menu[]): void {
    if (!items.length) {
      this.searchResultEmpty = true;
    } else {
      this.searchResultEmpty = false;
    }
  }

  addFix(): void {
    this.searchResult = true;
    body.classList.add('offcanvas');
  }

  removeFix(): void {
    this.searchResult = false;
    body.classList.remove('offcanvas');
    this.text = '';
  }
  ngOnInit(): void {
    this.elem = document.documentElement;
    this.navServices.items.subscribe((menuItems) => {
      this.items = menuItems;
    });
  }

  toggleFullScreen(): void {
    this.navServices.fullScreen = !this.navServices.fullScreen;
    if (this.navServices.fullScreen) {
      if (this.elem.requestFullscreen) {
        this.elem.requestFullscreen();
      } else if (this.elem.mozRequestFullScreen) {
        /* Firefox */
        this.elem.mozRequestFullScreen();
      } else if (this.elem.webkitRequestFullscreen) {
        /* Chrome, Safari and Opera */
        this.elem.webkitRequestFullscreen();
      } else if (this.elem.msRequestFullscreen) {
        /* IE/Edge */
        this.elem.msRequestFullscreen();
      }
    } else {
      if (!this.document.exitFullscreen) {
        this.document.exitFullscreen();
      } else if (this.document.mozCancelFullScreen) {
        /* Firefox */
        this.document.mozCancelFullScreen();
      } else if (this.document.webkitExitFullscreen) {
        /* Chrome, Safari and Opera */
        this.document.webkitExitFullscreen();
      } else if (this.document.msExitFullscreen) {
        /* IE/Edge */
        this.document.msExitFullscreen();
      }
    }
  }

  logout(): void {
    this.authApi.setUser(null);
    this.router.navigateByUrl('/auth/login');
  }
}
