import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle } from '@angular/common';
// Decorator
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgIf, NgFor, NgClass, NgStyle],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class App {

  title: string = "Welcome to Angular from Typescript";
  number : number = 55.2254;
  bool : boolean = true;
  arr : string[] = ["one", "two", "three"];

  students = [
    {id:0, name: "stu1", mark: 49},
    {id:1, name: "stu2", mark: 88},
    {id:2, name: "stu3", mark: 56},
    {id:3, name: "stu4", mark: 32},
    {id:4, name: "stu5", mark: 98},
    {id:5, name: "stu6", mark: 66},
  ];

  temp(x : number , y : string) : number{
    let num : number;
    //num = "ss";
    return 5;
  }
}
