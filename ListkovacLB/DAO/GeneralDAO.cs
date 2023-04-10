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
            string sql = "SELECT Id, Name, Text,Date, AutorId, Upvotes Date FROM Article WHERE Id = @ClanekID";

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
            string sql = "SELECT Id, ClanekId, Text, UserId, Time FROM Coments";
            using (var connection = new SqlConnection(ConnString))
            {
                await connection.OpenAsync();
                var komenty = await connection.QueryAsync<KomentarDTO>(sql);

                foreach (var komentar in komenty)
                {
                    if(komentar.ClanekId == id)
                    {
                        komentarDTOs.Add(new KomentarDTO()
                        {
                            Id = komentar.Id,
                            ClanekId = komentar.ClanekId,
                            Text = komentar.Text,
                            Time = komentar.Time,
                            UserId = komentar.UserId,
                        }); 
                    }
                }
                return komentarDTOs;
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
        public async Task<KomentarDTO> CreateNewComentAsync(KomentarDTO koment)
        {
            var parameters = new { Koment = koment.Text };
            string sql = "INSERT INTO Coments VALUES ";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<KomentarDTO>(sql, parameters);

            return result;
        }
        public async Task<BlogUserDTO> GetUserNameAsync(string username)
        {
            var parameters = new { Username = username };
            string sql = "Select ID, Name, Pass, Email from Users where Name = @Username";

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





        public async Task<AkceFullDTO> CreateNewAkceAsync(AkceFullDTO fullAkce)
        {
            var parameters = new { Datum = fullAkce.Datum, Nazevakce = fullAkce.Nazevakce, PoradateleID = fullAkce.PoradateleID, Prostory_konaniID = fullAkce.Prostory_konaniID, Popisakce = fullAkce.Popisakce };
            string sql = "INSERT INTO Akce(Datum,Nazevakce,PoradateleID,Prostory_konaniID,Popisakce) Values (@Datum,@Nazevakce,@PoradateleID,@Prostory_konaniID,@Popisakce) ";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<AkceFullDTO>(sql, parameters);

            return result;
        }
        public async Task<AkceDTO> GetAkceByIdAsync(int akceId)
        {
            var parameters = new { AkceId = akceId };
            string sql = "SELECT Datum,Nazevakce,Mesto,Popisakce,PoradateleID,AkceID,Akce.Prostory_konaniID,Cas FROM Akce,Prostory_konani,Adresy WHERE Akce.Prostory_konaniID = Prostory_konani.Prostory_konaniID AND Prostory_konani.AdresyID = Adresy.AdresyID AND Akce.AkceID = @AkceId";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleAsync<AkceDTO>(sql, parameters);

            return result;

            //SELECT Datum,Nazevakce,Mesto,Popisakce FROM Akce,Prostory_konani,Adresy WHERE Akce.Prostory_konaniID = Prostory_konani.Prostory_konaniID AND Prostory_konani.AdresyID = Adresy.AdresyID AND Akce.AkceID =".$i ;
        }
        public async Task<List<AkceFullDTO>> GetAkceByTimeAsync()
        {
            // var parameters = new { AkceId = akceId };
            string sql = "SELECT AkceID,Prostory_konani.Prostory_konaniID,Datum,Nazevakce,PoradateleID,Popisakce,Nazevprostor,Kapacita,Mesto,Cislopopisne,PSC,Ulice,Stat,Cas FROM Akce,Prostory_konani,Adresy WHERE Akce.Prostory_konaniID = Prostory_konani.Prostory_konaniID AND Prostory_konani.AdresyID = Adresy.AdresyID ORDER BY Datum";
            List<AkceFullDTO> akceFullDTOs = new List<AkceFullDTO>();

            using (var connection = new SqlConnection(ConnString))
            {

                await connection.OpenAsync();
                var nazvyakci = await connection.QueryAsync<AkceFullDTO>(sql);
                foreach (var nazevakce in nazvyakci)
                {
                    akceFullDTOs.Add(new AkceFullDTO()
                    {
                        AkceID = nazevakce.AkceID,
                        Prostory_konaniID = nazevakce.Prostory_konaniID,
                        Datum = nazevakce.Datum,
                        Nazevakce = nazevakce.Nazevakce,
                        PoradateleID = nazevakce.PoradateleID,
                        Popisakce = nazevakce.Popisakce,
                        Nazevprostor = nazevakce.Nazevprostor,
                        Kapacita = nazevakce.Kapacita,
                        Mesto = nazevakce.Mesto,
                        Cislopopisne = nazevakce.Cislopopisne,
                        PSC = nazevakce.PSC,
                        Ulice = nazevakce.Ulice,
                        Stat = nazevakce.Stat,
                        Cas = nazevakce.Cas
                    });
                }
            }

            return akceFullDTOs;

        }
        public async Task<UserDTO> GetUserbyNameAsync(string username)
        {
            var parameters = new { Username = username };
            string sql = "Select Username, Pass, Role, IdPoradatele, IdZakaznika from Users where Username = @Username";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<UserDTO>(sql, parameters);

            return result;
        }
        public async Task<UserDTO> UpdatePasswordNameAsync(string username, string password)
        {
            var parameters = new { Username = username, Password = password };
            string sql = "UPDATE Users SET Pass = @Password Where Username = @Username";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<UserDTO>(sql, parameters);

            return result;
        }
        public async Task<UserDTO> CreateUserAsync(string username, string password, string jmeno, string prijmeni, string telefon, string cpp, string mesto, string psc, string stat, string ulice)
        {
            var parameters = new { Email = username, Password = password, Jmeno = jmeno, Prijmeni = prijmeni, Telefon = telefon, Cpp = cpp, Mesto = mesto, Psc = psc, Stat = stat, Ulice = ulice };
            string sql = "EXEC [RegistrujZakaznika]@Email,@Jmeno,@Prijmeni,@Telefon,@Cpp,@Mesto,@Psc,@Stat,@Ulice,@Password";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<UserDTO>(sql, parameters);

            return result;
        }

        public async Task<List<VenueDTO>> GetAllVenuesAsync()
        {
            // var parameters = new { AkceId = akceId };
            string sql = "SELECT * FROM Prostory_konani";
            List<VenueDTO> VenueDTOs = new List<VenueDTO>();

            using (var connection = new SqlConnection(ConnString))
            {

                await connection.OpenAsync();
                var venues = await connection.QueryAsync<VenueDTO>(sql);

                foreach (var venue in venues)
                {
                    VenueDTOs.Add(new VenueDTO()
                    {
                        AdresyID = venue.AdresyID,
                        Prostory_konaniID = venue.Prostory_konaniID,
                        Kapacita = venue.Kapacita,
                        Nazevprostor = venue.Nazevprostor
                    });

                }
                return VenueDTOs;
            }

        }
        
        public async Task<List<VenueDTO>> GetMyEvents()
        {
            // var parameters = new { AkceId = akceId };
            string sql = "SELECT * FROM Akce ";
            List<VenueDTO> VenueDTOs = new List<VenueDTO>();

            using (var connection = new SqlConnection(ConnString))
            {

                await connection.OpenAsync();
                var venues = await connection.QueryAsync<VenueDTO>(sql);

                foreach (var venue in venues)
                {
                    VenueDTOs.Add(new VenueDTO()
                    {
                        AdresyID = venue.AdresyID,
                        Prostory_konaniID = venue.Prostory_konaniID,
                        Kapacita = venue.Kapacita,
                        Nazevprostor = venue.Nazevprostor
                    });

                }
                return VenueDTOs;
            }

        }
        public async Task<AddressDTO> GetAddressById(int id)
        {
            var parameters = new { Id = id };
            string sql = "Select * from Adresy where AdresyID = @Id";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<AddressDTO>(sql, parameters);

            return result;
        }
        public async Task<VenueDTO> GetVenueById(int id)
        {
            var parameters = new { Id = id };
            string sql = "Select * from Prostory_konani where Prostory_konaniID = @Id";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<VenueDTO>(sql, parameters);
            return result;
        }

        public async Task<CreateTicketsDTO> GenerujVstupenkyPROCEDURE(CreateTicketsDTO tickets)
        {
            var parameters = new { cena = tickets.Cena, cenaVIP = tickets.CenaVIP, akceID = tickets.AkceID };
            string sql = "EXEC [GenerujVstupenky]@cena,@cenaVIP,@akceID";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync(sql, parameters);
            return result;
        }

        public async Task<BuyTicketsDTO> ObjednaniVstupenekPROCEDURE(BuyTicketsDTO order, string username, string heslo)
        {
            if (order.Verze == "normal")
            {
                order.PocetVIP = 0;
            }
            else
            {
                order.PocetVIP = order.Pocet;
                order.Pocet = 0;
            }

            var parameters = new { PocetNormal = order.Pocet, PocetVip = order.PocetVIP, AkceID = order.AkceID, Email = username, Heslo = heslo };

            string sql = "EXEC [ObjednaniVstupenek]@PocetNormal,@PocetVip ,@AkceID,@Email,@Heslo";
            using var connection = new SqlConnection(ConnString);
            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<BuyTicketsDTO>(sql, parameters);


            return result;
        }
        public async Task<TicketDTO> QrHandler(TicketDTO vstupT)
        {
            TicketDTO ticket = new TicketDTO();
            ticket.VstupenkyID = vstupT.VstupenkyID;
            var parameters = new { VstupenkaID = ticket.VstupenkyID };
            string sql = "Select Qr From Qr_kody Where @VstupenkaID = VstupenkyID";
            using var connection = new SqlConnection(ConnString);
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<TicketDTO>(sql, parameters);
            await connection.CloseAsync();

            if (result != null)
            {
                ticket.QrCode = "Id Listku:" + vstupT.VstupenkyID + "Id Akce:" + vstupT.AkceID;
                /*
                var parameters2 = new { Qr = ticket.QrCode, VstupenkaID = ticket.TicketID };
                string sql2 = "Insert Into Qr_kody(Qr) VALUES @Qr Where @VstupenkaID = VstupenkyID";
                using var connection2 = new SqlConnection(ConnString);
                await connection2.OpenAsync();
                var result2 = await connection.QuerySingleOrDefaultAsync<BuyTicketsDTO>(sql2, parameters2);
                */
            }
            else
            {
                ticket.QrCode = Convert.ToString(result);
            }

            return ticket;
        }


        public async Task<CreateVenueDTO> VytvorProstoryPROCEDURE(CreateVenueDTO prostory)
        {
            var parameters = new { nazevProstoru = prostory.nazevProstoru, kapacita = prostory.kapacita, pocetsekci = prostory.pocetsekci, kSezeniNormal = prostory.kSezeniNormal, pocetVipsekci = prostory.pocetVipsekci, kSezeniVip = prostory.kSezeniVIP, cpp = prostory.cpp, mesto = prostory.mesto, psc = prostory.psc, stat = prostory.stat, ulice = prostory.ulice };
            string sql = "EXEC [VytvorProstory]@nazevProstoru, @kapacita, @pocetsekci, @kSezeniNormal, @pocetVipsekci, @kSezeniVip, @cpp, @mesto, @psc, @stat, @ulice";

            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync(sql, parameters);
            return result;
        }
        public async Task<List<TicketsAkceDTO>> GetVstupenkyByAkceID(int AkceId)
        {

            List<TicketsAkceDTO> akceDTOs = new List<TicketsAkceDTO>();
            var parameters = new { AkceID = AkceId };
            string sql = "SELECT  COUNT(AkceID) AS 'PocetDostupnychTicketu', Verze From Vstupenky Where AkceID = @AkceID AND ISNULL(ObjednavkyID, '') = '' GROUP BY Verze";

            using (var connection = new SqlConnection(ConnString))
            {

                await connection.OpenAsync();
                var series = await connection.QueryAsync<TicketsAkceDTO>(sql, parameters);

                foreach (var serie in series)
                {
                    akceDTOs.Add(new TicketsAkceDTO()
                    {
                        PocetDostupnychTicketu = serie.PocetDostupnychTicketu,
                        Verze = serie.Verze
                    });

                }
                return akceDTOs;
            }
        }
        public async Task<List<TicketDTO>> GetVstupenkyByUser(int ZakaznikId)
        {

            List<TicketDTO> akceDTOs = new List<TicketDTO>();
            var parameters = new { ZakaznikId = ZakaznikId };
            string sql = "SELECT AkceID, VstupenkyID, Cena, Verze FROM Users, Objednavky, Vstupenky WHERE Objednavky.ZakazniciID = @ZakaznikId AND Vstupenky.ObjednavkyID = Objednavky.ObjednavkyID AND IdZakaznika = @ZakaznikId";


            using (var connection = new SqlConnection(ConnString))
            {

                await connection.OpenAsync();
                var vstupenky = await connection.QueryAsync<TicketDTO>(sql, parameters);

                foreach (var vstupenka in vstupenky)
                {
                    akceDTOs.Add(new TicketDTO()
                    {
                        AkceID = vstupenka.AkceID,
                        VstupenkyID = vstupenka.VstupenkyID,
                        Cena = vstupenka.Cena,
                        Verze = vstupenka.Verze,
                    });

                }
                return akceDTOs;
            }
        }
        public async Task<ZakazniciDTO> GetZakaznikInfo(int ZakaznikId)
        {
            var parameters = new { ZakaznikId = ZakaznikId };
            string sql = "SELECT Email, Jmeno, Prijmeni, Telefon FROM Zakaznici WHERE ZakazniciID = @ZakaznikId";
            using var connection = new SqlConnection(ConnString);

            await connection.OpenAsync();

            var result = await connection.QuerySingleOrDefaultAsync<ZakazniciDTO>(sql, parameters);
            return result;
        }
    }
}
