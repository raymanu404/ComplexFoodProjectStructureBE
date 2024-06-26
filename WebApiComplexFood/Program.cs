namespace WebApiComplexFood
{
    class Program
    {
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
                    webBuilder.UseStartup<Startup>();
				})
			.UseDefaultServiceProvider(options => options.ValidateScopes = false);
	}
}


