using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using corewcfservice;
using System.ServiceModel;
using corewcfservice.model;
 using corewcfservice.IServices;



namespace corewcfservice.dataaccess{
public class AddressService : IAddressService
{
    private readonly string connectionString = "Server=SURYA\\SQLEXPRESS; Database=VuejsApp; Integrated Security=True";

    public async Task<List<Address>> GetAddressesAsync()
    {
        var addresses = new List<Address>();

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT * FROM Address_table", connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        addresses.Add(new Address
                        {
                            AddressID = reader.GetInt32(0),
                            AccountID = reader.GetInt32(1),
                            AddressTypeID = reader.GetInt32(2),
                            //Address = reader.GetString(3),
                            CreatedDate = reader.GetDateTime(4),
                            CreatedBy = reader.GetString(5),
                            UpdatedDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            UpdatedBy = reader.IsDBNull(7) ? null : reader.GetString(7)
                        });
                    }
                }
            }
        }

        return addresses;
    }

    public async Task<Address> GetAddressAsync(int id)
    {
        Address address = null;

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT * FROM Address_table WHERE AddressID = @AddressID", connection))
            {
                command.Parameters.AddWithValue("@AddressID", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        address = new Address
                        {
                            AddressID = reader.GetInt32(0),
                            AccountID = reader.GetInt32(1),
                            AddressTypeID = reader.GetInt32(2),
                            //Address = reader.GetString(3),
                            CreatedDate = reader.GetDateTime(4),
                            CreatedBy = reader.GetString(5),
                            UpdatedDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            UpdatedBy = reader.IsDBNull(7) ? null : reader.GetString(7)
                        };
                    }
                }
            }
        }

        return address;
    }

    public async Task AddAddressAsync(Address address)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("INSERT INTO Address_table (AddressID, AccountID, AddressTypeID, Address, CreatedDate, CreatedBy) VALUES (@AddressID, @AccountID, @AddressTypeID, @Address, @CreatedDate, @CreatedBy)", connection))
            {
                command.Parameters.AddWithValue("@AddressID", address.AddressID);
                command.Parameters.AddWithValue("@AccountID", address.AccountID);
                command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
               // command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@CreatedDate", address.CreatedDate);
                command.Parameters.AddWithValue("@CreatedBy", address.CreatedBy);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAddressAsync(Address address)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("UPDATE Address_table SET AccountID = @AccountID, AddressTypeID = @AddressTypeID, Address = @Address, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AddressID = @AddressID", connection))
            {
                command.Parameters.AddWithValue("@AddressID", address.AddressID);
                command.Parameters.AddWithValue("@AccountID", address.AccountID);
                command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
              //  command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@UpdatedDate", address.UpdatedDate.HasValue ? (object)address.UpdatedDate.Value : CoreWCF.DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedBy", address.UpdatedBy ?? (object)CoreWCF.DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAddressAsync(int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DELETE FROM Address_table WHERE AddressID = @AddressID", connection))
            {
                command.Parameters.AddWithValue("@AddressID", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
}