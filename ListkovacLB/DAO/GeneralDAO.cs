using System.Data.SqlClient;
using Dapper;
using ListkovacDTO;

namespace ListkovacBL.DAO
{
    public class GeneralDAO : IGeneralDAO
    {
        private const string ConnString = @"Data Source=localhost\MSSQLSERVER01;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;Initial Catalog=blog";

        public GeneralDAO()
        {
        }
        public async Task<ClanekDTO> GetClanekById(int clanekId)
        {
            var parameters = new { ClanekID = clanekId };
            string sql = "SELECT Id, Name, Text,Date, AutorId, Upvotes FROM Article WHERE Id = @ClanekID";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleAsync<ClanekDTO>(sql, parameters);

            return result;

            //SELECT Datum,Nazevakce,Mesto,Popisakce FROM Akce,Prostory_konani,Adresy WHERE Akce.Prostory_konaniID = Prostory_konani.Prostory_konaniID AND Prostory_konani.AdresyID = Adresy.AdresyID AND Akce.AkceID =".$i ;
        }
        
        public async Task<List<KomentarDTO>> GetKomentareKClanku(int id)
        {
            List<KomentarDTO> komentarDTOs = new List<KomentarDTO>();
            var parameters = new { Hledana = id };
            string sql = "SELECT Coments.Id, Coments.ClanekId, Coments.Text, Coments.UserId, Coments.Time, Users.Name AS 'Username' FROM Coments INNER JOIN Users ON Coments.UserId = Users.ID WHERE ClanekId = @Hledana";
            using (var connection = new SqlConnection(ConnString))
            {
                await connection.OpenAsync();
                var komenty = await connection.QueryAsync<KomentarDTO>(sql, parameters);

                return komenty.ToList();
            }
        }
        public async Task<BlogUserDTO> GetUser(int userId)
        {
            var parameters = new { UserId = userId };
            string sql = "SELECT * FROM Users WHERE ID = @UserId";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleAsync<BlogUserDTO>(sql, parameters);

            return result;

        }
        public async Task<BlogUserDTO> GetByNameUser(string name)
        {
            var parameters = new { Name = name };
            string sql = "SELECT * FROM Users WHERE Name = @Name";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleAsync<BlogUserDTO>(sql, parameters);

            return result;

        }
        public async Task CreateNewComentAsync(KomentarDTO koment)
        {
            var parameters = new { Text = koment.Text, ClanekId = koment.ClanekId, UserId = koment.UserId, Cas = koment.Time };
            string sql = "INSERT INTO Coments(ClanekId, Text, UserId, Time) VALUES (@ClanekId, @Text, @UserId, @Cas)";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(sql, parameters);

        }
        public async Task<BlogUserDTO> GetUserNameAsync(string username)
        {
            var parameters = new { Username = username };
            string sql = "Select ID, Name, Pass, Email, Token from Users where Name = @Username";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<BlogUserDTO>(sql, parameters);

            return result;
        }
        public async Task<BlogUserDTO> CreateBlogUserAsync(string name, string pass, string email)
        {
            var parameters = new { Name = name, Pass = pass, Email = email};
            string sql = "INSERT INTO Users(Name,Pass,Email) VALUES (@Name,@Pass,@Email);";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<BlogUserDTO>(sql, parameters);

            return result;
        }
        public async Task CreateNewClanekAsync(ClanekDTO clanek)
        {
            var parameters = new { Name = clanek.Name, Text = clanek.Text, Date = clanek.Date, AutorId = clanek.AutorId };
            string sql = "INSERT INTO Article(Name, Text, Date, AutorId) VALUES (@Name, @Text, @Date, @AutorId)";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(sql, parameters);

        }
        public async Task<List<ClanekDTO>> GetClankyByUserId(int userId)
        {
            var parameters = new { UserId = userId };
            string sql = "SELECT * FROM Article WHERE AutorId = @UserId";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QueryAsync<ClanekDTO>(sql, parameters);

            return result.ToList();

       }
        public async Task EditClanekById(ClanekDTO clanek)
        {
            var parameters = new {Name = clanek.Name, Text = clanek.Text, Id = clanek.Id };
            string sql = "UPDATE Article SET Name = @Name, Text = @Text WHERE Id = @Id";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(sql, parameters);

        }

    }
}
