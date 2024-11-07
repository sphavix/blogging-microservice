import { Component, OnInit } from '@angular/core';
import { UploadPictureService } from './upload-picture.service';
import { Observable } from 'rxjs';
import { Picture } from '../../models/blog-picture.model';

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
          
          this.getPictures();
        }
      });
    }
  }


  private getPictures(){
    this.pictures$ = this.pictureService.getPictures();
  }
}
