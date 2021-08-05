using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySqlDataPusher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection("server=172.18.0.2;Port=3306;Protocol=TCP;database=onlyoffice;User Id=onlyoffice_user;password=onlyoffice_pass;"))
            {
                var hasher = new Hasher();

                mySqlConnection.Open();

                int index = 1;
                for (int i = 0; i < 10; i++)
                {
                    var coreUserRows = new List<string>();
                    var coreUserSecurityRows = new List<string>();

                    var commandCoreUser = new StringBuilder("INSERT INTO core_user (tenant, id, username, firstname, lastname, email, last_modified) VALUES ");
                    var commandUserSecurity = new StringBuilder("INSERT INTO core_usersecurity (tenant, userid, pwdhash) VALUES ");

                    for (int j = 0; j < 10000; j++)
                    {
                        var user = Guid.NewGuid().ToString();

                        coreUserRows.Add($"(1, '{user}', 'TestUser{index}', 'Test', 'testuser{index}@onlyoffice.com', 'User', NOW())");
                        coreUserSecurityRows.Add($"(1, '{user}', '{hasher.ComputeHash(user)}')");
                        index++;
                    }

                    commandCoreUser.Append(string.Join(",", coreUserRows));
                    commandUserSecurity.Append(string.Join(",", coreUserSecurityRows));
                    commandCoreUser.Append(";");
                    commandUserSecurity.Append(";");

                    using (MySqlCommand mySqlCommand = new MySqlCommand(commandCoreUser.ToString(), mySqlConnection))
                    {
                        mySqlCommand.CommandType = CommandType.Text;
                        mySqlCommand.ExecuteNonQuery();
                    }

                    using (MySqlCommand sqlCommand = new MySqlCommand(commandUserSecurity.ToString(), mySqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
