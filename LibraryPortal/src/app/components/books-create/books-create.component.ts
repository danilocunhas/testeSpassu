import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book.model';
import { Author } from '../../models/author.model';
import { AuthorService } from '../../services/author.service';
import { ToastrService } from 'ngx-toastr';

declare var bootstrap: any; 

@Component({
  selector: 'app-books-create',
  templateUrl: './books-create.component.html',
  styleUrl: './books-create.component.css'
})
export class booksCreateComponent implements OnInit, AfterViewInit  {

  @Input() book: Book = new Book(0, 0, '', '', 0, 0, 0, [], []);
  @Output() bookUpdatedOrSaved = new EventEmitter<void>();

  authors: Author[] = [];
  private modal: any;
  
  constructor(private bookService: BookService,
    private authorService: AuthorService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  ngAfterViewInit() {    
    const modalElement = document.getElementById('createBookModal');
    this.modal = new bootstrap.Modal(modalElement);
  }

  saveBook() {
    if (this.book.id === 0) {
      this.bookService.addBook(this.book).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.toastr.success('Livro cadastrado com sucesso!');
            this.modal.hide();
          }
          else {
            response.errors?.forEach(error => {       
              this.toastr.error(error.message);
            });
          }
          this.bookUpdatedOrSaved.emit();
        },
        error: (error) => {
          this.toastr.error(error, 'Erro ao cadastrar livro!');
        }
      });
    } else {
      this.bookService.updateBook(this.book).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.toastr.success('Livro editado com sucesso!');
            this.modal.hide();
          }
          else {
            response.errors?.forEach(error => {       
              this.toastr.error('Erro ao editar livro!', error.message );
            });
          }
          this.bookUpdatedOrSaved.emit();
        },
        error: (error) => {
          this.toastr.error('Erro ao editar livro!', error.message );
        }
      });
    }

  }

  getAuthors(): void {
    this.authorService.getAuthors().subscribe((data) => {
      this.authors = data;
    });
  }
}
