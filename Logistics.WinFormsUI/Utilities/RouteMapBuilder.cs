using System;
using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.WinFormsUI.UserControls;

namespace Logistics.WinFormsUI.Utilities
{
    public static class RouteMapBuilder
    {
        public static List<RouteMapPoint> Build(Order order)
        {
            List<RouteMapPoint> points = new List<RouteMapPoint>();
            if (order == null)
            {
                return points;
            }

            string pickup = order.Route != null ? order.Route.PickupAddress : "Diem lay hang";
            string delivery = order.Route != null ? order.Route.DeliveryAddress : "Diem giao hang";

            points.Add(new RouteMapPoint
            {
                Title = "Lay hang",
                Subtitle = pickup,
                State = GetPickupState(order.CurrentStatus)
            });

            List<string> warehouseIds = CollectWarehouseIds(order);
            if (warehouseIds.Count == 0 && IsWarehouseStatus(order.CurrentStatus))
            {
                points.Add(new RouteMapPoint
                {
                    Title = "Kho trung chuyen",
                    Subtitle = "Dang xu ly tai kho",
                    State = RouteMapPointState.Current
                });
            }
            else
            {
                for (int i = 0; i < warehouseIds.Count; i++)
                {
                    string warehouseId = warehouseIds[i];
                    bool isCurrent = IsCurrentWarehouse(order, warehouseId);
                    points.Add(new RouteMapPoint
                    {
                        Title = "Kho " + warehouseId,
                        Subtitle = ResolveWarehouseLabel(warehouseId),
                        State = isCurrent ? RouteMapPointState.Current : RouteMapPointState.Completed
                    });
                }
            }

            if (order.CurrentStatus == OrderStatus.InTransit || order.CurrentStatus == OrderStatus.OutForDelivery)
            {
                points.Add(new RouteMapPoint
                {
                    Title = order.CurrentStatus == OrderStatus.OutForDelivery ? "Dang giao" : "Dang van chuyen",
                    Subtitle = string.IsNullOrWhiteSpace(order.AssignedVehicleID) ? "Chua co xe gan" : "Xe " + order.AssignedVehicleID,
                    State = RouteMapPointState.Current
                });
            }

            if (IsProblemStatus(order.CurrentStatus))
            {
                points.Add(new RouteMapPoint
                {
                    Title = GetProblemTitle(order.CurrentStatus),
                    Subtitle = "Can xu ly nghiep vu",
                    State = RouteMapPointState.Problem
                });
            }

            points.Add(new RouteMapPoint
            {
                Title = "Giao hang",
                Subtitle = delivery,
                State = GetDeliveryState(order.CurrentStatus)
            });

            return points;
        }

        public static string BuildStatusText(Order order)
        {
            return order == null ? "Chua co du lieu don hang" : "Trang thai: " + order.CurrentStatus;
        }

        public static string BuildProgressText(Order order)
        {
            if (order == null)
            {
                return string.Empty;
            }

            int packageCount = order.Packages != null ? order.Packages.Count : 0;
            string distance = order.Route != null ? order.Route.EstimatedDistanceKm.ToString("F1") + " km" : "--";
            return packageCount + " kien | " + order.TotalWeight.ToString("F1") + " kg | " + distance;
        }

        private static RouteMapPointState GetPickupState(OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.WaitingPickup)
            {
                return RouteMapPointState.Current;
            }

            if (status == OrderStatus.Cancelled)
            {
                return RouteMapPointState.Problem;
            }

            return RouteMapPointState.Completed;
        }

        private static RouteMapPointState GetDeliveryState(OrderStatus status)
        {
            if (status == OrderStatus.Delivered)
            {
                return RouteMapPointState.Completed;
            }

            return RouteMapPointState.Pending;
        }

        private static bool IsWarehouseStatus(OrderStatus status)
        {
            return status == OrderStatus.ArrivedAtWarehouse ||
                   status == OrderStatus.Sorting ||
                   status == OrderStatus.ReadyForDispatch;
        }

        private static bool IsProblemStatus(OrderStatus status)
        {
            return status == OrderStatus.Failed ||
                   status == OrderStatus.Cancelled ||
                   status == OrderStatus.DeliveryAttemptFailed ||
                   status == OrderStatus.Returning ||
                   status == OrderStatus.Returned;
        }

        private static string GetProblemTitle(OrderStatus status)
        {
            if (status == OrderStatus.Cancelled)
            {
                return "Da huy";
            }

            if (status == OrderStatus.DeliveryAttemptFailed)
            {
                return "Giao that bai";
            }

            if (status == OrderStatus.Returning || status == OrderStatus.Returned)
            {
                return "Hoan hang";
            }

            return "Su co";
        }

        private static List<string> CollectWarehouseIds(Order order)
        {
            List<string> ids = new List<string>();
            if (order.Packages != null)
            {
                for (int i = 0; i < order.Packages.Count; i++)
                {
                    Package package = order.Packages[i];
                    if (package != null && !string.IsNullOrWhiteSpace(package.CurrentWarehouseID))
                    {
                        AddUnique(ids, package.CurrentWarehouseID);
                    }
                }
            }

            if (order.StatusHistories != null)
            {
                for (int i = 0; i < order.StatusHistories.Count; i++)
                {
                    string id = ExtractWarehouseId(order.StatusHistories[i].Description);
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        AddUnique(ids, id);
                    }
                }
            }

            return ids;
        }

        private static void AddUnique(List<string> values, string value)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (string.Equals(values[i], value, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            values.Add(value);
        }

        private static string ExtractWarehouseId(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return string.Empty;
            }

            string marker = "warehouse ";
            int index = description.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                return string.Empty;
            }

            int start = index + marker.Length;
            int end = start;
            while (end < description.Length && !char.IsWhiteSpace(description[end]) && description[end] != '.' && description[end] != ',')
            {
                end++;
            }

            return description.Substring(start, end - start).Trim();
        }

        private static bool IsCurrentWarehouse(Order order, string warehouseId)
        {
            if (order.Packages == null)
            {
                return false;
            }

            for (int i = 0; i < order.Packages.Count; i++)
            {
                Package package = order.Packages[i];
                if (package != null && package.IsCurrentlyInWarehouse(warehouseId))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ResolveWarehouseLabel(string warehouseId)
        {
            try
            {
                Warehouse warehouse = DependencyContainer.GetWarehouseService().GetWarehouseById(warehouseId);
                if (warehouse != null && !string.IsNullOrWhiteSpace(warehouse.Name))
                {
                    return warehouse.Name;
                }
            }
            catch
            {
                // Tracking map should keep rendering even if warehouse data is unavailable.
            }

            return "Trung chuyen";
        }
    }
}
