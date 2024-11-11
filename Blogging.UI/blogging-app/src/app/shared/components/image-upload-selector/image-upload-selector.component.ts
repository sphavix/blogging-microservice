import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadPictureService } from './upload-picture.service';
import { Observable } from 'rxjs';
import { Picture } from '../../models/blog-picture.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-upload-selector',
  templateUrl: './image-upload-selector.component.html',
  styleUrls: ['./image-upload-selector.component.css']
})
export class ImageUploadSelectorComponent implements OnInit {


  private file?: File;
  fileName: string = '';
  title: string = '';
  pictures$?: Observable<Picture[]>;

  @ViewChild('form', { static: false}) pictureUploadForm?: NgForm;


  constructor(private pictureService: UploadPictureService){

  }


  ngOnInit(): void {
    this.getPictures();
  }


  onPictureUploadChange(event: Event): void {

    const element = event.currentTarget as HTMLInputElement;

    this.file = element.files?.[0];

  }

  onPictureUpload(): void {
    if(this.file && this.fileName !== '' && this.title !== ''){

      this.pictureService.uploadPicture(this.file, this.fileName, this.title).subscribe({
        next: (response) => {
          this.pictureUploadForm?.resetForm();
          this.getPictures();
        }
      });
    }
  }

  selectedPicture(picture: Picture): void {
    this.pictureService.selectPicture(picture);
  }


  private getPictures(){
    this.pictures$ = this.pictureService.getPictures();
  }
}
