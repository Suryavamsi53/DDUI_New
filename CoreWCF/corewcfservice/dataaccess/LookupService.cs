using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using corewcfservice.IServices;
using corewcfservice.model;

namespace corewcfservice.dataaccess
{
    public class LookupService : ILookupService
    {
        // private readonly string _connectionString = "Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True";

       private readonly string  _connectionString="Server=SURYA\\SQLEXPRESS;initial catalog=VuejsApp; Database=VuejsApp; integrated security=True; MultipleActiveResultSets=True";

        public List<Lookup> GetLookups()
        {
            var lookups = new List<Lookup>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using var command = new SqlCommand("SELECT * FROM lookup_table", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lookups.Add(new Lookup
                    {
                        Lookup_id = reader.GetInt32(0),
                        Lookup_type = reader.GetString(1),
                        Lookup_desc = reader.GetString(2),
                        Is_active = reader.GetString(3),
                        Createdby = reader.GetString(4),
                        CreatedDATE = reader.GetDateTime(5),
                        Updatedby = reader.IsDBNull(6) ? null : reader.GetString(6),
                        UpdatedDATE = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
                    });
                }
            }
            Console.WriteLine("GetLookups called, returning " + lookups.Count + " items.");
            return lookups;
        }

        public Lookup GetLookup(int id)
        {
            Lookup? lookup = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM lookup_table WHERE lookup_id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lookup = new Lookup
                            {
                                Lookup_id = reader.GetInt32(0),
                                Lookup_type = reader.GetString(1),
                                Lookup_desc = reader.GetString(2),
                                Is_active = reader.GetString(3),
                                Createdby = reader.GetString(4),
                                CreatedDATE = reader.GetDateTime(5),
                                Updatedby = reader.IsDBNull(6) ? null : reader.GetString(6),
                                UpdatedDATE = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            Console.WriteLine("GetLookup called for ID " + id);
            return lookup;
        }

        public void AddLookup(Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO lookup_table (lookup_id, lookup_type, lookup_desc, is_active, createdby, createdDATE, UPDATEDBY, UPDATEDDATE) VALUES (@id, @type, @desc, @active, @createdby, @createddate, @updatedby, @updateddate)", connection))
                {
                    command.Parameters.AddWithValue("@id", lookup.Lookup_id);
                    command.Parameters.AddWithValue("@type", lookup.Lookup_type);
                    command.Parameters.AddWithValue("@desc", lookup.Lookup_desc);
                    command.Parameters.AddWithValue("@active", lookup.Is_active);
                    command.Parameters.AddWithValue("@createdby", lookup.Createdby);
                    command.Parameters.AddWithValue("@createddate", lookup.CreatedDATE);
                    command.Parameters.AddWithValue("@updatedby", (object)lookup.Updatedby ?? System.DBNull.Value);
                    command.Parameters.AddWithValue("@updateddate", (object)lookup.UpdatedDATE ?? System.DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("AddLookup called for ID " + lookup.Lookup_id);
        }

        public void UpdateLookup(Lookup lookup)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE lookup_table SET lookup_type = @type, lookup_desc = @desc, is_active = @active, UPDATEDBY = @updatedby, UPDATEDDATE = @updateddate WHERE lookup_id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", lookup.Lookup_id);
                    command.Parameters.AddWithValue("@type", lookup.Lookup_type);
                    command.Parameters.AddWithValue("@desc", lookup.Lookup_desc);
                    command.Parameters.AddWithValue("@active", lookup.Is_active);
                    command.Parameters.AddWithValue("@updatedby", (object)lookup.Updatedby ?? System.DBNull.Value);
                    command.Parameters.AddWithValue("@updateddate", (object)lookup.UpdatedDATE ?? System.DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("UpdateLookup called for ID " + lookup.Lookup_id);
        }

        public void DeleteLookup(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM lookup_table WHERE lookup_id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("DeleteLookup called for ID " + id);
        }
    }
}
