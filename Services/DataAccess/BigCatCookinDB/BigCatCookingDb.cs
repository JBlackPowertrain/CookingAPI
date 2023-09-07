using BigCatCookinAPI.Models.GPT.GPTResponses;
using BigCatCookinAPI.Models.Recipes.DAO;
using BigCatCookinAPI.Services.Interface;
using BigCatCookinAPI.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;
using static BigCatCookinAPI.Services.DataAccess.BigCatCookinDB.IBigCatCookingDb;

namespace BigCatCookinAPI.Services.DataAccess.BigCatCookinDB;

public class BigCatCookingDb : IBigCatCookingDb
{
    private readonly IBigCatCookinConfig _webConfig;

    private readonly Databases connectionStrings; 
    public Databases ConnectionStrings => connectionStrings;

    public BigCatCookingDb(IBigCatCookinConfig webConfig)
    {
        _webConfig = webConfig;
        connectionStrings = new Databases(_webConfig.CouponDBConnString,
            _webConfig.StoreDBConnString,
            _webConfig.RecipeDBConnString,
            _webConfig.UserDBConnString);
    }

    //TODO: Change IEnumerable Key Value to IEnumerable Dictionary
    public bool BulkInsertTransactions(string dbConnection, IEnumerable<string> storedProcedures, IEnumerable<Dictionary<string, object>> parameters)
    {
        return CommitBulkInsert(GetBulkCommands(dbConnection, storedProcedures, parameters));
    }

