
/* Chimera.Entities.Subject.cs*/ 
using System;
using Chimera.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Chimera.DataAcess
{
	public class SubjectAdapter
	{
		private readonly string _connectionString;

	    public BioProfileAdapter(string connectionString)
	    {
	        _connectionString = connectionString;
	    }

		public Subject Read(long id)
		{
			const string sql = "SELECT Id, CodeName, BirthPlace, Nationality, BioProfile, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt FROM Subject WHERE Id=@Id";

			Subject entity = null;

			using(var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					var reader = command.ExecuteReader(CommandBehavior.SingleResult);

					while (reader.Read())
					{
						entity = new Subject()
						{
							Id = (System.Int32) reader["Id"],
							CodeName = (System.String) reader["CodeName"],
							BirthPlace = (Chimera.Entities.BirthPlace) reader["BirthPlace"],
							Nationality = (Chimera.Entities.Nationality) reader["Nationality"],
							BioProfile = (Chimera.Entities.BioProfile) reader["BioProfile"],
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

		public bool Insert(Subject entity)
		{
			const string sql = "INSERT INTO Subject ( Id, CodeName, BirthPlace, Nationality, BioProfile, Id, ObjectId, CreatedAt, ModifiedAt, DeletedAt ) VALUES ( @Id, @CodeName, @BirthPlace, @Nationality, @BioProfile, @Id, @ObjectId, @CreatedAt, @ModifiedAt, @DeletedAt )";

			long rows;

			using(var connection = new SqlConnection(_connectionString))
			{
				using(var command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Id", entity.Id);
					command.Parameters.AddWithValue("@CodeName", entity.CodeName);
					command.Parameters.AddWithValue("@BirthPlace", entity.BirthPlace);
					command.Parameters.AddWithValue("@Nationality", entity.Nationality);
					command.Parameters.AddWithValue("@BioProfile", entity.BioProfile);
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

		public bool Update(Subject entity)
		{
			return false;
		}

		public Subject Delete(Guid id)
		{
			return null;
		}
	}
}

	
