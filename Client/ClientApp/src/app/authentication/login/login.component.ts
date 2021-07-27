import { Router, ActivatedRoute } from '@angular/router'; 
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms'; 
import { AuthenticationService } from 'src/services/authentication.service';
import { UserForAuthenticationDto } from 'src/_interfaces/UserForAuthenticationDto'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: UserForAuthenticationDto = new UserForAuthenticationDto();
  
  constructor(
    private authenticationApiService: AuthenticationService, 
    private router: Router
   ) { } 

  ngOnInit(): void { 
  }

  onSubmit(form: NgForm): void {
    if (form.invalid) {
      form.form.markAllAsTouched(); 
    }
    this.authenticationApiService.loginUser(this.user).subscribe((res) => {  
      this.router.navigate(['/']);
    }
    );
  } 
}