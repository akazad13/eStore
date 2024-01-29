import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { finalize, firstValueFrom } from 'rxjs';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ICategoryApi } from 'src/app/api';
import { handleError } from 'src/app/shared/functions/error-handler';

@Component({
  selector: 'app-add-edit-category',
  templateUrl: './add-edit-category.component.html',
  styleUrls: ['./add-edit-category.component.css']
})
export class AddEditCategoryComponent implements OnInit {
  addEditCategoryForm!: FormGroup;
  saveInProgress: boolean = false;
  id!: string;
  constructor(
    private CategoryApi: ICategoryApi,
    private location: Location,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id') ?? '0';
    this.addEditCategoryForm = this.fb.group({
      id: ['0'],
      name: ['', [Validators.required]]
    });
  }

  async ngOnInit() {
    if (this.id != '0') {
      await this.getCategory(+this.id);
    }
  }

  async getCategory(id: number) {
    try {
      const data = await firstValueFrom(
        this.CategoryApi.getCategory(id)
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.addEditCategoryForm.patchValue({ name: data.name, id: data.id });
      });
    } catch (error) {
      handleError(null, error);
    }
  }

  async saveCategory() {
    this.addEditCategoryForm.markAllAsTouched();

    if (this.saveInProgress || this.addEditCategoryForm.invalid) {
      return;
    }
    this.saveInProgress = true;
    try {
      var apiCall;
      if (this.addEditCategoryForm.value.id == '0') {
        apiCall = this.CategoryApi.createCategory(
          this.addEditCategoryForm.value.name
        );
      } else {
        apiCall = this.CategoryApi.updateCategory(
          this.addEditCategoryForm.value.id,
          this.addEditCategoryForm.value.name
        );
      }

      const data = await firstValueFrom(
        apiCall.pipe(finalize(() => (this.saveInProgress = false)))
      ).then((value) => {
        // this.router.navigateByUrl('/dashboard');
      });
    } catch (error) {
      handleError(this.addEditCategoryForm, error);
    }
  }

  back(): void {
    this.location.back();
  }

  get name() {
    return this.addEditCategoryForm.get('name') as FormControl;
  }
}
