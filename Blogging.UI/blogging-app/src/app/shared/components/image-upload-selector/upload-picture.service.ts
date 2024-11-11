import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Picture } from '../../models/blog-picture.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadPictureService {

  // create a behavior subject for the model to trigger the observables
  selectedPicture: BehaviorSubject<Picture> = new BehaviorSubject<Picture>({
    id: '',
    fileExtension: '',
    fileName: '',
    title: '',
    pictureUrl: ''
  });

  constructor(private http: HttpClient) { }

  getPictures(): Observable<Picture[]> {
    return this.http.get<Picture[]>(`${environment.apiBaseUrl}/api/pictures`);
  }

  uploadPicture(file: File, fileName: string, title: string): Observable<Picture> {
    const formData = new FormData();

    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('title', title);

    return this.http.post<Picture>(`${environment.apiBaseUrl}/api/pictures`, formData);

  }

  selectPicture(picture: Picture): void {
    this.selectedPicture.next(picture);
  }

  onSelectedPicture(): Observable<Picture> {
    return this.selectedPicture.asObservable();
  }
}


