import { Component } from '@angular/core';
import { Employee } from '../../interfaces/employee.interface';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { first } from 'rxjs';
@Component({
  selector: 'app-employees',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css',
})
export class EmployeesComponent {

  employeesTableColumns: string[] = [
    "Name",
    "Position",
    "BirthDate",
    "Status",
    "Email",
    "Salary",
    "Department",
    "Manager",
  ];

  employees: Employee[] = [
    {
      id: 1, firstName: "Emp", lastName: "1", birthDate: new Date(2000, 0, 1), email: "Emp1@gmail.com", salary: 1000, startDate: new Date(),
      isActive: false, positionId: 3, positionName: "Manager", departmentId: 1, departmentName: "IT", userId: 1, phoneNumber: "+9625458852",
      managerId: null, managerName: null
    },
    {
      id: 2, firstName: "Emp", lastName: "2", birthDate: new Date(1995, 1, 1), startDate: new Date(),
      email: 'Emp2@gmail.com', salary: 1500, isActive: true, phoneNumber: "+9625458852",
      positionId: 2, positionName: 'HR', departmentId: 2, departmentName: 'HR', userId: 2,
      managerId: null, managerName: null
    },
    {
      id: 3, firstName: "Emp", lastName: "3", birthDate: new Date(1998, 5, 2), startDate: new Date(),
      email: 'Emp3@gmail.com', salary: 1800, isActive: true, phoneNumber: "+9625458852",
      positionId: 1, positionName: 'Developer', departmentId: 1, departmentName: 'IT', userId: 3,
      managerId: null, managerName: null
    },
    {
      id: 4, firstName: "Emp", lastName: "4", birthDate: new Date(1995, 1, 2), startDate: new Date(),
      email: 'Emp4@gmail.com', salary: 1200, isActive: false, phoneNumber: "+9625458852",
      positionId: 1, positionName: 'Developer', departmentId: 1, departmentName: 'IT', userId: 4,
      managerId: 3, managerName: "Emp 3"
    },
    {
      id: 5, firstName: "Emp", lastName: "5", birthDate: new Date(2001, 11, 25), startDate: new Date(),
      email: 'Emp5@gmail.com', salary: 800, isActive: true, phoneNumber: "+9625458852",
      positionId: 2, positionName: 'HR', departmentId: 2, departmentName: 'HR', userId: 5,
      managerId: 2, managerName: "Emp 2"
    }
  ];


  employeeForm: FormGroup = new FormGroup({
    id: new FormControl(null),
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    birthDate: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email]),
    salary: new FormControl(null),
    phoneNumber: new FormControl(null, [Validators.required]),
    startDate: new FormControl(null, [Validators.required]),
    endDate: new FormControl(null),
    departmentId: new FormControl(null),
    positionId: new FormControl(null),
    managerId: new FormControl(null),
    isActive: new FormControl(false, [Validators.required]),
  });


  departments = [
    {id: null, name: "Select Department"},
    {id: 1, name: "HR"},
    {id: 2, name: "IT"},
  ];

  positions = [
    {id: null, name: "Select Position"},
    {id: 1, name: "Developer"},
    {id: 2, name: "HR"},
    {id: 3, name: "Manager"},
  ];

  managers = [
    {id: null, name: "Select Manager"},
    {id: 1, name: "Emp 1"},
    {id: 2, name: "Emp 2"}
  ]


}

