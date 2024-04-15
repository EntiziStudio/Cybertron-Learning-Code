# Allspark Project

This guide provides step-by-step instructions to set up and build the Allspark project on your machine.

## Prerequisites

Before getting started, ensure that the following prerequisites are met:

- .NET Core SDK (version 7.0 or later) is installed on your machine. You can download the latest version of .NET Core SDK from the official website: [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
- Visual Studio is installed. You can download Visual Studio from the official website: [visualstudio.microsoft.com/downloads](https://visualstudio.microsoft.com/downloads).

## Getting Started

1. Clone the Allspark repository from the remote repository to your local machine:

   ```bash
   git clone https://gitlab.com/cybertron_planet/allspark.git
   ```

2. Open the `Allspark.sln` solution file in Visual Studio.

## Setting the Startup Project

1. Right-click on the `Allspark.Web` project within the Solution Explorer.

2. Select `Set as Startup Project` from the context menu.

3. Ensure that `Allspark.Web` is set as the startup project for the solution.

## Configuration

Modify the `appsettings.Development.json` file located in the `Allspark.Web` to change the configuration. Below is an example of the configuration file:

```json
{
  ...
  "ConnectionStrings": {
    "CybertronAllsparkDbConnection": "Database=CybertronAllsparkDb;Server=<SQLServerName>;User Id=<SQLUser>;Password=<SQLPassword>;TrustServerCertificate=true;"
  },
  ...
}
```

In the `ConnectionStrings` section, you need to replace `<SQLServerName>`, `<SQLUser>`, and `<SQLPassword>` with the appropriate values for your SQL Server configuration.

Make sure to replace the placeholders accordingly to match your SQL Server connection details.

## Checking TCP/IP Configuration in SQL Server

Before running the Allspark project, ensure that the TCP/IP protocol is enabled in your SQL Server configuration. Follow the steps below:

1. Open SQL Server Configuration Manager.

2. Expand the `SQL Server Network Configuration` node.

3. Click on `Protocols for [Your SQL Server Instance]`.

4. In the right-hand pane, verify that the `TCP/IP` protocol is enabled. If it is not enabled, right-click on `TCP/IP` and select `Enable`.

5. Restart the SQL Server service to apply the changes.

## Applying EF Migrations

1. Open the Package Manager Console in Visual Studio by navigating to `Tools -> NuGet Package Manager -> Package Manager Console`.

2. In the Package Manager Console, set the default project to `Allspark.Infrastructure` by selecting it from the drop-down list.

3. Run the following command to apply EF migrations:

   ```bash
   Update-Database
   ```

4. EF migrations will be applied to the configured database. Verify the success of the migration process by checking the output in the Package Manager Console.

## Building and Running the Project

1. Press `Ctrl + Shift + B` (or navigate to `Build -> Build Solution`) to build the entire solution.

2. Once the build is successful, you can run the project by pressing `F5` or selecting `Start Debugging` from the toolbar.

3. The Allspark application will launch in your default browser, and you can interact with it.

## Copyright

The Allspark project is confidential and proprietary to HUTECH. Unauthorized copying, distribution, or disclosure of this project or its contents is strictly prohibited.

© 2023 HUTECH. All rights reserved.