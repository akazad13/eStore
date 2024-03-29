import {
  Directive,
  Input,
  ViewContainerRef,
  TemplateRef,
  OnInit
} from '@angular/core';
import { AuthService } from '../services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole!: string[];
  isVisible = false;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const userRoles = this.authService.getDecodedToken().role as Array<string>;

    // if no role, clear the viewcontainerRef
    if (!userRoles) {
      this.viewContainerRef.clear();
    }

    // if user has roles, then render the element
    if (this.authService.roleMatch(this.appHasRole)) {
      if (
        this.authService.isRoleExist('Admin') ||
        !this.authService.isRoleExist('Host')
      ) {
        if (!this.isVisible) {
          this.isVisible = true;
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.isVisible = false;
          this.viewContainerRef.clear();
        }
      } else {
        // if (this.appHasRole.includes(this.authService.selectedRole())) {
        //   if (!this.isVisible) {
        //     this.isVisible = true;
        //     this.viewContainerRef.createEmbeddedView(this.templateRef);
        //   } else {
        //     this.isVisible = false;
        //     this.viewContainerRef.clear();
        //   }
        // }
      }
    }
  }
}
