using Common.AppConfiguration.Common;
using HRMS_Core.DataAccessLayer.DatabaseHelper.Datablase.Common;
using System.Data;
using System.Data.SqlClient;
using static Common.Enum;

namespace HRMS_Core.DataAccessLayer.DatabaseHelper
{
	namespace Datablase
	{
		#region Namespace - Common
		namespace Common
		{
			public class SQLParameter
			{
				string _parameterName = "";
				object _parameterValue = null;
				ParameterDirection _direction = ParameterDirection.Input;
				SqlDbType? _dbType = null;
				int _size = -1;
				private static ConnectionType _conType = ConnectionType.WebConn;

				public SQLParameter()
				{

				}


				public SQLParameter(string parameterName, object parameterValue)
				{
					_parameterName = parameterName;
					_parameterValue = parameterValue;
				}

				public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType)
				{
					_parameterName = parameterName;
					_parameterValue = parameterValue;
					_dbType = dbType;
				}

				public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType, int size)
				{
					_parameterName = parameterName;
					_parameterValue = parameterValue;
					_dbType = dbType;
					_size = size;
				}

				public SQLParameter(string parameterName, object parameterValue, ParameterDirection paramDirection)
				{
					_parameterName = parameterName;
					_parameterValue = parameterValue;
					_direction = paramDirection;
				}

				public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection paramDirection)
				{
					_parameterName = parameterName;
					_parameterValue = parameterValue;
					_dbType = dbType;
					_direction = paramDirection;
				}

				public string ParameterName
				{
					get { return _parameterName; }
					set { _parameterName = value; }
				}

				public object ParameterValue
				{
					get { return _parameterValue; }
					set { _parameterValue = value; }
				}

				public SqlDbType? DBType
				{
					get { return _dbType; }
					set { _dbType = value; }
				}

				public int Size
				{
					get { return _size; }
					set { _size = value; }
				}

				public ParameterDirection ParameterDirection
				{
					get { return _direction; }
					set { _direction = value; }
				}

				public static ConnectionType contype
				{
					get { return _conType; }
					set { _conType = value; }
				}

