
/* Chimera.Entities.Nationality.cs*/ 
using System;
using Chimera.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Chimera.DataAcess
{
	public class NationalityAdapter
	{
		private readonly string _connectionString;

	    public NationalityAdapter(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

		public Nationality Read(long id)
		{
			const string sql = "SELECT Name, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt FROM Nationality WHERE Id=@Id";

			Nationality entity = null;

			using(var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					var reader = command.ExecuteReader(CommandBehavior.SingleResult);

					while (reader.Read())
					{
						entity = new Nationality()
						{
							Name = (System.String) reader["Name"],
							Id = (System.Int64) reader["Id"],
							ObjectId = (System.Guid) reader["ObjectId"],
							CreatedAt = (System.DateTime) reader["CreatedAt"],
							ModifiedAt = (System.DateTime) reader["ModifiedAt"],
							DeletedAt = (System.DateTime?) reader["DeletedAt"],
						};
					}
				}
				
			}

            return entity;
		}

		public bool Insert(Nationality entity)
		{
			const string sql = "INSERT INTO Nationality ( Name, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt ) VALUES ( @Name, @Id, @ObjectId, @CreatedAt, @ModifiedAt, @DeletedAt )";

			long rows;

			using(var connection = new SqlConnection(_connectionString))
			{
				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Name", entity.Name);
					command.Parameters.AddWithValue("@Id", entity.Id);
					command.Parameters.AddWithValue("@ObjectId", entity.ObjectId);
					command.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);
					command.Parameters.AddWithValue("@ModifiedAt", entity.ModifiedAt);
					command.Parameters.AddWithValue("@DeletedAt", entity.DeletedAt);

					rows = command.ExecuteNonQuery();
				}
			}

			return (rows > 0);
		}

		public bool Update(Nationality entity)
		{
			return false;
		}

		public Nationality Delete(Guid id)
		{
			return null;
		}
	}
}

	