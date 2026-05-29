using System.Collections.Generic;
using Logistics.Core.Models.Business;

namespace Logistics.Core.DataAccess.Repositories
{
    public class TransactionRepository : JsonRepository<Transaction>
    {
        public TransactionRepository(string filePath) : base(filePath)
        {
        }

        public override Transaction GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].TransactionID == id)
                {
                    return _items[i];
                }
            }

            return null!;
        }

        public List<Transaction> FindByOrder(string orderId)
        {
            List<Transaction> result = new List<Transaction>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].OrderID == orderId)
                {
                    result.Add(_items[i]);
                }
            }

            return result;
        }

        public override void Update(Transaction entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].TransactionID == entity.TransactionID)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }
    }
}
