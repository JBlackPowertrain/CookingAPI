using System.Data.SqlClient;

namespace BigCatCookinAPI.Services.DataAccess.BigCatCookinDB;

public interface IBigCatCookingDb
{

    public readonly struct Databases
    {
        public readonly string CouponDatabase;
        public readonly string GrocerDatabase;
        public readonly string RecipeDatabase;
        public readonly string UserDatabase;

        public Databases(string couponDatabase, 
            string grocerDatabase, 
            string recipeDatabase, 
            string userDatabase)
        {
            this.CouponDatabase = couponDatabase;
            this.GrocerDatabase = grocerDatabase;
            this.RecipeDatabase = recipeDatabase;   
            this.UserDatabase = userDatabase;
        }
    }

    public Databases ConnectionStrings { get; }

    public List<T> Get<T>(string dbConnection, string storedProcedure, Dictionary<string, object> parameters, Type type);
    public T GetSingle<T>(string dbConnection, string storedProcedure, Dictionary<string, object> parameters, Type type);
    public bool Insert(string dbConnection, string storedProcedure, Dictionary<string, object> parameters);

    public Guid InsertGetId(string dbConnection, string storedProcedure, Dictionary<string, object> parameters);

    public bool Delete(string dbConnection, string storedProcedure, Dictionary<string, object> parameters);

    public bool BulkInsertTransactions(string dbConnection, IEnumerable<string> storedProcedures, IEnumerable<Dictionary<string, object>> parameters);
    public bool CommitBulkInsert(IList<SqlCommand> commands);
}
