# Welcome to our global Taxation API

You can see more at: https://TaxationAPI.com :)

Taxation API is an open-source project, where we will collect available tax-rates from different countries. This will be exposed

# Using the data

You can read more about the API here:
https://taxationapi.com/

You can see the Swagger documentation here:
https://api.taxationapi.com/

Down the line, I will require registering a user with an email to get an API key. However, will not launch with this, so it's super important that you set the user agent in your API call to something like:

[Project name] - [Contact person] - [Contact email]

Example useragent string: Taxation project - Lars Holdgaard - mcoroklo@gmail.com

If not, the application will stop working when I add the token authentication.

# How to run projects 

It's a completely standard .NET 7 core API solution. Just open solution and run. No connections or databases, only data-base is a JSON file.

# Your data is wrong

You're VERY welcome to make a pull request. To change data, change it here:
https://github.com/taxationapi/taxationapi.web/blob/main/src/TaxationApi.Web/country_data.json

In your pull request, please explain what it's wrong & preferablyt also add a source.
