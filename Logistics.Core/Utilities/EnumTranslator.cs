using Logistics.Core.Models.Account;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Utilities
{
    public static class EnumTranslator
    {
        public static string TranslateUserRole(UserRole role)
        {
            switch (role)
            {
                case UserRole.Admin:
                    return "Quản trị viên";
                case UserRole.Driver:
                    return "Tài xế";
                case UserRole.Dispatcher:
                    return "Điều phối viên";
                case UserRole.WarehouseStaff:
                    return "Nhân viên kho";
                default:
                    return role.ToString();
            }
        }

        public static string TranslateWorkStatus(WorkStatus status)
        {
            switch (status)
            {
                case WorkStatus.Active:
                    return "Đang hoạt động";
                case WorkStatus.OnLeave:
                    return "Nghỉ phép";
                case WorkStatus.Resigned:
                    return "Đã thôi việc";
                default:
                    return status.ToString();
            }
        }

        public static WorkStatus ParseWorkStatus(string translated)
        {
            switch (translated)
            {
                case "Đang hoạt động":
                    return WorkStatus.Active;
                case "Nghỉ phép":
                    return WorkStatus.OnLeave;
                case "Đã thôi việc":
                    return WorkStatus.Resigned;
                default:
                    return WorkStatus.Active;
            }
        }

        public static string TranslateRole(Role role)
        {
            switch (role)
            {
                case Role.Admin:
                    return "Quản trị viên";
                case Role.Driver:
                    return "Tài xế";
                case Role.WarehouseManager:
                    return "Nhân viên kho";
                case Role.Dispatcher:
                    return "Điều phối viên";
                default:
                    return role.ToString();
            }
        }

        public static string TranslateRole(string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return "Quản trị viên";
                case "Driver":
                    return "Tài xế";
                case "WarehouseStaff":
                case "WarehouseManager":
                    return "Nhân viên kho";
                case "Dispatcher":
                    return "Điều phối viên";
                default:
                    return roleName;
            }
        }

        public static string TranslateVehicleType(VehicleType type)
        {
            switch (type)
            {
                case VehicleType.Motorbike:
                    return "Xe máy";
                case VehicleType.Van:
                    return "Xe tải nhỏ";
                case VehicleType.Truck_1Ton:
                    return "Xe tải 1 tấn";
                case VehicleType.Container_40ft:
                    return "Container 40ft";
                case VehicleType.ColdStorageTruck:
                    return "Xe tải đông lạnh";
                default:
                    return type.ToString();
            }
        }

        public static VehicleType ParseVehicleType(string translated)
        {
            if (translated.Contains("Xe máy")) return VehicleType.Motorbike;
            if (translated.Contains("nhỏ") || translated.Contains("Van")) return VehicleType.Van;
            if (translated.Contains("1 tấn")) return VehicleType.Truck_1Ton;
            if (translated.Contains("Container")) return VehicleType.Container_40ft;
            if (translated.Contains("đông lạnh")) return VehicleType.ColdStorageTruck;
            return VehicleType.Motorbike;
        }

        public static string TranslateVehicleStatus(VehicleStatus status)
        {
            switch (status)
            {
                case VehicleStatus.Ready:
                    return "Sẵn sàng";
                case VehicleStatus.Maintenance:
                    return "Bảo trì";
                case VehicleStatus.InTransit:
                    return "Đang vận chuyển";
                case VehicleStatus.Broken:
                    return "Đang hỏng";
                default:
                    return status.ToString();
            }
        }

        public static VehicleStatus ParseVehicleStatus(string translated)
        {
            switch (translated)
            {
                case "Sẵn sàng":
                    return VehicleStatus.Ready;
                case "Bảo trì":
                    return VehicleStatus.Maintenance;
                case "Đang vận chuyển":
                    return VehicleStatus.InTransit;
                case "Đang hỏng":
                    return VehicleStatus.Broken;
                default:
                    return VehicleStatus.Ready;
            }
        }

        public static string TranslateDriverStatus(DriverStatus status)
        {
            switch (status)
            {
                case DriverStatus.Available:
                    return "Sẵn sàng";
                case DriverStatus.Busy:
                    return "Đang giao hàng";
                case DriverStatus.OffDuty:
                    return "Ngoài ca";
                default:
                    return status.ToString();
            }
        }

        public static string TranslateOrderStatus(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Pending:
                    return "Chờ xử lý";
                case OrderStatus.InTransit:
                    return "Đang vận chuyển";
                case OrderStatus.Delivered:
                    return "Đã giao";
                case OrderStatus.Cancelled:
                    return "Đã hủy";
                case OrderStatus.Returned:
                    return "Hoàn trả";
                case OrderStatus.Failed:
                    return "Giao thất bại";
                case OrderStatus.WaitingPickup:
                    return "Chờ lấy hàng";
                case OrderStatus.PickedUp:
                    return "Đã lấy hàng";
                case OrderStatus.ArrivedAtWarehouse:
                    return "Đã nhập kho";
                case OrderStatus.Sorting:
                    return "Đang phân loại";
                case OrderStatus.ReadyForDispatch:
                    return "Sẵn sàng điều phối";
                case OrderStatus.OutForDelivery:
                    return "Đang giao hàng";
                case OrderStatus.DeliveryAttemptFailed:
                    return "Giao không thành công";
                case OrderStatus.Returning:
                    return "Đang hoàn hàng";
                default:
                    return status.ToString();
            }
        }

        public static string TranslateServiceType(ServiceType type)
        {
            switch (type)
            {
                case ServiceType.Standard:
                    return "Tiêu chuẩn";
                case ServiceType.Express:
                    return "Nhanh";
                case ServiceType.Instant:
                    return "Hỏa tốc";
                default:
                    return type.ToString();
            }
        }

        public static string TranslatePackageStatus(PackageStatus status)
        {
            switch (status)
            {
                case PackageStatus.Created:
                    return "Mới tạo";
                case PackageStatus.WaitingPickup:
                    return "Chờ lấy hàng";
                case PackageStatus.PickedUp:
                    return "Đã lấy hàng";
                case PackageStatus.InWarehouse:
                    return "Đang ở kho";
                case PackageStatus.Sorting:
                    return "Đang phân loại";
                case PackageStatus.LoadedForDelivery:
                    return "Đã xuất kho";
                case PackageStatus.InTransit:
                    return "Đang vận chuyển";
                case PackageStatus.Delivered:
                    return "Đã giao";
                case PackageStatus.FailedDelivery:
                    return "Giao thất bại";
                case PackageStatus.Returning:
                    return "Đang hoàn";
                case PackageStatus.Returned:
                    return "Đã hoàn";
                case PackageStatus.Lost:
                    return "Thất lạc";
                case PackageStatus.Damaged:
                    return "Hư hỏng";
                default:
                    return status.ToString();
            }
        }

        public static string TranslatePaymentMethod(PaymentMethod method)
        {
            switch (method)
            {
                case PaymentMethod.COD:
                    return "Thu hộ COD";
                case PaymentMethod.Banking:
                    return "Chuyển khoản";
                case PaymentMethod.EWallet:
                    return "Ví điện tử";
                case PaymentMethod.Credit:
                    return "Ghi công nợ";
                default:
                    return method.ToString();
            }
        }

        public static string TranslateTransactionStatus(TransactionStatus status)
        {
            switch (status)
            {
                case TransactionStatus.Pending:
                    return "Chờ xử lý";
                case TransactionStatus.Completed:
                    return "Đã thanh toán";
                case TransactionStatus.Failed:
                    return "Thất bại";
                case TransactionStatus.Refunded:
                    return "Đã hoàn tiền";
                default:
                    return status.ToString();
            }
        }

        public static string TranslateIssueType(IssueType type)
        {
            switch (type)
            {
                case IssueType.Damaged:
                    return "Hư hỏng";
                case IssueType.Lost:
                    return "Thất lạc";
                case IssueType.Delay:
                    return "Giao trễ";
                case IssueType.WrongAddress:
                    return "Sai địa chỉ";
                default:
                    return type.ToString();
            }
        }

        public static string TranslateResolutionStatus(ResolutionStatus status)
        {
            switch (status)
            {
                case ResolutionStatus.Open:
                    return "Mới ghi nhận";
                case ResolutionStatus.Investigating:
                    return "Đang xử lý";
                case ResolutionStatus.Resolved:
                    return "Đã xử lý";
                default:
                    return status.ToString();
            }
        }

        public static string TranslateWarehouseType(WarehouseType type)
        {
            switch (type)
            {
                case WarehouseType.SortingHub:
                case WarehouseType.SortingCenter:
                    return "Trung tâm phân loại";
                case WarehouseType.DistributionCenter:
                    return "Trung tâm điều phối";
                case WarehouseType.TransitPoint:
                    return "Điểm trung chuyển";
                case WarehouseType.FulfillmentCenter:
                    return "Kho hoàn tất đơn";
                case WarehouseType.TransshipmentPoint:
                    return "Trạm trung chuyển";
                case WarehouseType.ColdStorage:
                    return "Kho lạnh";
                default:
                    return type.ToString();
            }
        }

        public static WarehouseType ParseWarehouseType(string translated)
        {
            switch (translated)
            {
                case "Trung tâm phân loại":
                    return WarehouseType.SortingHub;
                case "Trung tâm điều phối":
                    return WarehouseType.DistributionCenter;
                case "Điểm trung chuyển":
                    return WarehouseType.TransitPoint;
                case "Kho hoàn tất đơn":
                    return WarehouseType.FulfillmentCenter;
                case "Trạm trung chuyển":
                    return WarehouseType.TransshipmentPoint;
                case "Kho lạnh":
                    return WarehouseType.ColdStorage;
                default:
                    return WarehouseType.SortingHub;
            }
        }
    }
}
