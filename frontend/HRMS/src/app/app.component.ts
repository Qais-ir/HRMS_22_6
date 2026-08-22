import { Component, signal } from '@angular/core';
import { EmployeesComponent } from './components/employees/employees.component';
// Decorator
@Component({
  selector: 'app-root',
  // componet, directives, module, pipe
  imports: [EmployeesComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class App {

}
