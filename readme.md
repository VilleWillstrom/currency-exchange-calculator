# Currency Converter API

Currency Converter API is a .NET 6 web API that allows users to convert amounts between different currencies using exchange rates fetched from an external API.

<br><br>

## Features

- Convert amounts from one currency to another.
- Get real-time exchange rates from an external API.
- OpenAPI (Swagger) support for easy API testing and documentation.

## Prerequisites

Before you begin, make sure you have the following installed on your local machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- An API key from [ExchangeRate-API](https://www.exchangerate-api.com/)

<br><br>


# Setup

### Clone the repository

```
git clone https://github.com/your-username/CurrencyConverterAPI.git
cd CurrencyConverterAPI
```

## Configuration

1. Copy the example settings file for development:

```
cp appsettings.Development.example appsettings.Development.json
```

2. Add your API key to the appsettings.Development.json file (make a copy of appsettings.Development.json.example). This file will hold needed environmental variables on development environment:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "API_KEY": "your-api-key-here"
}
```

## Install Dependencies
Run the following command to restore the project dependencies:
```
dotnet restore
```

## Build and Run the Application

1. Use the following command to build and run the application locally:

```
dotnet run
```

2. Once the application is running, you can access the API in your browser at: 
```
https://localhost:5001
```
<br><br><br>


# API Endpoints

## Swagger / Development

If you are running the project in the development environment, Swagger UI will be enabled, allowing you to test the API directly from the web interface.

Endpoint:
```
/swagger
```
<br><br>
## Default Route

A simple default route returning an empty JSON object:

Endpoint:
```
Get /
```
<br><br>
## Convert Currency
This endpoint allows you to calculate the conversion of an amount between two currencies. You need to provide three parameters:

- FromCurrency: The currency code (e.g., USD, EUR) from which the amount will be converted.
- ToCurrency: The currency code (e.g., EUR, GBP) to which the amount will be converted.
- Amount: The amount of money to be converted.

The service will retrieve the current exchange rate between the two currencies and return the converted amount along with the exchange rate used.

Upon success, the API returns a JSON object containing:

- FromCurrency: The original currency from which the amount was converted.
- ToCurrency: The target currency to which the amount was converted.
- Amount: The original amount before conversion.
- ConvertedAmount: The amount after conversion to the target currency.
- ExchangeRate: The exchange rate used for the conversion.

Endpoint:
```
POST /api/currency/convert
```

Request Body:
```
{
  "fromCurrency": "USD",
  "toCurrency": "EUR",
  "amount": 100
}
```

Response:
```
{
  "fromCurrency": "USD",
  "toCurrency": "EUR",
  "amount": 100,
  "convertedAmount": 85.5,
  "exchangeRate": 0.855
}
```