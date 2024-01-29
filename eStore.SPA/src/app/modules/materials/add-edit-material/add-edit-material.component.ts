import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { finalize, firstValueFrom } from 'rxjs';
import { handleError } from 'src/app/shared/functions/error-handler';
import { IMaterialApi } from 'src/app/api';

@Component({
  selector: 'app-add-edit-material',
  templateUrl: './add-edit-material.component.html',
  styleUrls: ['./add-edit-material.component.css']
})
export class AddEditMaterialComponent implements OnInit {
  addEditMaterialForm!: FormGroup;
  saveInProgress: boolean = false;
  file!: File;
  url: string = '';
  id!: string;
  constructor(
    private materialApi: IMaterialApi,
    private location: Location,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id') ?? '0';
    this.addEditMaterialForm = this.fb.group({
      id: ['0'],
      name: ['', [Validators.required]]
    });
  }

  async ngOnInit() {
    if (this.id != '0') {
      await this.getMaterial(+this.id);
    }
  }

  async getMaterial(id: number) {
    try {
      const data = await firstValueFrom(
        this.materialApi.getMaterial(id)
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.addEditMaterialForm.patchValue({ name: data.name, id: data.id });
        this.url = data.materialImagePath ?? '';
      });
    } catch (error) {
      handleError(null, error);
    }
  }

  async saveMaterial() {
    this.addEditMaterialForm.markAllAsTouched();

    if (this.url == '') {
      this.addEditMaterialForm.setErrors({
        server: 'Material Image is required.'
      });
    }

    if (this.saveInProgress || this.addEditMaterialForm.invalid) {
      return;
    }
    this.saveInProgress = true;
    try {
      var apiCall;
      if (this.addEditMaterialForm.value.id == '0') {
        apiCall = this.materialApi.createMaterial(
          this.addEditMaterialForm.value.name,
          this.file
        );
      } else {
        apiCall = this.materialApi.updateMaterial(
          this.addEditMaterialForm.value.id,
          this.addEditMaterialForm.value.name,
          this.file
        );
      }

      const data = await firstValueFrom(
        apiCall.pipe(finalize(() => (this.saveInProgress = false)))
      ).then((value) => {
        // this.router.navigateByUrl('/dashboard');
      });
    } catch (error) {
      handleError(this.addEditMaterialForm, error);
    }
  }

  onChange(event: any) {
    let files = event.target.files;

    if (files) {
      this.file = files[0];
      let reader = new FileReader();
      reader.onload = (e: any) => {
        this.url = e.target.result;
      };
      reader.readAsDataURL(this.file);
    }
  }

  removeImage() {
    this.url = '';
  }

  back(): void {
    this.location.back();
  }

  get name() {
    return this.addEditMaterialForm.get('name') as FormControl;
  }
}
