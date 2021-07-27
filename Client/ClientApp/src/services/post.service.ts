import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseUrl } from 'src/environments/environment';
import { CreatePost } from 'src/Models/Post';

@Injectable({
  providedIn: 'root'
})
export class PostService {  
  prefix = 'post';

  constructor(private http: HttpClient) { 
  } 

  public CreatePost = (body: CreatePost) => {
    console.log('call api');
    console.log(body);

    return this.http.post<CreatePost>(`${baseUrl}/${this.prefix}`, body).subscribe();
  }
}
