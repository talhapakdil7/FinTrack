using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

// Interface = ne yapılacağını tanımlar
// Nasıl yapılacağı burada yazılmaz
public interface ITransactionService
{
    // Tüm transactionları getir
    IEnumerable<TransactionDto> GetAll();

    // Yeni transaction oluştur
    TransactionDto Create(CreateTransactionDto dto);

    // Transaction sil
    bool Delete(int id);

    // Transaction güncelle
    // ? = null dönebilir (bulamazsa)
    TransactionDto? Update(int id, CreateTransactionDto dto);
}