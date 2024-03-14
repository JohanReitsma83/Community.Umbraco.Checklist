# Configuration Readme

## JSON Configuration File Reference in appsettings

To reference the JSON configuration file `checklist.json` within the `appsettings.json` file, use the following settings under the "Checklist" section:

```json
"Checklist": {
    "file": "/settings/checklist.json"
}
```

## File Location

Ensure that the `checklist.json` file is located at the specified path: `/configuration/checklist.json`. Adjust the path if the file is located in a different directory.

## Usage

1. **File Path Configuration**: Modify the `"file"` value under the "Checklist" section to specify the correct path to the `checklist.json` file relative to your application's root directory.

2. **Accessing the Configuration**: The JSON configuration file can then be accessed by your application using the provided file path.

## Example appsettings.json:

```json
{
  "Checklist": {
    "file": "/settings/checklist.json"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## Note

Ensure that the specified file path is correct and accessible by your application. Invalid file paths may lead to errors when attempting to access the configuration file.


## JSON Configuration File for Checklist

This JSON configuration file defines a list of tasks related to website development and maintenance. Each task is represented by an object with the following properties:

- **name**: The name of the task.
- **uniqueAlias**: A unique identifier for the task.
- **description**: A brief description of what the task entails.
- **category**: The category to which the task belongs. This will group the UI,

## Usage

1. **Accessing the Configuration File**: The JSON configuration file can be accessed and modified using any text editor or JSON parser.

2. **Understanding the Structure**: Each task object within the JSON file contains four properties: "name", "uniqueAlias", "description", and "category".

3. **Editing Tasks**: To modify or add tasks, edit the existing JSON file following the same structure and guidelines. Ensure that each task has a unique "uniqueAlias".


## Example Task:

```json
{
    "name": "Website Performance Test",
    "uniqueAlias": "9d8b78a2-4bf0-4dc7-9178-9b8e2ff12655",
    "description": "Website is tested for performance by service (WCAG + Pagespeed) and is OK",
    "category": "Manager"
}
```

## Validation

Ensure that any modifications or additions to the JSON file adhere to the defined structure and constraints mentioned above. Invalid JSON structures may cause errors during parsing or processing.

## Feedback and Contributions

If you have suggestions for improving this configuration or would like to contribute additional tasks, please feel free to submit a pull request or reach out to the project maintainers.

