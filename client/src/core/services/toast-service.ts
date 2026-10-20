import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor() {
    this.createToastContainer(); // استدعاء إنشاء الحاوية فور بدء الخدمة
  }

  private createToastContainer() {
    if (!document.getElementById('toast-container')) {
      const container = document.createElement('div');
      container.id = 'toast-container';
      container.className = 'toast toast-bottom toast-end z-50'; // إضافة z-50
      document.body.appendChild(container);
    }
  }

  private createToastElement(message: string, alertClass: string, duration = 5000) {
    const toastContainer = document.getElementById('toast-container');
    if (!toastContainer) return;

    const toast = document.createElement('div');
    toast.classList.add('alert', alertClass, 'shadow-lg', 'mb-2', 'flex', 'justify-between', 'items-center');

    // إغلاق الوسوم بشكل سليم وإضافة مسار أيقونة الـ X
    toast.innerHTML = `
      <span>${message}</span>
      <button class="btn btn-sm btn-ghost ml-2" aria-label="close">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>
    `;

    toast.querySelector('button')?.addEventListener('click', () => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    });

    toastContainer.appendChild(toast);

    setTimeout(() => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    }, duration);
  }

  success(message: string, duration?: number) {
    this.createToastElement(message, 'alert-success', duration);
  }

  error(message: string, duration?: number) {
    this.createToastElement(message, 'alert-error', duration);
  }

  warning(message: string, duration?: number) {
    this.createToastElement(message, 'alert-warning', duration);
  }

  info(message: string, duration?: number) {
    this.createToastElement(message, 'alert-info', duration);
  }
}