using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface ITransactionService
{
    // Listeyi async getirir
    Task<IEnumerable<TransactionDto>> GetAllAsync(int userId);
    Task<TransactionDto> CreateAsync(CreateTransactionDto dto, int userId);
    Task<TransactionDto?> UpdateAsync(int id, CreateTransactionDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);

}

