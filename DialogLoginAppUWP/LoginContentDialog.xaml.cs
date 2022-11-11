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

namespace DialogLoginAppUWP {

    public sealed partial class LoginContentDialog : ContentDialog {

        public string Result { get; set; }

        public LoginContentDialog(string url) {
            this.InitializeComponent();

            loginWebView.Source = new Uri(url);
        }

        private void OnCancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            this.Result = null;
            this.Hide();
        }

        private async void OnLoginWebViewNavigationCompleted(Microsoft.UI.Xaml.Controls.WebView2 sender, CoreWebView2NavigationCompletedEventArgs args) {
            string currentUrl = loginWebView.Source.ToString();
            Debug.WriteLine($"Current URL is {currentUrl}");

            await GetAllCookies(currentUrl);
        }

        private async Task<IReadOnlyList<CoreWebView2Cookie>> GetAllCookies(string url) {
            var cookieList = await loginWebView.CoreWebView2.CookieManager.GetCookiesAsync(url);

            Debug.WriteLine("Cookie jar content is:");
            foreach (var cookie in cookieList) {
                Debug.WriteLine($"\t- {cookie.Name}: {cookie.Value}");
            }

            return cookieList;
        }
    }
}
