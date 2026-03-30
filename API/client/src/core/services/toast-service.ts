import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private router=inject(Router);

  constructor() { 
    this.createToastContainer();   
  }

  private createToastContainer(){
    if (!document.getElementById('toast-container')) {
      const container = document.createElement('div');
      container.id = 'toast-container';
      container.className = 'toast toast-bottom toast-end z-50';      
      document.body.appendChild(container);
    }
  }

  private createToastElement(message: string, alterClass:string, duration:number = 5000,
     avtaar?:string,route?:string ){ 
    const toastContainer = document.getElementById('toast-container');
    if (!toastContainer) return;
      
    const toast = document.createElement('div');
    toast.classList.add('alert', alterClass, 'shadow-lg','flex',
      'items-center','gap-3','cursor-pointer');

if(route){
  toast.addEventListener('click', () => {
    this.router.navigateByUrl(route);
  });
}

    toast.innerHTML = `
    ${avtaar ? `<img src="${avtaar || 'user.png'}" alt="Avatar" class="w-10 h-10 rounded-full">` : ''}>}
    <span>${message}</span>
    <button class="btn btn-sm btn-ghost ml-4">✕</button>`;

    toast.querySelector('button')?.addEventListener('click', () => {
      toastContainer.removeChild(toast);
    });
    toastContainer.appendChild(toast);
    setTimeout(() => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    }, duration);
  }

  success(message: string, duration?: number, avtaar?:string,route?:string) {
    this.createToastElement(message, 'alert-success', duration,avtaar,route);
  }

  error(message: string, duration?: number,avtaar?:string,route?:string) {
    this.createToastElement(message, 'alert-error', duration,avtaar,route);
  }

  warning(message: string, duration?: number,avtaar?:string,route?:string) {
    this.createToastElement(message, 'alert-warning', duration,avtaar,route);
  }
  info(message: string, duration?: number,avtaar?:string,route?:string) {
    this.createToastElement(message, 'alert-info', duration,avtaar,route);
  }
  
}