				public ConnectionType connectiontype
				{
					get { return SQLParameter.contype; }
				}

			}

			public class ParameterList : List<SQLParameter>
			{
				public ParameterList()
				{

				}

				public SQLParameter GetParameterByName(string parameterName)
				{
					return this.Find(delegate (Common.SQLParameter p) { return p.ParameterName.ToUpper() == parameterName.ToUpper(); });
				}

				public int GetParameterIndexByName(string parameterName)
				{
					return this.FindIndex(delegate (Common.SQLParameter p) { return p.ParameterName.ToUpper() == parameterName.ToUpper(); });
				}
			}

			public class Key
			{
				string _key;

				public Key()
				{

				}

				public Key(string keyName)
				{
					_key = keyName;
				}

				public string KeyName
				{
					get { return _key; }
					set { _key = value; }
				}
			}

			public class ForeignKey
			{
				Key _sourceKey = new Key();
				Key _targetKey = new Key();
				int _primaryKeyQueryIndex = -1;

				public ForeignKey()
				{

				}

				public ForeignKey(string sourceKey, string targetKey)
				{
					_sourceKey.KeyName = sourceKey;
					_targetKey.KeyName = targetKey;
				}

				public ForeignKey(string sourceKey, string targetKey, int primaryKeyQueryIndex)
				{
					_sourceKey.KeyName = sourceKey;
					_targetKey.KeyName = targetKey;
					_primaryKeyQueryIndex = primaryKeyQueryIndex;
				}

				public string SourceKey
				{
					get { return _sourceKey.KeyName; }
					set { _sourceKey.KeyName = value; }
				}

				public string TargetKey
				{
					get { return _targetKey.KeyName; }
					set { _targetKey.KeyName = value; }
				}

				public int PrimaryKeyQueryIndex
				{
					get { return _primaryKeyQueryIndex; }
					set { _primaryKeyQueryIndex = value; }
				}
			}

			public class ForeignKeyList : List<ForeignKey>
			{
				public ForeignKeyList()
				{

				}
			}

			public class PrimaryKeyList : List<Key>
			{
				public PrimaryKeyList()
				{

				}
			}

			public class TransactionQuery
			{
				string _procedureName;
				ParameterList _parameterList = new ParameterList();
				PrimaryKeyList _primaryKeyList = new PrimaryKeyList();
				ForeignKeyList _foreignKeyList = new ForeignKeyList();
				int _primaryKeyQueryIndex = -1;

				public TransactionQuery()
				{

				}

				public string ProcedureName
				{
					get { return _procedureName; }
					set { _procedureName = value; }
				}

				public ParameterList SQLParameters
				{
					get { return _parameterList; }
					set { _parameterList = value; }
				}

				public PrimaryKeyList PrimaryKeyList
				{
					get { return _primaryKeyList; }
					set { _primaryKeyList = value; }
				}

				public ForeignKeyList ForeignKeyList
				{
					get { return _foreignKeyList; }
					set { _foreignKeyList = value; }
				}

				
			}

			public class TransactionQueryList : List<TransactionQuery>
			{
				public TransactionQueryList()
				{

				}
			}

			
		}
		#endregion
		namespace DatabaseHelper
		{
			public interface IExecuteQuery
			{
				//SqlDataAdapter ExecuteDataAdapter(string procedureName, Common.ParameterList SQLParam);
				DataTableReader ExecuteReader(string procedureName, Common.ParameterList SQLParam);
				DataTableReader ExecuteReader(string SQL);
				void ExecuteNonQuery(string procedureName, Common.ParameterList SQLParam);
			}

			public class ExecuteQuery : IExecuteQuery
			{
				private static string _connectionString = "";

				private IAppConfiguration _appConfiguration;

				public ExecuteQuery(IAppConfiguration appConfiguration)
				{
					_appConfiguration = appConfiguration;
					//
					// TODO: Add constructor logic here
					//
				}
				public  DataTableReader ExecuteReader(string procedureName, Common.ParameterList SQLParam)
				{
					DataTableReader reader;
					DataSet ds;

					try
					{
						ds = ExecuteDataSet(procedureName, SQLParam);
						reader = ds.CreateDataReader();
						return reader;
					}
					catch (Exception)
					{
						throw;
					}
				}
				public  DataSet ExecuteDataSet(string procedureName, Common.ParameterList SQLParam)
				{
					SqlDataAdapter ad;
					DataSet ds = new DataSet();

					try
					{
						ad = ExecuteDataAdapter(procedureName, SQLParam);
						ad.Fill(ds);
						return ds;
					}
					catch (Exception)
					{
						throw;
					}
				}
				public SqlDataAdapter ExecuteDataAdapter(string procedureName, Common.ParameterList SQLParam)
				{
					SqlConnection conn = null;
					SqlDataAdapter ad;

					try
					{
						conn = new SqlConnection(_appConfiguration.ConnectionString());
						conn.Open();
						ad = new SqlDataAdapter();
						ad.SelectCommand = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam);
						conn.Close();
						//SITI_API.Core.Database.Common.SQLParameter.ConType = ConnectionType.WebConn;

						return ad;
					}

					catch (Exception)
					{
						throw;
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
							conn.Close();
						//SITI_API.Core.Database.Common.SQLParameter.ConType = ConnectionType.WebConn;
					}
				}
				private static SqlCommand BuildSqlCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string procedureName, Common.ParameterList SQLParam)
				{
					SqlCommand cmd = BuildBaseSqlCommand(conn, transaction, cmdType, procedureName, SQLParam);
					return cmd;
				}
				private static SqlCommand BuildBaseSqlCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string procedureName, Common.ParameterList SQLParam)
				{
					SqlCommand cmd = new SqlCommand();
					cmd.CommandType = cmdType;
					cmd.Connection = conn;
					cmd.Transaction = transaction;
					cmd.CommandText = procedureName;
					foreach (Common.SQLParameter param in SQLParam)
					{
						cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.ParameterValue));

						if (param.DBType != null)
							cmd.Parameters[cmd.Parameters.Count - 1].SqlDbType = (SqlDbType)param.DBType;

						if (param.Size != -1)
							cmd.Parameters[cmd.Parameters.Count - 1].Size = param.Size;

						cmd.Parameters[cmd.Parameters.Count - 1].Direction = param.ParameterDirection;
					}
					return cmd;
				}
				public  DataTable ExecuteDataTable(string SQL, Common.ParameterList SQLParam)
				{
					SqlDataAdapter ad;
					DataTable dt = new DataTable();

					try
					{
						ad = ExecuteDataAdapter(SQL, SQLParam);
						ad.Fill(dt);
						return dt;
					}
					catch (Exception)
					{
						throw;
					}
				}
				public  DataTableReader ExecuteReader(string SQL)
				{
					DataTableReader reader;
					DataSet ds;

					try
					{
						ds = ExecuteDataSet(SQL);
						reader = ds.CreateDataReader();
						return reader;
					}
					catch (Exception)
					{
						throw;
					}
				}

				public  DataSet ExecuteDataSet(string SQL)
				{
					SqlConnection conn = null;
					SqlDataAdapter ad;
					DataSet ds = new DataSet();

					try
					{
						conn = new SqlConnection(_appConfiguration.ConnectionString());
						conn.Open();
						ad = new SqlDataAdapter(SQL, conn);
						ad.SelectCommand.CommandTimeout = 120;
						ad.Fill(ds);
						conn.Close();
						SQLParameter.contype = ConnectionType.WebConn;
						return ds;
					}
					catch (Exception)
					{
						throw;
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
							conn.Close();
						SQLParameter.contype = ConnectionType.WebConn;
					}
				}

				public  void ExecuteNonQuery(string procedureName, Common.ParameterList SQLParam)
				{
					SqlConnection conn = new SqlConnection();

					try
					{
						using (conn = new SqlConnection(_appConfiguration.ConnectionString()))
						{
							conn.Open();
							using (SqlCommand cmd = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam))
							{
								cmd.ExecuteNonQuery();
								StoreParameterOutputValues(cmd, SQLParam);
							}
							conn.Close();
							//Renaissance.Database.Common.SQLParameter.ConType = ConnectionType.WebConn;
						}
					}
					catch (Exception)
					{
						throw;
					}
					finally
					{
						if (conn.State != ConnectionState.Closed)
							conn.Close();
						//Renaissance.Database.Common.SQLParameter.ConType = ConnectionType.WebConn;
					}
				}

				private static void StoreParameterOutputValues(SqlCommand cmd, Common.ParameterList SQLParam)
				{
					foreach (SqlParameter param in cmd.Parameters)
					{
						if (param.Direction != ParameterDirection.Input)
							SQLParam.GetParameterByName(param.ParameterName).ParameterValue = param.Value;
					}
				}
			}
		}

	}
	internal class DatabaseHelper
	{
	}
}
