# .NET Developer Exercise

## Notes
*   **Make sure you are using the `master` branch.**
*   **Prior to running the application, please change the path of the database file to a path of your choice.** It is stored in a constant called `filePath` in the `Window_Initialized` method inside the `MainWindow` class. If the file pointed to doesn't exist, it will be created automatically.

    You may wish to use the path of the database file provided in the repository. The file is called `database.sqlite` and located in the `DotNetDeveloperExercise1` project folder.
*   There are three buttons:
    *   *Create table* --- creates database table (if it doesn't exist already);
    *   *Insert cars* --- inserts some dummy car data into the database (if they don't exist in the database already);
    *   *Refresh* --- loads all data from the database into the data grid.
    
    If in Step 1 you set the path to that of the provided `database.sqlite` file, the data grid should be populated automatically and you shouldn't need to press any of the buttons. Otherwise, you will need press all three buttons in the order they are shown (from left to right), after which the data grid should be populated. Pressing any of the buttons more than necessary is OK.
