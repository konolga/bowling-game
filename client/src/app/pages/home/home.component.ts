import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@services/auth.service';
import { User } from '@models/user';
import { AlertifyService } from '@services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;

  user: User = {
    Id: null,
    Username: '',
  };

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['', [Validators.required, Validators.pattern('^[a-zA-Zs]*$')]],
    });
  }
  get f() {
    return this.form.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.user.Username = this.f.username.value;
    this.loading = true;
    this.authService
      .auth(this.user)
      .subscribe(response  => {
          this.alertify.success(`Welcome to Bowling game`);
          this.router.navigate(['dashboard']);
        }, error => {
          this.alertify.error("Please try to play later");
          this.loading = false;
        });
  }
}
