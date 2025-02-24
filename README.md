# Prezi Viewer App
This is a simple WPF app built with ReactiveUI, It uses the MVVM pattern and dependency injection.


## Overview
PreziViewer lets you load and view presentations. It fetches data from local and online sources and shows you a list of presentations. You can click on a presentation to see more details, use a custom toolbar for window commands, and navigate between views.

![image](https://github.com/user-attachments/assets/0ce5ee7a-4491-408f-b333-5eabd1064774)



## Getting Started
- Build The solution PreziViewer.
- Open the PreziViewer.App as it is the starting point.
- Run the PreziViewer.App.

## Configuration:

- The app reads configuration from appsettings.json in the PreziViewer.App project.
- Modify the file paths or online repository URL as needed.

## Architecture
- WPF & MVVM: The app uses WPF for the UI and follows the Model-View-ViewModel pattern.
- ReactiveUI: For data binding and reactive commands.
- Dependency Injection: Uses Microsoft.Extensions.Hosting to set up services, views, and view models.
- Services: Handles fetching presentations from local and online sources.
- Views & ViewModels: Separate UI logic (Views) from business logic (ViewModels).
