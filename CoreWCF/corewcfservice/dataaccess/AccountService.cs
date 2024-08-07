using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using corewcfservice.IServices;
using corewcfservice.model;
namespace corewcfservice.dataaccess
{
    [CoreWCF.ServiceContract]
    public interface IAccountService
    {
        [CoreWCF.OperationContract]
        List<Account> GetAccounts();

        [CoreWCF.OperationContract]
        Account GetAccount(int id);

        [CoreWCF.OperationContract]
        void AddAccount(Account account);

        [CoreWCF.OperationContract]
        void UpdateAccount(Account account);

        [CoreWCF.OperationContract]
        void DeleteAccount(int id);
    }



    public class AccountService : IAccountService
    {
        private readonly string _connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;Integrated Security=True";

        public List<Account> GetAccounts()
        {
            var accounts = new List<Account>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Account_table", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new Account
                            {
                                AccId = reader.GetInt32(0),
                                AccountNumber = reader.GetString(1),
                                AccountStatus_id = reader.GetInt32(2),
                                CreatedDate = reader.GetDateTime(3),
                                CreatedBy = reader.GetString(4),
                                UpdatedDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                UpdatedBy = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return accounts;
        }

        public Account GetAccount(int id)
        {
            Account account = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Account_table WHERE AccId = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account = new Account
                            {
                                AccId = reader.GetInt32(0),
                                AccountNumber = reader.GetString(1),
                                AccountStatus_id = reader.GetInt32(2),
                                CreatedDate = reader.GetDateTime(3),
                                CreatedBy = reader.GetString(4),
                                UpdatedDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                UpdatedBy = reader.IsDBNull(6) ? null : reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return account;
        }

        public void AddAccount(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Account_table (AccId, AccountNumber, AccountStatus_id, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) VALUES (@id, @number, @status, @createdDate, @createdBy, @updatedDate, @updatedBy)", connection))
                {
                    command.Parameters.AddWithValue("@id", account.AccId);
                    command.Parameters.AddWithValue("@number", account.AccountNumber);
                    command.Parameters.AddWithValue("@status", account.AccountStatus_id);
                    command.Parameters.AddWithValue("@createdDate", account.CreatedDate);
                    command.Parameters.AddWithValue("@createdBy", account.CreatedBy);
                    command.Parameters.AddWithValue("@updatedDate", (object)account.UpdatedDate ?? CoreWCF.DBNull.Value);
                    command.Parameters.AddWithValue("@updatedBy", (object)account.UpdatedBy ?? CoreWCF.DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAccount(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Account_table SET AccountNumber = @number, AccountStatus_id = @status, UpdatedDate = @updatedDate, UpdatedBy = @updatedBy WHERE AccId = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", account.AccId);
                    command.Parameters.AddWithValue("@number", account.AccountNumber);
                    command.Parameters.AddWithValue("@status", account.AccountStatus_id);
                    command.Parameters.AddWithValue("@updatedDate", (object)account.UpdatedDate ?? CoreWCF.DBNull.Value);
                    command.Parameters.AddWithValue("@updatedBy", (object)account.UpdatedBy ?? CoreWCF.DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAccount(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
