# Agent Notes

- **Do not auto-run GUI applications**: The TestApp requires visual inspection. Only build the code, don't run it automatically. Let the user run `.\build.ps1 -Run` or `dotnet run` manually when they're ready to test.

- **Do not add comments to the code**: If something is actually very tricky and non-trivial, the human will add comments.

- C API wrappers: do not add null checks for the widget pointer, in setters. The calling C# code never passes null.

- When adding or implementing a property on `Control` or one of its derived classes, remember to call the Qt native method that sets it both in the setter and in EnsureCreated().