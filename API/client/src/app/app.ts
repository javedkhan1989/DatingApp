import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Nav } from "../layout/nav/nav";


import { User } from '../types/user';
import { Router, RouterOutlet } from '@angular/router';
import { Home } from '../features/home/home';
import { NgClass } from '@angular/common';


@Component({
  selector: 'app-root',
  imports: [Nav, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App  {  
  protected router = inject(Router);
  
}
