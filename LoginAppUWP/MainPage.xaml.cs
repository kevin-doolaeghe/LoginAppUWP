using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LoginAppUWP {

    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();
        }

        private async void OnLoginButtonClick(object sender, RoutedEventArgs e) {
            string url = loginUrlTextBox.Text;
            if (!string.IsNullOrEmpty(url)) {
                loginWebView.Source = new Uri(url);
            } else {
                ContentDialog dialog = new ContentDialog {
                    Title = "Could not open login dialog",
                    Content = "Login URL is empty.",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

        private void OnLoginWebViewNavigationCompleted(Microsoft.UI.Xaml.Controls.WebView2 sender, CoreWebView2NavigationCompletedEventArgs args) {
            string currentUrl = loginWebView.Source.ToString();
            Debug.WriteLine($"Current URL is {currentUrl}");

            currentUrlTextBlock.Text = currentUrl;

            GetAllCookies(currentUrl);
        }

        private async void GetAllCookies(string url) {
            var cookieList = await loginWebView.CoreWebView2.CookieManager.GetCookiesAsync(url);
            Debug.WriteLine("Cookie jar content is:");
            foreach (var cookie in cookieList) {
                Debug.WriteLine($"\t- {cookie.Name}: {cookie.Value}");
            }
        }
    }
}
