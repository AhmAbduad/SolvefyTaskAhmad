import { Component ,inject} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
   private router = inject(Router);

  login(event: Event): void {
    event.preventDefault();

    // Demo navigation only — no credentials are verified.
    this.router.navigate(['/dashboard']);
  }
}
