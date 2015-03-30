using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.WebUI.Hubs;

namespace Edi.WebUI.DbNotifier
{
    public class PoChangeNotifier
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public PoChangeNotifier()
        {
            // Start SqlDependency with application initialization
            SqlDependency.Start(_connString);
        }

        public void GetAllPos()
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [ID]
                      FROM [PurchaseOrders].[PurchaseOrders]", connection))
                {
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    // Must execute reader to make things go
                    using (var reader = command.ExecuteReader())
                    {
                    }
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                PoHub.UpdatePos();
                GetAllPos();
            }
        }
    }
}
