import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from './user';
import { lastValueFrom } from 'rxjs';
import { TypicodeComment } from './typicode-comment';

@Injectable({
  providedIn: 'root',
})
export class MyhttpService {
  url = environment.serverUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Promise<User[]> {
    return lastValueFrom(
      this.http.get<User[]>('https://jsonplaceholder.typicode.com/users')
    );
  }

  // getCommentsOfPost(postId: string): Promise<TypicodeComment[]> {
  //   const encoded = encodeURIComponent(postId);
  //   return lastValueFrom(
  //     this.http.get<TypicodeComment[]>(
  //       `https://jsonplaceholder.typicode.com/comments?postId=${encoded}`
  //     )
  //   );
  // }

  getCommentsOfPost(postId: string): Promise<TypicodeComment[]> {
    const url = 'https://jsonplaceholder.typicode.com/comments';
    const params = new HttpParams()
      .set('postId', postId);
    return lastValueFrom(this.http.get<TypicodeComment[]>(url, { params }));
  }
}
