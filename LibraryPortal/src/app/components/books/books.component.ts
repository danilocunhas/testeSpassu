import { Component, OnInit } from '@angular/core';
import { Book } from '../../models/book.model';
import { BookService } from '../../services/book.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  books: Book[] = [];

  selectedBook: Book = new Book(0, 0, '', '', 0, 0, 0, [], []);

  constructor(private bookService: BookService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getBooks();
  }

  openModal(book: Book): void {
    this.selectedBook = { ...book };   
  }

  createBook(): void {
    this.selectedBook = new Book(0, 0, '', '', 0, 0, 0, [], []);
  }

  getBooks(): void {
    this.bookService.getBooks().subscribe((data) => {
      this.books = data;    
    });
  }

  deleteBook(book: Book): void {
    this.bookService.deleteBook(book).subscribe((data) => {
      this.toastr.success('Livro excluido com sucesso!');  
      this.getBooks();
    });
  }

  onBookUpdatedOrSaved(): void {
    this.getBooks();
  }
}