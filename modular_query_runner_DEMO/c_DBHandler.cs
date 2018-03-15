using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace modular_query_runner_DEMO
{
	public static class c_DBHandler
	{
		public static SQLiteConnection connecttodb()
		{
			SQLiteConnection sqlc = new SQLiteConnection("Data Source=demo.sqlite;Version=3;");
			sqlc.Open();
			return sqlc;
		}

		public static SQLiteDataReader getData(SQLiteConnection sqlc, string command)
		{
			SQLiteDataReader r = null;

			SQLiteCommand sql = new SQLiteCommand(command, sqlc);

			r = sql.ExecuteReader();

			return r;
		}



	}
}
