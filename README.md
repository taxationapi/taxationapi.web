# Welcome to TaxationAPI.com :)

Taxation API is an open-source project, where we will collect available tax-rates from different countries. This will be exposed in an API everyone can use.

Right now (Nov 12, 2022) this has JUST been started. As all hobby projetcts, use with caution as it's not even ready. Collecting data as we speak.

# Using the data

STATUS: Do NOT use yet.

This will be launched at TaxationAPI.com, and the API will be under /api, so the Swagger will be available at TaxationAPI.com/api.

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
