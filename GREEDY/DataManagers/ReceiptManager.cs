using System;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;

namespace GREEDY.DataManagers
{
    public class ReceiptManager : IReceiptManager, IDisposable
    {
        private readonly DbContext _context;

        public ReceiptManager(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ReceiptDataModel GetReceipt(int receiptId)
        {
            var receipt = _context.Set<ReceiptDataModel>()
                .Include(x => x.Shop)
                .First(x => x.ReceiptId == receiptId);
            return receipt;
        }
    }
}