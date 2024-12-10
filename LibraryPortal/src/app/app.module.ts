import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';  
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { booksCreateComponent } from './components/books-create/books-create.component';
import { BooksComponent } from './components/books/books.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxMaskDirective, provideEnvironmentNgxMask, provideNgxMask } from 'ngx-mask';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { AuthorsCreateComponent } from './components/authors-create/authors-create.component';

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    booksCreateComponent,
    HomeComponent,
    AuthorsCreateComponent,
    AuthorsComponent,
    AuthorsComponent,
    AuthorsCreateComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,  
    NgxMaskDirective,
    ToastrModule.forRoot()  
  ],
  providers: [
    provideEnvironmentNgxMask(),
    provideNgxMask({ thousandSeparator: ',', decimalMarker: '.' })
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
