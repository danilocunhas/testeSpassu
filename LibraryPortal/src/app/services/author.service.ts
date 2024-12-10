import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfigService } from './app.config.service';
import { Author } from '../models/author.model';
import { IResult } from '../models/result.model';


@Injectable({providedIn: 'root'})
export class AuthorService {

  private apiUrl:string; 

  constructor(private http: HttpClient, private config: AppConfigService) { 
    this.apiUrl = `${this.config.apiUrl}/Author`;
  }  

  getAuthors(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); 
  }

  deleteAuthor(author: Author): Observable<IResult> {
    return this.http.delete<IResult>(`${this.apiUrl}/${author.id}`);
  }

  addAuthor(author: Author): Observable<IResult> {
    return this.http.post<IResult>(this.apiUrl, author);
  }

}