import { Location } from '@angular/common';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  editProductForm!: FormGroup;
  updateProductInProgress: boolean = false;
  constructor(private fb: FormBuilder, private location: Location) {}

  ngOnInit() {
    this.editProductForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]]
    });
  }

  updateProduct() {}

  get name() {
    return this.editProductForm.get('name') as FormControl;
  }

  back(): void {
    this.location.back();
  }
}
