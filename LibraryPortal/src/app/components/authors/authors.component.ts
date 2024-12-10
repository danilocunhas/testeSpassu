import { Component } from '@angular/core';
import { Author } from '../../models/author.model';
import { AuthorService } from '../../services/author.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrl: './authors.component.css'
})
export class AuthorsComponent {
  authors: Author[] = [];

  selectedAuthor: Author = new Author(0,0, '');

  constructor(private authorService: AuthorService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  openModal(author: Author): void {
    this.selectedAuthor = { ...author };  
  }

  createAuthor(): void {
    this.selectedAuthor = new Author(0, 0, '');
  }

  getAuthors(): void {
    this.authorService.getAuthors().subscribe((data) => {
      this.authors = data;    
    });
  }

  deleteAuthor(author: Author): void {
    this.authorService.deleteAuthor(author).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.toastr.success('Autor excluido com sucesso!');      
          this.getAuthors();    
        }
        else {
          response.errors?.forEach(error => {
            this.toastr.error(error.message);
          });
        }        
      },
      error: (error) => {
        this.toastr.error(error, 'Erro ao excluir autor!');
      }
    });

  }

  onAuthorUpdatedOrSaved(): void {
    this.getAuthors();
  }
}
