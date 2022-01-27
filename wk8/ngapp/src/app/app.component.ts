import { Component } from '@angular/core';
import { MyhttpService } from './myhttp.service';
import { TypicodeComment } from './typicode-comment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'ngapp';
  postId: string = '';
  comments: TypicodeComment[] = [];

  constructor(private service: MyhttpService) {}

  showComments(): void {
    this.service.getCommentsOfPost(this.postId).then((comments) => {
      this.comments = comments;
    });
  }
}
