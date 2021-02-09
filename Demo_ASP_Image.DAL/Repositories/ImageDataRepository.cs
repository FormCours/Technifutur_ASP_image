using Demo_ASP_Image.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Database;
using Toolbox.Repository;
using System.Configuration;

namespace Demo_ASP_Image.DAL.Repositories
{
    public class ImageDataRepository : IRepository<Guid, ImageData>
    {
        private ConnectDB DB { get; }

        public ImageDataRepository()
        {
            DB = new ConnectDB(ConfigurationManager.ConnectionStrings["db-source"].ConnectionString);
        }


        protected ImageData ConvertReaderToEntity(SqlDataReader dataReader)
        {
            return new ImageData()
            {
                Id = Guid.Parse(dataReader["Id"].ToString()),
                OriginalName = dataReader["OriginalName"].ToString(),
                ImagePath = dataReader["ImagePath"].ToString(),
                Description = dataReader["Description"] is DBNull ? null : dataReader["Description"].ToString()
            };
        }

        public ImageData Get(Guid id)
        {
            QueryDB query = new QueryDB("SELECT * FROM [ImageData] WHERE [Id] = @id");
            query.AddParametre("id", id);

            return DB.ExecuteReader(query, ConvertReaderToEntity).SingleOrDefault();
        }

        public IEnumerable<ImageData> GetAll()
        {
            QueryDB query = new QueryDB("SELECT * FROM [ImageData]");

            return DB.ExecuteReader(query, ConvertReaderToEntity);
        }

        public Guid Insert(ImageData entity)
        {
            QueryDB query = new QueryDB("INSERT INTO [ImageData] ([OriginalName], [ImagePath], [Description])" +
                                        " OUTPUT Inserted.Id" +
                                        " VALUES (@OriginalName, @ImagePath, @Description)");
            query.AddParametre("OriginalName", entity.OriginalName);
            query.AddParametre("ImagePath", entity.ImagePath);
            query.AddParametre("Description", entity.Description);

            string id = DB.ExecuteScalar(query).ToString();
            return Guid.Parse(id);
        }

        public bool Update(Guid id, ImageData entity)
        {
            QueryDB query = new QueryDB("UPDATE [ImageData]" +
                                        " SET [OriginalName] = @OriginalName" +
                                        "     [ImagePath] = @ImagePath" +
                                        "     [Description] = @Description" +
                                        " WHERE [Id] = @Id");
            query.AddParametre("Id", entity.Id);
            query.AddParametre("OriginalName", entity.OriginalName);
            query.AddParametre("ImagePath", entity.ImagePath);
            query.AddParametre("Description", entity.Description);

            int nbRow = DB.ExecuteNonQuery(query);
            return nbRow == 1;
        }
        
        public bool Delete(Guid id)
        {
            QueryDB query = new QueryDB("DELETE FROM [ImageData] WHERE [Id] = @Id");
            query.AddParametre("Id", id);

            return DB.ExecuteNonQuery(query) == 1;
        }
    }
}
