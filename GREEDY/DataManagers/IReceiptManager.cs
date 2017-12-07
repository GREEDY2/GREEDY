using GREEDY.Data;

namespace GREEDY.DataManagers
{
    public interface IReceiptManager
    {
        ReceiptDataModel GetReceipt(int receiptId);
    }
}