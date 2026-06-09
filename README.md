Gemini: https://gemini.google.com/share/c3eb9388b850

**Step 1: Create a New Project in VS 2022**

1. Open Visual Studio 2022.

2. On the startup window, click Create a new project.

3. In the search bar at the top, type Web API.

4. Select ASP.NET Core Web API (make sure it has the C# tag) and click Next.

**Step 2: Configure Your Project**

1. Project Name: Type ExpenseTrackerApi.

2. Location: Choose where you want to save your project on your machine.

3. Click Next.

**Step 3: Select Framework and Project Options**

On the Additional Information screen, configure these exact settings to ensure a modern, lightweight setup:

Framework: Select .NET 8.0 (Long Term Support) or .NET 9.0.

Authentication type: None.

Configure for HTTPS: Checked.

Enable Docker: Unchecked (keep it simple for now).

Use controllers (uncheck to use minimal APIs): UNCHECK this box. (Unchecking this tells Visual Studio you want to use modern Minimal APIs instead of the older, heavy Controllers style).

Enable OpenAPI support: CHECK this box. (This automatically installs and configures Swagger for you).

Click Create.
