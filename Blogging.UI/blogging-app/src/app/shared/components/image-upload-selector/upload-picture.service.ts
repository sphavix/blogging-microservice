import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Picture } from '../../models/blog-picture.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadPictureService {

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
}
