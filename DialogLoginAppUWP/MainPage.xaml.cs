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

namespace DialogLoginAppUWP {

    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();
        }

        private async void OnLoginButtonClick(object sender, RoutedEventArgs e) {
            string url = loginUrlTextBox.Text;
            if (!string.IsNullOrEmpty(url)) {
                var dialog = new LoginContentDialog(url);
                await dialog.ShowAsync();

                resultTextBlock.Text = dialog.Result ?? "Operation canceled.";
            } else {
                ContentDialog dialog = new ContentDialog {
                    Title = "Could not open login dialog",
                    Content = "Login URL is empty.",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
