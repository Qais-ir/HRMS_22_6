import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle } from '@angular/common';
import { FormsModule, FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
// Decorator
@Component({
  selector: 'app-root',
  // componet, directives, module, pipe
  imports: [RouterOutlet, NgIf, NgFor, NgClass, NgStyle, FormsModule, ReactiveFormsModule],
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

  images = [
    "https://i0.wp.com/picjumbo.com/wp-content/uploads/beautiful-fall-nature-scenery-free-image.jpeg?w=2210&quality=70",
    "https://t3.ftcdn.net/jpg/02/70/35/00/360_F_270350073_WO6yQAdptEnAhYKM5GuA9035wbRnVJSr.jpg",
    "https://images.pexels.com/photos/26151151/pexels-photo-26151151/free-photo-of-night-sky-filled-with-stars-reflecting-in-the-lake.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
  ];
  currentIndex: number = 0; // Global Variable

  name : string = "employee";

  form = new FormGroup({
    // Form Controls
    name: new FormControl("Employee"),
  });

  next(){
    // let temp = 5; // local variable
    // temp++;

    if(this.currentIndex < this.images.length - 1){
      this.currentIndex++;
    }
  }

  previous(){
    if(this.currentIndex > 0){
        this.currentIndex--;
    }
  }


  temp(x : number , y : string) : number{
    let num : number;
    //num = "ss";
    return 5;
  }
}
