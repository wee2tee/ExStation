using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace ExStation.Models
{
    public class DBX
    {
        public const string member_server = "localhost";
        public const string member_db_name = "xmember";
        public const string member_db_uid = "root";
        public const string member_db_pwd = "12345";
        public const int member_db_port = 3306;

        public static mEntities GetMemberDB(string server_name, string user_name, string password, string database_name, int port = 3306)
        {
            string originalConnectionString = ConfigurationManager.ConnectionStrings["mEntities"].ConnectionString;
            EntityConnectionStringBuilder ecsBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            SqlConnectionStringBuilder scsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString)
            {
                DataSource = server_name,
                UserID = user_name,
                Password = password,
                InitialCatalog = database_name
            };

            string providerConnectionString = scsBuilder.ToString();
            ecsBuilder.ProviderConnectionString = providerConnectionString;

            string contextConnectionString = ecsBuilder.ToString();
            return new mEntities(contextConnectionString);
        }
    }
}
