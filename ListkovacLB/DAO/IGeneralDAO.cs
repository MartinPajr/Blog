﻿using ListkovacDTO;

namespace ListkovacBL.DAO
{
    public interface IGeneralDAO
    {
        Task<ClanekDTO> GetClanekById(int clanekId);
        Task<List<KomentarDTO>> GetKomentareKClanku(int id);
        Task<BlogUserDTO> GetUser(int userId);
        Task<BlogUserDTO> GetByNameUser(string name);
        Task<KomentarDTO> CreateNewComentAsync(KomentarDTO koment);
        Task<BlogUserDTO> GetUserNameAsync(string username);
        Task<BlogUserDTO> CreateBlogUserAsync(string name, string pass, string email);
    }
}