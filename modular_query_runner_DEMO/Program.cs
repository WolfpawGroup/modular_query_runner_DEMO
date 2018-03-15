using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Xml;

namespace modular_query_runner_DEMO
{
	class Program
	{
		public static List<string> queryLst = new List<string>();
		public static int posX = 0;
		public static int posY = 0;
		public static int currentlySelectedSQL = 0;
		public static List<column> columns = new List<column>();
		public static List<List<row>> rows = new List<List<row>>();

		public static object SQLiteConection { get; private set; }

		static void Main(string[] args)
		{
			if (Directory.Exists("queries"))
			{
				foreach(String s in Directory.GetFiles("queries","*.q", SearchOption.AllDirectories))
				{
					queryLst.Add(s.Substring(s.LastIndexOf("\\") + 1));
				}
			}

			listActiveQueries();
			getpos();
			selectQuery();
			printOutResult();
			Console.ReadLine();
		}

		public static string pad(string txt)
		{
			return txt.PadRight(30, ' ');
		}

		public static void printOutResult()
		{
			Console.WriteLine("\r\n==================== PRINT RESULTS ====================\r\n");
			
			if (rows.Count > 0)
			{
				Console.BufferHeight += rows.Count * 2;

				int wid = 0;
				foreach (row r in rows[0])
				{
					wid += pad(r.name).Length;
				}

				Console.BufferWidth = wid;
				Console.WindowWidth = wid;

				foreach (row r in rows[0])
				{
					Console.Write(pad(r.name));
				}

				Console.WriteLine("");

				for (int i = 0; i < wid; i++) { Console.Write("-"); }

				Console.WriteLine("");

				foreach (List<row> lst in rows)
				{
					foreach (row r in lst)
					{
						Console.Write(pad(r.value));
					}

					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("No data returned by the query...");
			}
		}

		public static void printSelectedQuery()
		{
			Console.Clear();
			string q = File.ReadAllText("queries\\" + queryLst[currentlySelectedSQL]);
			string query = "";

			XmlDocument xd = new XmlDocument();
			xd.LoadXml(q);
			query = xd["DATA"]["SQL"].InnerText;

			foreach(XmlNode x in xd.GetElementsByTagName("column"))
			{
				int tmp = 0;
				int.TryParse(x["type"].Attributes["value"].Value, out tmp);
				column c = new column()
				{
					name = x["name"].Attributes["value"].Value,
					column_name = x["column_name"].Attributes["value"].Value,
					type = (types)tmp
				};

				columns.Add(c);
			}

			Console.WriteLine(query);
			Console.WriteLine("\r\n\r\n==================== READ DATA ====================\r\n");

			SQLiteConnection sqlc = c_DBHandler.connecttodb();
			if (sqlc != null && sqlc.State == System.Data.ConnectionState.Open)
			{
				var r = c_DBHandler.getData(sqlc, query);
				
				while (r.Read())
				{
					List<row> _row = new List<row>();
					
					foreach(column c in columns)
					{
						row rr = new row();
						rr.name = c.name;

						if (r.IsDBNull(r.GetOrdinal(c.column_name)))
						{
							rr.value = "<NULL>";
						}
						else
						{
							if (c.type == types.INTEGER)
							{
								rr.value = r.GetInt32(r.GetOrdinal(c.column_name)).ToString();
							}
							else
							{
								rr.value = r.GetString(r.GetOrdinal(c.column_name));
							}
						}

						_row.Add(rr);
					}

					rows.Add(_row);
				}
			}
			Console.WriteLine("--DATA READ--");
		}

		public static void getpos()
		{
			posX = Console.CursorLeft;
			posY = Console.CursorTop;
		}

		public static void setpos(int x, int y)
		{
			Console.CursorLeft = x;
			Console.CursorTop = y;
		}

		public static void listActiveQueries()
		{
			foreach(String s in queryLst) { Console.WriteLine(s); }
			Console.Write("\r\nSelect the query you'd like to run\r\n> ");
		}

		public static void writeCurrentSelection()
		{
			setpos(posX, posY);
			int l = queryLst.OrderBy(x => x.Length).First().Length;
			for(int i = 0; i < l + 1; i++) { Console.Write(" "); }
			setpos(posX, posY);
			Console.Write(queryLst[currentlySelectedSQL]);
		}

		public static void selectQuery()
		{
			string s = "";
			writeCurrentSelection();
			while (s != ConsoleKey.Enter.ToString())
			{
				s = Console.ReadKey().Key.ToString();
				if (s == ConsoleKey.UpArrow.ToString())
				{
					if (currentlySelectedSQL == 0) { currentlySelectedSQL = queryLst.Count - 1; }
					else { currentlySelectedSQL--; }
					writeCurrentSelection();
				}
				else if (s == ConsoleKey.DownArrow.ToString())
				{
					if (currentlySelectedSQL == queryLst.Count - 1) { currentlySelectedSQL = 0; }
					else { currentlySelectedSQL++; }
					writeCurrentSelection();
				}
				else if (s == ConsoleKey.Enter.ToString())
				{
					printSelectedQuery();
				}
				else
				{
					writeCurrentSelection();
				}

			}
		}


	}

	public class column
	{
		public string name { get; set; }
		public string column_name { get; set; }
		public types type { get; set; }
	}

	public class row
	{
		public string name { get; set; }
		public string value { get; set; }
	}
	

	public enum types
	{
		TEXT,
		INTEGER
	}
}
