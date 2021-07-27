import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from 'src/environments/environment';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public posts: Post[];

  constructor(http: HttpClient) {
    http.get<Result>(baseUrl + '/post').subscribe(result => { 
      this.posts = result.data; 
    }, error => console.error(error));
  }
}

interface Result{
  data: Post[];
}

interface Post {
  text: string; 
}
 