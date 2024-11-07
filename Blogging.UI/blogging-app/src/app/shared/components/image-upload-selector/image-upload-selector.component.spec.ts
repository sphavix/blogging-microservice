import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImageUploadSelectorComponent } from './image-upload-selector.component';

describe('ImageUploadSelectorComponent', () => {
  let component: ImageUploadSelectorComponent;
  let fixture: ComponentFixture<ImageUploadSelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImageUploadSelectorComponent]
    });
    fixture = TestBed.createComponent(ImageUploadSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
