using ListkovacDTO;

namespace Listkovac2Auth.Authentication
{
    public interface IJwtProvider
    {
        string Generate(BlogUserDTO user);
    }
}