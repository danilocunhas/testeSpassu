import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/book.model';
import { IResult } from '../models/result.model';
import { AppConfigService } from './app.config.service';

@Injectable({providedIn: 'root'})
export class BookService {

  private apiUrl:string;

  constructor(private http: HttpClient, private config: AppConfigService) { 
    this.apiUrl = `${this.config.apiUrl}/Book`;
  }  

  addBook(book: Book): Observable<IResult> {
    return this.http.post<IResult>(this.apiUrl, book);
  }

  updateBook(book: Book): Observable<IResult> {
    return this.http.put<IResult>(this.apiUrl, book);
  }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  deleteBook(book: Book): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${book.id}`);
  }
}