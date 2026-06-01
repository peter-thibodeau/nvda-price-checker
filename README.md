# NVDA Price Checker

A simple Windows WPF application that displays the current market price for NVIDIA (NVDA) stock.

## Features

- 📊 Real-time NVDA stock price display
- 📈 Shows price change and percentage change
- 🔄 Refresh button to update price on demand
- 🎨 Clean, modern UI
- 🌐 Uses Alpha Vantage API for accurate pricing

## Requirements

- Windows 10 or later
- .NET 8.0 SDK or runtime
- Internet connection

## Setup

1. Clone this repository:
   ```bash
   git clone https://github.com/peter-thibodeau/nvda-price-checker.git
   cd nvda-price-checker
   ```

2. Get a free API key from [Alpha Vantage](https://www.alphavantage.co/):
   - Visit https://www.alphavantage.co/
   - Sign up for a free API key
   - Note: Free tier has 5 requests per minute limit

3. Update the API key in `MainWindow.xaml.cs`:
   ```csharp
   private readonly string _apiKey = "YOUR_API_KEY_HERE";
   ```

## Building

Using Visual Studio:
1. Open `NVDAPriceChecker.csproj` in Visual Studio 2022
2. Build the solution (Ctrl+Shift+B)
3. Run the application (F5)

Using .NET CLI:
```bash
dotnet build
dotnet run
```

## Usage

1. Launch the application
2. Click the "Refresh Price" button
3. The current NVDA price, change, and change percentage will be displayed
4. Price changes are color-coded:
   - 🟢 Green: Price increased
   - 🔴 Red: Price decreased

## API Information

This application uses the [Alpha Vantage API](https://www.alphavantage.co/) for stock data:
- Free tier: 5 requests per minute
- Premium tiers available for higher rate limits
- Real-time data available for US stocks

## Troubleshooting

- **"API rate limit reached"**: The free tier is limited to 5 requests per minute. Wait a moment and try again or upgrade your API key.
- **"Could not retrieve price data"**: Check your internet connection and API key.
- **Network error**: Ensure you have internet connectivity.

## License

This project is open source and available under the MIT License.

## Contributing

Feel free to fork this repository and submit pull requests for any improvements!
