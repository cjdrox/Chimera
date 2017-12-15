
/* Chimera.Entities.Region.cs*/ 
using System;
using Chimera.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Chimera.DataAcess
{
	public class RegionAdapter
	{
		private readonly string _connectionString;

	    public RegionAdapter(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

		public Region Read(long id)
		{
			const string sql = "SELECT Id, Name, Territory, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt FROM Region WHERE Id=@Id";

			Region entity = null;

			using(var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					var reader = command.ExecuteReader(CommandBehavior.SingleResult);

					while (reader.Read())
					{
						entity = new Region()
						{
							Id = (System.Int32) reader["Id"],
							Name = (System.String) reader["Name"],
							Territory = (Chimera.Entities.CountryTerritory) reader["Territory"],
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

		public bool Insert(Region entity)
		{
			const string sql = "INSERT INTO Region ( Id, Name, Territory, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt ) VALUES ( @Id, @Name, @Territory, @Id, @ObjectId, @CreatedAt, @ModifiedAt, @DeletedAt )";

			long rows;

			using(var connection = new SqlConnection(_connectionString))
			{
				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", entity.Id);
					command.Parameters.AddWithValue("@Name", entity.Name);
					command.Parameters.AddWithValue("@Territory", entity.Territory);
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

		public bool Update(Region entity)
		{
			return false;
		}

		public Region Delete(Guid id)
		{
			return null;
		}
	}
}

	