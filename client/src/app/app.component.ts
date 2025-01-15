import { AccountService } from './_services/account.service';
import { Component, inject, OnInit } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [RouterOutlet, NavComponent, HomeComponent],
})
export class AppComponent implements OnInit {
  accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const userObj = JSON.parse(userString);
    this.accountService.currentUser.set(userObj);
  }
}
