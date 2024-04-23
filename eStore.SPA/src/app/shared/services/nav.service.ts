import { Injectable, HostListener } from '@angular/core';
import { BehaviorSubject, Observable, Subscriber } from 'rxjs';

// Menu
export interface Menu {
  path: string;
  title: string;
  icon: string;
  type: string;
  badgeType?: string;
  badgeValue?: string;
  active?: boolean;
  bookmark?: boolean;
  children?: Menu[];
  allowedRole: string[];
}

@Injectable({
  providedIn: 'root'
})
export class NavService {
  public screenWidth: any;
  public collapseSidebar = false;
  public fullScreen = false;

  // Windows width
  @HostListener('window:resize', ['$event'])
  MENUITEMS: Menu[] = [
    {
      title: 'Dashboard',
      path: '/dashboard',
      icon: 'fa-tachometer',
      type: 'link',
      active: false,
      allowedRole: ['Admin', 'User']
    },
    {
      title: 'Order Management',
      path: '',
      icon: 'fa-shopping-cart',
      type: 'sub',
      active: false,
      allowedRole: ['Admin', 'User'],
      children: [
        {
          path: '/orders',
          title: 'Orders',
          type: 'link',
          icon: 'fa-shopping-bag',
          active: false,
          allowedRole: ['Admin']
        }
      ]
    },
    {
      title: 'Product Management',
      path: '',
      icon: 'fa-database',
      type: 'sub',
      active: false,
      allowedRole: ['Admin', 'User'],
      children: [
        {
          path: '/categories',
          title: 'Categories',
          type: 'link',
          icon: 'fa-sitemap',
          active: false,
          allowedRole: ['Admin']
        },
        {
          path: '/products',
          title: 'Products',
          type: 'link',
          icon: 'fa-cubes',
          active: false,
          allowedRole: ['Admin']
        },
        {
          path: '/materials',
          title: 'Materials',
          type: 'link',
          icon: 'fa-bullseye',
          active: false,
          allowedRole: ['Admin']
        }
      ]
    },
    {
      title: 'Marketing',
      path: '',
      icon: 'fa-bullhorn',
      type: 'sub',
      active: false,
      allowedRole: ['Admin', 'User'],
      children: [
        {
          path: '',
          title: 'Promos',
          type: 'link',
          icon: 'fa-tag',
          active: false,
          allowedRole: ['Admin']
        },
        {
          path: '',
          title: 'Newsletter Subscribers',
          type: 'link',
          icon: 'fa-send-o',
          active: false,
          allowedRole: ['Admin']
        }
      ]
    },
    {
      title: 'Admin',
      path: '',
      icon: 'fa-user',
      type: 'sub',
      active: false,
      allowedRole: ['Admin', 'User'],
      children: [
        {
          path: '',
          title: 'Users',
          type: 'link',
          icon: 'fa-users',
          active: false,
          allowedRole: ['Admin']
        },
        {
          path: '',
          title: 'Roles',
          type: 'link',
          icon: 'fas fa-cog',
          active: false,
          allowedRole: ['Admin']
        }
      ]
    },
    {
      path: 'configurations',
      title: 'Configurations',
      type: 'link',
      icon: 'fa-cogs',
      active: false,
      allowedRole: ['Admin']
    },
    {
      title: 'Profile',
      path: '/profile/view',
      icon: 'fa-user',
      type: 'link',
      active: false,
      allowedRole: ['Admin', 'User']
    },
    {
      title: 'Blog',
      path: '/blog',
      icon: 'fa-edit',
      type: 'link',
      active: false,
      allowedRole: ['Admin', 'User']
    }
  ];
  // Array
  items = new BehaviorSubject<Menu[]>(this.MENUITEMS);

  constructor() {
    this.onResize();
    if (this.screenWidth < 991) {
      this.collapseSidebar = true;
    }
  }

  onResize(event?: any): void {
    this.screenWidth = window.innerWidth;
  }
}
