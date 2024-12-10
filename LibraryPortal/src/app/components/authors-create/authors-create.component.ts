import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthorService } from '../../services/author.service';
import { Author } from '../../models/author.model';
import { ToastrService } from 'ngx-toastr';

declare var bootstrap: any;

@Component({
  selector: 'app-authors-create',
  templateUrl: './authors-create.component.html',
  styleUrl: './authors-create.component.css'
})
export class AuthorsCreateComponent implements OnInit, AfterViewInit {

  @Input() author: Author = new Author(0, 0, '');
  @Output() authorUpdatedOrSaved = new EventEmitter<void>();

  authors: Author[] = [];
  private modal: any;

  constructor(private authorService: AuthorService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  ngAfterViewInit() {
    const modalElement = document.getElementById('createAuthorModal');
    this.modal = new bootstrap.Modal(modalElement);
  }

  saveAuthor() {
    this.authorService.addAuthor(this.author).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.toastr.success('Autor cadastrado com sucesso!');
          this.modal.hide();
        }
        else {
          response.errors?.forEach(error => {
            this.toastr.error(error.message);
          });
        }
        this.authorUpdatedOrSaved.emit();
      },
      error: (error) => {
        this.toastr.error(error, 'Erro ao cadastrar o autor!');
      }
    });
  }

  getAuthors(): void {
    this.authorService.getAuthors().subscribe((data) => {
      this.authors = data;
    });
  }
}
