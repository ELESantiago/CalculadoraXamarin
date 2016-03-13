using System;

namespace CalculadoraXamarinTest
{
	public class App : Xamarin.Forms.Application
	{
		public App ()
		{
            // The root page of your application
            MainPage = new CalculadoraTest();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

