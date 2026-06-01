using System;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace NVDAPriceChecker
{
    public partial class MainWindow : Window
    {
        private readonly string _apiKey = "demo"; // Replace with your API key from https://www.alphavantage.co/
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshButton.IsEnabled = false;
            StatusTextBlock.Text = "Fetching price...";
            StatusTextBlock.Foreground = System.Windows.Media.Brushes.Blue;

            try
            {
                // Using Alpha Vantage API (free tier available)
                string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=NVDA&apikey={_apiKey}";
                
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonResponse);

                // Check for API error messages
                if (data["Note"] != null)
                {
                    StatusTextBlock.Text = "API rate limit reached. Please try again later or upgrade your API key.";
                    StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                    RefreshButton.IsEnabled = true;
                    return;
                }

                JObject quote = (JObject)data["Global Quote"];
                
                if (quote == null || quote["05. price"] == null)
                {
                    StatusTextBlock.Text = "Error: Could not retrieve price data. Please try again.";
                    StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                    RefreshButton.IsEnabled = true;
                    return;
                }

                string price = quote["05. price"].ToString();
                string change = quote["09. change"].ToString();
                string changePercent = quote["10. change percent"].ToString();

                // Update UI
                PriceTextBlock.Text = $"${price}";
                ChangeTextBlock.Text = $"{change} ({changePercent})";
                
                // Color code the change
                if (decimal.TryParse(change, out decimal changeValue))
                {
                    ChangeTextBlock.Foreground = changeValue >= 0 
                        ? System.Windows.Media.Brushes.Green 
                        : System.Windows.Media.Brushes.Red;
                }

                LastUpdatedTextBlock.Text = $"Last updated: {DateTime.Now:g}";
                StatusTextBlock.Text = "Price updated successfully";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (HttpRequestException ex)
            {
                StatusTextBlock.Text = $"Network error: {ex.Message}";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error: {ex.Message}";
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
            finally
            {
                RefreshButton.IsEnabled = true;
            }
        }
    }
}
