using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GetYandexMusicToken
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			InitializeAsync();
		}

		/// <summary>
		/// Асинхронное выполнение кода
		/// </summary>
		async Task InitializeAsync()
		{
			// Включение CoreWebView2
			await webView.EnsureCoreWebView2Async(null);

			// Подписка на эвент, вызываемый при перенаправлении на новую страницу
			webView.CoreWebView2.NavigationStarting += (_, e) =>
			{
				// Если ссылка не содержит токен - продолжение работы
				if (!e.Uri.Contains("#access_token=")) return;

				((Grid)webView.Parent).Children.Remove(webView);
				Loading.Visibility = Visibility.Collapsed;

				token.Text = e.Uri.Substring(e.Uri.IndexOf("#access_token=") + 14).Split('&')[0];
			};
		}

		//Копирование текста
		private void Button_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(token.Text);

		//Удаление куков
		private void Button_Click_1(object sender, RoutedEventArgs e) => webView.CoreWebView2.CookieManager.DeleteAllCookies();
	}
}
