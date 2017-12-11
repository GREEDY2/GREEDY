using System;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;

namespace GREEDY.DataManagers
{
    public class ReceiptManager : IReceiptManager, IDisposable
    {
        private DbContext context;

        public ReceiptManager(DbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public ReceiptDataModel GetReceipt(int receiptId)
        {
            var receipt = context.Set<ReceiptDataModel>()
                .Include(x => x.Shop)
                .First(x => x.ReceiptId == receiptId);
            return receipt;
        }
    }
}
