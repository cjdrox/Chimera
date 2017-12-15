
/* Chimera.Entities.BioProfile.cs*/ 
using System;
using Chimera.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Chimera.DataAcess
{
	public class BioProfileAdapter
	{
		private readonly string _connectionString;

	    public BioProfileAdapter(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

		public BioProfile Read(long id)
		{
			const string sql = "SELECT Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt FROM BioProfile WHERE Id=@Id";

			BioProfile entity = null;

			using(var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					var reader = command.ExecuteReader(CommandBehavior.SingleResult);

					while (reader.Read())
					{
						entity = new BioProfile()
						{
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

		public bool Insert(BioProfile entity)
		{
			const string sql = "INSERT INTO BioProfile ( Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt ) VALUES ( @Id, @ObjectId, @CreatedAt, @ModifiedAt, @DeletedAt )";

			long rows;

			using(var connection = new SqlConnection(_connectionString))
			{
				using(var command = new SqlCommand(sql, connection))
				{
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

		public bool Update(BioProfile entity)
		{
			return false;
		}

		public BioProfile Delete(Guid id)
		{
			return null;
		}
	}
}

	