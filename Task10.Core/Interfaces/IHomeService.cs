using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IHomeService
    {
        public Task<HomeDto> GetHomeDtoAsync(CancellationToken cancellationToken);
    }
}
