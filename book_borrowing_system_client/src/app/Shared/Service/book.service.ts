import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../Interface/Book.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(private http: HttpClient) {}
  apiurl = `${environment.baseurl}/book`;

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiurl);
  }
  getBookById(id: string | null): Observable<Book> {
    return this.http.get<Book>(`${this.apiurl}/${id}`);
  }
  getBookByQuery(
    name: string | null,
    author: string | null,
    genre: string | null
  ): Observable<Book[]> {
    return this.http.get<Book[]>(
      `${this.apiurl}/search?name=${name}&author=${author}&genre=${genre}`
    );
  }
  deleteBook(id: string) {
    return this.http.delete(`${this.apiurl}/${id}`);
  }
  postBook(book: Book) {
    let formData = new FormData();
    // formData.append('id', '' + book.id);
    formData.append('name', book.name);
    formData.append('rating', '' + book.rating);
    formData.append('author', '' + book.author);
    formData.append('genre', '' + book.genre);
    formData.append('description', book.description);
    // formData.append('is_book_available', '' + book.is_book_available);
    // formData.append('bookImage', book.bookImage);
    formData.append('imageFile', book.imageFile ?? '');

    return this.http.post(this.apiurl, formData);
  }
  borrowBook(id: string){
    return this.http.put<string>(`${this.apiurl}/borrow/${Number(id)}`, {});
  }
  returnBook(id: number){
    return this.http.put<string>(`${this.apiurl}/return/${id}`, {});
  }
  updateBook(id: string, book: Book) {
    let formData = new FormData();
    formData.append('id', '' + book.id);
    formData.append('name', book.name);
    formData.append('author', '' + book.author);
    formData.append('description', book.description);
    formData.append('rating', '' + book.rating);
    formData.append('genre', '' + book.genre);
    formData.append('is_book_available', '' + book.is_book_available);
    formData.append('bookImage', book.bookImage);
    formData.append('imageFile', book.imageFile ?? '');
    return this.http.put(`${this.apiurl}/${id}`, formData);
  }
}
