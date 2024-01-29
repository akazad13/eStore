/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AddEditMaterialComponent } from './add-edit-material.component';

describe('AddMaterialComponent', () => {
  let component: AddEditMaterialComponent;
  let fixture: ComponentFixture<AddEditMaterialComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditMaterialComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