    //TODO: Change IEnumerable Key Value to IEnumerable Dictionary
    IList<SqlCommand> GetBulkCommands(string dbConnection, IEnumerable<string> storedProcedures, IEnumerable<Dictionary<string, object>> parameters)
    {
        if(storedProcedures.Count() != parameters.Count())
        {
            return null; 
        }

        string[] _storedProcedures = storedProcedures.ToArray();
        Dictionary<string, object>[] keyValuePairs = parameters.ToArray();

        try
        {
            using (var dbConn = new SqlConnection(dbConnection))
            {
                List<SqlCommand> commands = new List<SqlCommand>();
                dbConn.Open();


                try
                {
                    for(int x = 0; x < _storedProcedures.Length; x++)
                    {
                        Dictionary<string, object> dict = keyValuePairs[x];
                        SqlTransaction transaction = dbConn.BeginTransaction();

                        SqlCommand spCommand = new SqlCommand(_storedProcedures[x], dbConn);
                        spCommand.Transaction = transaction;

                        spCommand.CommandType = System.Data.CommandType.StoredProcedure; 

                        foreach (string key in dict.Keys)
                        {
                            spCommand.Parameters.Add(new SqlParameter(key, dict[key]));
                        }
                        spCommand.ExecuteNonQuery();
                        commands.Add(spCommand);
                    }
                    return commands;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
   
    public bool CommitBulkInsert(IList<SqlCommand> commands)
    {
        List<SqlCommand> commitedCommands = new List<SqlCommand>();
        try
        {
            foreach(SqlCommand command in commands)
            {
                command.Transaction.Commit(); 
                commitedCommands.Add(command);
            }
            return true; 
        }
        catch (Exception e)
        {
            commitedCommands.Reverse();
            foreach (SqlCommand committed in commitedCommands)
            {
                committed.Transaction.Rollback();
            }
            return false; 
        }
    }

    //TODO: Implement
    public bool Delete(string dbConnection, string storedProcedure, Dictionary<string, object> parameters)
    {
        throw new NotImplementedException();
    }

    public T GetSingle<T>(string dbConnection, string storedProcedure, Dictionary<string, object> parameters, Type type)
    {
        try
        {
            string[] fields = DataUtilities.GetPropertiesFromObject(type);
            using (var dbConn = new SqlConnection(dbConnection))
            {
                try
                {
                    dbConn.Open();
                    SqlTransaction transaction = dbConn.BeginTransaction();

                    SqlCommand spCommand = new SqlCommand(storedProcedure, dbConn);
                    spCommand.Transaction = transaction;

                    spCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (string key in parameters.Keys)
                    {
                        spCommand.Parameters.Add(new SqlParameter(key, parameters[key]));
                    }

                    SqlDataReader reader = spCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();
                        foreach (string col in fields)
                        {
                            result.Add(col, reader[col]);
                        }

                        object obj = FormatterServices.GetUninitializedObject(type);
                        foreach (string key in result.Keys)
                        {
                            obj.GetType().GetProperty(key).SetValue(obj, result[key], null);
                        }
                        return ((T)obj);
                    }
                }
                catch (Exception e)
                {
                    dbConn.Close();
                    return default(T);
                }
            }
        }
        catch (Exception e)
        {
            return default(T);
        }
        return default(T);
    }

    public List<T> Get<T>(string dbConnection, string storedProcedure, Dictionary<string, object> parameters, Type type)
    {
        try
        {
            string[] fields = DataUtilities.GetPropertiesFromObject(type); 
            using (var dbConn = new SqlConnection(dbConnection))
            {
                try
                {
                    dbConn.Open();
                    SqlTransaction transaction = dbConn.BeginTransaction();

                    SqlCommand spCommand = new SqlCommand(storedProcedure, dbConn); 
                    spCommand.Transaction = transaction;

                    spCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (string key in parameters.Keys)
                    {
                        spCommand.Parameters.Add(new SqlParameter(key, parameters[key]));
                    }

                    List<T> resultSet = new List<T>(); 
                    SqlDataReader reader = spCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>(); 
                        foreach(string col in fields)
                        {
                            result.Add(col, reader[col]);  
                        }

                        object obj = FormatterServices.GetUninitializedObject(type);
                        foreach (string key in result.Keys)
                        {
                            if (result[key].GetType() == typeof(double))
                            {
                                obj.GetType().GetProperty(key).SetValue(obj, Convert.ToSingle(result[key]), null);
                            }
                            else
                            {
                                obj.GetType().GetProperty(key).SetValue(obj, result[key], null);
                            }
                        }
                        resultSet.Add((T)obj);
                    }
                    return resultSet;
                }
                catch(Exception e)
                {
                    dbConn.Close();
                    return new List<T>(); 
                }
            }
        }
        catch (Exception e)
        {
            return new List<T>();
        }
    }

    public bool Insert(string dbConnection, string storedProcedure, Dictionary<string, object> parameters)
    {
        try
        {
            using (var dbConn = new SqlConnection(dbConnection))
            {
                dbConn.Open();
                SqlTransaction transaction = dbConn.BeginTransaction();

                SqlCommand spCommand = new SqlCommand(storedProcedure, dbConn);
                spCommand.Transaction = transaction;

                spCommand.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (string key in parameters.Keys)
                {
                    spCommand.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
                spCommand.ExecuteNonQuery();
                transaction.Commit();
                dbConn.Close();
                return true; 
            }
        }
        catch (Exception e)
        {
            return false; 
        }
    }

    public Guid InsertGetId(string dbConnection, string storedProcedure, Dictionary<string, object> parameters)
    {
        try
        {
            using (var dbConn = new SqlConnection(dbConnection))
            {
                try
                {
                    dbConn.Open();
                    SqlTransaction transaction = dbConn.BeginTransaction();

                    SqlCommand spCommand = new SqlCommand(storedProcedure, dbConn);
                    spCommand.Transaction = transaction;

                    spCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (string key in parameters.Keys)
                    {
                        spCommand.Parameters.Add(new SqlParameter(key, parameters[key]));
                    }

                    SqlDataReader reader = spCommand.ExecuteReader();

                    Guid returnVal = new Guid(); 

                    while (reader.Read())
                    {
                         returnVal = (Guid)reader[0];
                    }
                    reader.Close();
                    transaction.Commit(); 
                    dbConn.Close();

                    return returnVal;
                }
                catch (Exception e)
                {
                    dbConn.Close();
                    throw e;
                }
            }
        }
        catch (Exception e)
        {
            throw e; 
        }
    }
}
