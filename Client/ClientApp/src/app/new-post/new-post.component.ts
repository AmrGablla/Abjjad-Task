import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { baseUrl } from 'src/environments/environment';
import { CreatePost } from 'src/Models/Post';
import { AuthenticationService } from 'src/services/authentication.service';
import { PostService } from 'src/services/post.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent implements OnInit {
  
  postForm;
  latitude : string;
  longitude: string;
  post: CreatePost = new CreatePost();
  constructor(private formBuilder: FormBuilder, private postService: PostService, private authenticationService : AuthenticationService) {
  
  }

  ngOnInit() {
    console.log(this.getPosition());
    this.postForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(140)]]
    });
  }

  getPosition(): Promise<any> {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition(resp => {

        resolve({ lng: resp.coords.longitude, lat: resp.coords.latitude });
        this.latitude = resp.coords.latitude.toString();
        this.longitude = resp.coords.longitude.toString();
      },
        err => {
          reject(err);
        });
    });

  }
  
  onSubmit() { 
    this.post.Text = this.postForm.get('text').value;
    this.post.Latitude = this.latitude;
    this.post.Longitude = this.longitude; 
    console.log(this.authenticationService.currentUserValue);
    this.post.UserId = this.authenticationService.currentUserValue.id;
    this.postService.CreatePost(this.post);
  }
}


