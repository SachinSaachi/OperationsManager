using Microsoft.Extensions.Configuration;

namespace Common.AppConfiguration
{
	namespace Common
	{
		public interface IAppConfiguration
		{
			string ConnectionString();
		}

		public class AppConfiguration : IAppConfiguration
		{
			private readonly IConfiguration _configuration;
			public AppConfiguration(IConfiguration configuration)
			{
				_configuration = configuration;
			}
			public  string ConnectionString()
			{
                var connectionString = _configuration["connectionStrings:ConnectionStringBaseProcess"].ToString();

				//var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
				//var IntExample = MyConfig.GetValue<int>("AppSettings:SampleIntValue");
				//var AppName = MyConfig.GetValue<string>("AppSettings:APP_Name");

				//string str1  = System.Configuration.ConfigurationManager.AppSettings["connectionStrings:ConnectionStringBaseProcess"].ToString();

				//string connectionString = Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringBaseProcessZT"].ToString());
				//string connectionString = Encryption.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringBaseProcess"].ToString());

				return connectionString;
			}
		}
	}
}
