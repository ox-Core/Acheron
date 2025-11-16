# Getting Started

This document provides instructions for installing and using Acheron in your project.

## Requirements
| Requirement | Purpose | Version |
| ----------  | ------- | ------- |
| .NET Developer Tools | Toolchain for building your game | 9.0 >= |

## Create Your First Game

In your terminal run the following
```
dotnet new console --name <project name>
```

And then open your editor in the created folder
Then to add acheron as a dependency run this in the projects folder 
```
dotnet add package Acheron.Engine
```


Now as a basic example try creating a window in main as shown
```C#
var world = new World();

world.ImportModule<WindowModule>();

ref var window = ref world.GetSingleton<Window>();
while(!window.ShouldClose) {
    world.Update();
}
```