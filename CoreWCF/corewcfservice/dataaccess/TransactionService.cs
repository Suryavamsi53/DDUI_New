using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
 using corewcfservice.IServices;
 using corewcfservice.model;

namespace corewcfservice.dataaccess
{
    public class TransactionService : ITransactionService
    {
        private readonly string connectionString = "Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True";

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Transaction_Table", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Transaction
                            {
                                TransID = reader.GetInt32(0),
                                AccountID = reader.GetInt32(1),
                                TransTypeID = reader.GetInt32(2),
                                Amount = reader.GetDecimal(3),
                                Date = reader.GetDateTime(4),
                                Transaction_type = reader.GetString(5),
                                CreatedDate = reader.GetDateTime(6),
                                CreatedBy = reader.GetString(7),
                                UpdatedDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                                UpdatedBy = reader.IsDBNull(9) ? null : reader.GetString(9)
                            });
                        }
                    }
                }
            }

            return transactions;
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            Transaction transaction = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Transaction_Table WHERE TransID = @TransID", connection))
                {
                    command.Parameters.AddWithValue("@TransID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            transaction = new Transaction
                            {
                                TransID = reader.GetInt32(0),
                                AccountID = reader.GetInt32(1),
                                TransTypeID = reader.GetInt32(2),
                                Amount = reader.GetDecimal(3),
                                Date = reader.GetDateTime(4),
                                Transaction_type = reader.GetString(5),
                                CreatedDate = reader.GetDateTime(6),
                                CreatedBy = reader.GetString(7),
                                UpdatedDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                                UpdatedBy = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };
                        }
                    }
                }
            }

            return transaction;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Transaction_Table (TransID, AccountID, TransTypeID, Amount, Date, Transaction_type, CreatedDate, CreatedBy) VALUES (@TransID, @AccountID, @TransTypeID, @Amount, @Date, @Transaction_type, @CreatedDate, @CreatedBy)", connection))
                {
                    command.Parameters.AddWithValue("@TransID", transaction.TransID);
                    command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
                    command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);
                    command.Parameters.AddWithValue("@Date", transaction.Date);
                    command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
                    command.Parameters.AddWithValue("@CreatedDate", transaction.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", transaction.CreatedBy);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Transaction_Table SET AccountID = @AccountID, TransTypeID = @TransTypeID, Amount = @Amount, Date = @Date, Transaction_type = @Transaction_type, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE TransID = @TransID", connection))
                {
                    command.Parameters.AddWithValue("@TransID", transaction.TransID);
                    command.Parameters.AddWithValue("@AccountID", transaction.AccountID);
                    command.Parameters.AddWithValue("@TransTypeID", transaction.TransTypeID);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);
                    command.Parameters.AddWithValue("@Date", transaction.Date);
                    command.Parameters.AddWithValue("@Transaction_type", transaction.Transaction_type);
                    command.Parameters.AddWithValue("@UpdatedDate", transaction.UpdatedDate.HasValue ? (object)transaction.UpdatedDate.Value : CoreWCF.DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedBy", transaction.UpdatedBy ?? (object)CoreWCF.DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteTransactionAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Transaction_Table WHERE TransID = @TransID", connection))
                {
                    command.Parameters.AddWithValue("@TransID", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
