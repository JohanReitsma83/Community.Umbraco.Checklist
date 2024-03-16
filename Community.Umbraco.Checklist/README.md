# Umbraco Checklist Dashboard

![Umbraco Healthcheck Screenshot](https://raw.githubusercontent.com/JohanReitsma83/Community.Umbraco.Checklist/main/documentation/screenshot-healthcheck.png "Umbraco Healthcheck")

Welcome to the Umbraco Dashboard project! This specialized dashboard is designed to seamlessly integrate with your Umbraco CMS, offering a comprehensive view of Umbraco's health checks alongside custom checklist items. Aimed at Umbraco Site builders, this dashboard serves as a basic location for monitoring the system's health and managing essential administrative tasks with ease.

## Features

- **Umbraco Health Checks:** Automatically displays the results of Umbraco's built-in health checks, ensuring your site's optimal performance and security.
- **Custom Checklist Items:** Allows you add your own checklist items using a config file.
- **Approval System:** Empowers Umbraco administrators to approve checklist items, facilitating quality deployment with tasks beyond code
- **Simple Interface:** Designed with a simple overview so you see at login what the status of the application is.

## Purpose

The purpose of this dashboard is to enhance the Umbraco administration experience by providing a tool that not only leverages the existing health check features of Umbraco but also introduces a customizable checklist functionality. This integration aims to improve site maintenance efficiency, and ensure that all aspects of site health and administrative duties are addressed promptly and effectively.



# Configuration

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
An example of the file (checklist.json) could be

```json
[
  {
	"name": "Website Performance Test",
	"uniqueAlias": "9d8b78a2-4bf0-4dc7-9178-9b8e2ff12655",
	"description": "Website is tested for performance by service (WCAG + Pagespeed) and is OK",
	"category": "Manager"
  },
  {
	"name": "Website Domainnames",
	"uniqueAlias": "ed93a5b9-6b3c-4d29-842f-c13b70e1f7aa",
	"description": "Domain names are known and configured for environment",
	"category": "Manager"
  },
  {
	"name": "Mail Formatted",
	"uniqueAlias": "c0d37646-6d5b-42e2-b675-9e07e13de45b",
	"description": "Test mails are correctly formatted in the style of the customer",
	"category": "Manager"
  },
  {
	"name": "mailIsReceived",
	"uniqueAlias": "bf6d7454-7509-4b46-9d37-9181ec3f3e56",
	"description": "Test mails are received by the customer",
	"category": "Manager"
  },
  {
	"name": "User access to CMS",
	"uniqueAlias": "52afac58-c2c7-4f0c-8e3c-5b0a0b7d101d",
	"description": "Does everybody have access to CMS with correct roles?",
	"category": "Manager"
  },
  {
	"name": "GTM / GA",
	"uniqueAlias": "fc367b4e-2ff6-4e88-8ea3-204b78dbcb62",
	"description": "GTM / GA code provided and configured",
	"category": "Marketing"
  },
  {
	"name": "GA search params",
	"uniqueAlias": "63a13276-6d1d-4dbd-9dd0-8b6b36f2b0ec",
	"description": "GA search params are configured",
	"category": "Marketing"
  },
  {
	"name": "DNS Adjusted",
	"uniqueAlias": "2f20e593-4bd1-4b2f-a90a-8cb49f9bf89f",
	"description": "DNS Adjusted before go-live date?",
	"category": "Service"
  },
  {
	"name": "404 Configured",
	"uniqueAlias": "c56b5833-97f4-4e05-a426-839003d5e62a",
	"description": "404 configured within appsettings or middleware?",
	"category": "Developer"
  }
]
```

## Validation

Ensure that any modifications or additions to the JSON file adhere to the defined structure and constraints mentioned above. Invalid JSON structures may cause errors during parsing or processing.

## Feedback and Contributions

If you have suggestions for improving this configuration or would like to contribute additional tasks, please feel free to submit a pull request or reach out to the project maintainers.

