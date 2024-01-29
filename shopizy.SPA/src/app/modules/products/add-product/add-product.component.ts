import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { finalize, firstValueFrom } from 'rxjs';
import { ICategoryApi, IProductApi } from 'src/app/api';
import { handleError } from 'src/app/shared/functions/error-handler';
import { Category } from 'src/app/shared/model/category/category.model';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  addProductForm!: FormGroup;
  saveInProgress: boolean = false;
  files: File[] = [];
  urls = new Array<string>();
  categories!: Category[];

  constructor(
    private productApi: IProductApi,
    private categoryApi: ICategoryApi,
    private fb: FormBuilder,
    private location: Location
  ) {
    this.addProductForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      productDetails: ['test', [Validators.required]],
      category: ['', [Validators.required]],
      sku: ['', [Validators.required]],
      price: ['', [Validators.required]],
      discount: ['', [Validators.required]],
      tags: ['test', [Validators.required]]
    });
  }

  async ngOnInit() {
    await this.getCategories();
  }

  async addProduct() {
    this.addProductForm.markAllAsTouched();
    debugger;
    if (this.saveInProgress || this.addProductForm.invalid) {
      return;
    }
    this.saveInProgress = true;
    try {
      const formdata = this.addProductForm.value;
      const data = await firstValueFrom(
        this.productApi
          .createProduct(
            formdata.name,
            formdata.description,
            formdata.productDetails,
            formdata.category,
            formdata.sku,
            formdata.price,
            formdata.discount,
            formdata.tags,
            this.files
          )
          .pipe(finalize(() => (this.saveInProgress = false)))
      ).then((value) => {
        // this.router.navigateByUrl('/dashboard');
      });
    } catch (error) {
      handleError(this.addProductForm, error);
    }
  }

  async getCategories(): Promise<void> {
    try {
      const data = await firstValueFrom(
        this.categoryApi.getCategories(null, 1, -1)
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.categories = data.items;
      });
    } catch (error) {
      handleError(null, error);
    }
  }

  onChange(event: any) {
    let files = event.target.files;

    if (files) {
      for (let file of files) {
        this.files.push(file);
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        };
        reader.readAsDataURL(file);
      }
    }
  }

  back(): void {
    this.location.back();
  }

  get name() {
    return this.addProductForm.get('name') as FormControl;
  }
  get description() {
    return this.addProductForm.get('description') as FormControl;
  }
  get productDetails() {
    return this.addProductForm.get('productDetails') as FormControl;
  }
  get category() {
    return this.addProductForm.get('category') as FormControl;
  }
  get sku() {
    return this.addProductForm.get('sku') as FormControl;
  }
  get price() {
    return this.addProductForm.get('price') as FormControl;
  }
  get discount() {
    return this.addProductForm.get('discount') as FormControl;
  }
  get tags() {
    return this.addProductForm.get('tags') as FormControl;
  }

  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: 'auto',
    minHeight: '12rem',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: 'Enter text here...',
    defaultParagraphSeparator: '',
    defaultFontName: 'Arial',
    defaultFontSize: '',
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' }
    ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote'
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1'
      }
    ],
    // uploadUrl: 'v1/image',
    // upload: (file: File) => {
    //   return true;
    // },
    // uploadWithCredentials: false,
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [['insertImage', 'insertVideo'], ['fontSize']]
  };
}
