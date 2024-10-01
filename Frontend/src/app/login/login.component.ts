import { Component ,OnInit} from '@angular/core';
import { MyServiceService } from '../my-service.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  rForm: FormGroup;
  constructor(private myService : MyServiceService,private router : Router,private fb :  FormBuilder){
    this.rForm = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required],
  });

  }
}
