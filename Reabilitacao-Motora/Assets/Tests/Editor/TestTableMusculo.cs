using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Text.RegularExpressions;
using musculo;
using DataBaseAttributes;
using Mono.Data.Sqlite;
using System.Data;


/**
* Cria um novo Musculo no banco de dados.
*/
namespace Tests
{
	public class TestTableMuscle
	{
		[SetUp]
		public void SetUp()
		{
			GlobalController.test = true;
			GlobalController.Initialize();
		}

		[Test]
		public void TestMusculoCreate ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				// tabela sendo criada no SetUp, no "Initialize" da GlobalController

				var check = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='MUSCULO';";

				var result = 0;

				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									result = reader.GetInt32(0);
								}
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}

				Assert.AreEqual (result, 1);

				conn.Dispose();
				conn.Close();
			}
			return;
		}

		[Test]
		public void TestMusculoDrop ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Drop();

				var check = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='MUSCULO';";

				var result = 0;

				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									result = reader.GetInt32(0);
								}
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}

				Assert.AreEqual (result, 0);

				conn.Dispose();
				conn.Close();
			}
			return;
		}

		[Test]
		public void TestMusculoInsert ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Insert("bíceps");
				Musculo.Insert("tríceps");
				Musculo.Insert("quadríceps");

				var check = "SELECT * FROM MUSCULO;";

				var id = 0;
				var result = "";
				int i = 1;

				string[] x = new string[] {"", "b", "tr", "quadr"};
				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									id = reader.GetInt32(0);
									Assert.AreEqual (id, i);
								}

								if (!reader.IsDBNull(1))
								{
									result = reader.GetString(1);
									string z = x[i] + "íceps";
									Assert.AreEqual (result, z);
								}

								i++;
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}
				conn.Dispose();
				conn.Close();
			}
			return;
		}

		[Test]
		public void TestMusculoUpdate ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Insert("bíceps");
				Musculo.Insert("tríceps");
				Musculo.Insert("quadríceps");

				Musculo.Update (3, "quintríceps");
				Musculo.Update (2, "quadríceps");

				var check = "SELECT * FROM MUSCULO;";

				var id = 0;
				var result = "";
				int i = 1;

				string[] x = new string[] {"", "b", "quadr", "quintr"};

				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									id = reader.GetInt32(0);
									Assert.AreEqual (id, i);
								}

								if (!reader.IsDBNull(1))
								{
									result = reader.GetString(1);
									string z = x[i] + "íceps";
									Assert.AreEqual (result, z);
								}

								i++;
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}
				conn.Dispose();
				conn.Close();
			}
			return;
		}

		[Test]
		public void TestMusculoRead ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Insert("bíceps");
				Musculo.Insert("tríceps");
				Musculo.Insert("quadríceps");

				List<Musculo> allMuscs = Musculo.Read();
				
				string[] x = new string[] {"", "b", "tr", "quadr"};

				for (int i = 1; i <= allMuscs.Count; ++i)
				{
					Assert.AreEqual (allMuscs[i-1].idMusculo, i);
					Assert.AreEqual (allMuscs[i-1].nomeMusculo, (x[i]+"íceps"));
				}

				conn.Dispose();
				conn.Close();
			}

			return;
		}

		[Test]
		public void TestMusculoReadValue ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Insert("bíceps");
				Musculo.Insert("tríceps");
				Musculo.Insert("quadríceps");

				string[] x = new string[] {"", "b", "tr", "quadr"};

				for (int i = 1; i <= 3; ++i)
				{
					Musculo musculo = Musculo.ReadValue(i);
					Assert.AreEqual (musculo.idMusculo, i);
					Assert.AreEqual (musculo.nomeMusculo, (x[i]+"íceps"));
				}

				conn.Dispose();
				conn.Close();
			}

			return;
		}

		[Test]
		public void TestMusculoDeleteValue ()
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				Musculo.Insert("bíceps");

				var check = "SELECT EXISTS(SELECT 1 FROM 'MUSCULO' WHERE \"idMusculo\" = \"1\" LIMIT 1)";

				var result = 0;
				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									result = reader.GetInt32(0);
								}
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}

				Assert.AreEqual (result, 1);
				Musculo.DeleteValue(1);

				result = 0;
				using (var cmd = new SqliteCommand(check, conn))
				{
					using (IDataReader reader = cmd.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								if (!reader.IsDBNull(0))
								{
									result = reader.GetInt32(0);
								}
							}
						}
						finally
						{
							reader.Dispose();
							reader.Close();
						}
					}
					cmd.Dispose();
				}

				Assert.AreEqual (result, 0);

				conn.Dispose();
				conn.Close();
			}
			return;
		}

		[TearDown]
		public static void AfterEveryTest ()
		{
			SqliteConnection.ClearAllPools();

			GC.Collect();
			GC.WaitForPendingFinalizers();

			GlobalController.DropAll();
		}
	}
}
