using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using pessoa;
using DataBaseAttributes;

namespace fisioterapeuta
{
  /**
   * Cria relação para cadastro dos fisioterapeutas a serem cadastrados pelo programa.
   */
	public class Fisioterapeuta
	{
		private const int tableId = 1;
		private int IdFisioterapeuta;
		private int IdPessoa;
		private string Login;
		private string Senha;
		private string Regiao;
		private string Crefito;
		private Pessoa Persona;

		public int idFisioterapeuta { get { return IdFisioterapeuta; } set { IdFisioterapeuta = value; }}
		public int idPessoa { get { return IdPessoa; } set { IdPessoa = value; }}
		public string login { get { return Login; } set { Login = value; }}
		public string senha { get { return Senha; } set { Senha = value; }}
		public string regiao { get { return Regiao; } set { Regiao = value; }}
		public string crefito { get { return Crefito; } set { Crefito = value; }}
		public Pessoa persona { get { return Persona; } set { Persona = value; }}

		/**
		 * Classe com todos os atributos de um fisioterapeuta.
		 */
		public Fisioterapeuta(Object[] columns)
		{
			this.idFisioterapeuta = (int)columns[0];
			this.idPessoa = (int)columns[1];
			this.login = (string)columns[2];
			this.senha = (string)columns[3];
			this.regiao = (string)columns[4];
			this.crefito = (string)columns[5];
			this.persona = Pessoa.ReadValue ((int)columns[1]);		
		}

		/**
		 * Cria a relação para fisioterapeuta, contendo um id gerado automaticamente pelo banco como chave primária.
		 */
		public static void Create()
		{
			string query = "CREATE TABLE IF NOT EXISTS FISIOTERAPEUTA (idFisioterapeuta INTEGER primary key AUTOINCREMENT,idPessoa INTEGER not null,login VARCHAR (255) not null,senha VARCHAR (255) not null,regiao VARCHAR (2),crefito VARCHAR (10),foreign key (idPessoa) references PESSOA (idPessoa),constraint crefito_regiao UNIQUE (crefito, regiao), constraint login_senha UNIQUE (login));";
			DataBase.Create(query);
		}

		/**
		 * Função que insere dados necessários para cadastro dos pacientes do fisioterapeuta na relação fisioterapeuta.
		 */
		public static void Insert(int idPessoa,
			string login,
			string senha,
			string regiao,
			string crefito)
		{
			Object[] columns = new Object[] {idPessoa, login, senha, regiao, crefito};
			DataBase.Insert(columns, TablesManager.Tables[tableId].tableName, tableId);
		}

		/**
		 * Função que atualiza dados já cadastrados anteriormente na relação fisioterapeuta.
		 */
		public static void Update(int id,
			int idPessoa,
			string login,
			string senha,
			string regiao,
			string crefito)
		{
			Object[] columns = new Object[] {id, idPessoa, login, senha, regiao, crefito};
			DataBase.Update(columns, TablesManager.Tables[tableId].tableName, tableId);

		}

		/**
		 * Função que lê dados já cadastrados anteriormente na relação fisioterapeuta.
		 */
		public static List<Fisioterapeuta> Read()
		{
			Object[] columns = new Object[] {0, 0, "", "", "", ""};

			List<Fisioterapeuta> physiotherapeuts = DataBase.Read<Fisioterapeuta>(TablesManager.Tables[tableId].tableName, columns);

			return physiotherapeuts;
		}

		public static Fisioterapeuta ReadValue (int id)
		{
			Object[] columns = new Object[] {0, 0, "", "", "", ""};

			Fisioterapeuta physiotherapeut = DataBase.ReadValue<Fisioterapeuta>(TablesManager.Tables[tableId].tableName,
				TablesManager.Tables[tableId].colName[0], id, columns);

			return physiotherapeut;
		}

		public static Fisioterapeuta GetLast ()
		{
			Object[] columns = new Object[] {0, 0, "", "", "", ""};

			Fisioterapeuta physiotherapeut = DataBase.GetLast<Fisioterapeuta>(TablesManager.Tables[tableId].tableName,
				TablesManager.Tables[tableId].colName[0], columns);

			return physiotherapeut;
		}

		/**
		 * Função que deleta dados cadastrados anteriormente na relação fisioterapeuta.
		 */
		public static void DeleteValue(int id)
		{
			DataBase.DeleteValue (tableId, id);
		}

		/**
		 * Função que apaga a relação fisioterapeuta inteira de uma vez.
		 */
		 public static void Drop()
		 {
		 	DataBase.Drop (tableId);
		 }
	}
}
