
using CustomerApi.Classes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomerApi.Models.CustomerModel;


namespace Crud.Classes
{
    public class DapperTransaction
    {
        
        private string cn;

        public DapperTransaction()
        {
            var config = new Config();
            cn = config.Credentials().Constring;
        }
        public bool InsertData(Customer cus)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(cn))
                {
                    string query = "INSERT INTO Customer (CustomerNo, Name, Contact) VALUES (@CustomerNo, @Name, @Contact); SELECT CAST(SCOPE_IDENTITY() as int);";
                    int insertedId = db.ExecuteScalar<int>(query, cus);

                    return insertedId > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool UpdateData(Customer user)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(cn))
                {
                    string query = "UPDATE Customer SET Name = @Name, Contact = @Contact WHERE CustomerNo = @CustomerNo";
                    db.Execute(query, user);
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeleteData(string cusID)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                string query = "DELETE FROM Customer WHERE CustomerNo = @CustomerNo";
                int rowsAffected = db.Execute(query, new { CustomerNo = cusID });

                return rowsAffected > 0; // Returns true if at least one row was deleted
            }
        }

        public Customer GetUserById(string cusID)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                string query = "SELECT * from Customer WHERE CustomerNo = @CustomerNo";
                //string query = "SELECT Id, Name, Email, Address FROM Users WHERE CustomerNo = @CustomerNo";
                return db.QueryFirstOrDefault<Customer>(query, new { CustomerNo = cusID });
            }
        }

        public List<Customer> GetCustomerList()
        {
            try
            {
                using (var connection = new SqlConnection(cn))
                {
                    string sql = "SELECT * FROM Customer";
                    var students = connection.Query<Customer>(sql).AsList();
                    return students;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
