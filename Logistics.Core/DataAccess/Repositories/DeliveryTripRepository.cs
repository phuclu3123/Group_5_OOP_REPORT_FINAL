using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.DataAccess.Repositories
{
    public class DeliveryTripRepository : JsonRepository<DeliveryTrip>
    {
        public DeliveryTripRepository(string filePath) : base(filePath) { }

        public override DeliveryTrip GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].TripID == id)
                {
                    return _items[i];
                }
            }

            return null!;
        }

        public override void Update(DeliveryTrip entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].TripID == entity.TripID)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }

        public List<DeliveryTrip> FindByStatus(DeliveryTripStatus status)
        {
            List<DeliveryTrip> result = new List<DeliveryTrip>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Status == status)
                {
                    result.Add(_items[i]);
                }
            }

            return result;
        }
    }
}
