import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from './user';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MyhttpService {
  url = environment.serverUrl;

  constructor(private http: HttpClient) {
  }

  getUsers(): Promise<User[]> {
    return lastValueFrom(this.http.get<User[]>('https://jsonplaceholder.typicode.com/users'));
  }
}
