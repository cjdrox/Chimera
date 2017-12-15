
/* Chimera.Entities.BirthPlace.cs*/ 
using System;
using Chimera.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Chimera.DataAcess
{
	public class BirthPlaceAdapter
	{
		private readonly string _connectionString;

	    public BirthPlaceAdapter(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

		public BirthPlace Read(long id)
		{
			const string sql = "SELECT Id, Name, Region, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt FROM BirthPlace WHERE Id=@Id";

			BirthPlace entity = null;

			using(var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					var reader = command.ExecuteReader(CommandBehavior.SingleResult);

					while (reader.Read())
					{
						entity = new BirthPlace()
						{
							Id = (System.Int32) reader["Id"],
							Name = (System.String) reader["Name"],
							Region = (Chimera.Entities.Region) reader["Region"],
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

		public bool Insert(BirthPlace entity)
		{
			const string sql = "INSERT INTO BirthPlace ( Id, Name, Region, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt ) VALUES ( @Id, @Name, @Region, @Id, @ObjectId, @CreatedAt, @ModifiedAt, @DeletedAt )";

			long rows;

			using(var connection = new SqlConnection(_connectionString))
			{
				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", entity.Id);
					command.Parameters.AddWithValue("@Name", entity.Name);
					command.Parameters.AddWithValue("@Region", entity.Region);
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

		public bool Update(BirthPlace entity)
		{
			return false;
		}

		public BirthPlace Delete(Guid id)
		{
			return null;
		}
	}
}

	