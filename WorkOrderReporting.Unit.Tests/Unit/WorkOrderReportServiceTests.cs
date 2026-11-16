using NationalInstruments.WorkOrderReporting;
using Xunit;

namespace NationalInstruments.Tests.Unit.WorkOrderReporting
{
    public sealed class WorkOrderReportServiceTests
    {
        [Fact]
        public void EmptyOrders_GetOverdueReport_ReturnsEmpty()
        {
            var service = new WorkOrderReportService();

            var result = service.GetOverdueReport(Array.Empty<WorkOrder>(), new DateTime(2026, 11, 17));

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(WorkOrderStatus.Pending)]
        [InlineData(WorkOrderStatus.InProgress)]
        public void ActiveOrderDueYesterday_GetOverdueReport_ReturnsProjectedOrder(WorkOrderStatus status)
        {
            var service = new WorkOrderReportService();
            var orders = new[] { new WorkOrder("A", "scope-1", status, new DateTime(2026, 11, 16, 18, 0, 0)) };

            var result = service.GetOverdueReport(orders, new DateTime(2026, 11, 17, 9, 0, 0));

            Assert.Equal(new OverdueWorkOrder("A", "scope-1", 1), Assert.Single(result));
        }

        [Fact]
        public void CompletedAndFutureOrders_GetOverdueReport_ReturnsEmpty()
        {
            var service = new WorkOrderReportService();
            var orders = new[]
            {
                new WorkOrder("C", "scope-3", WorkOrderStatus.Completed, new DateTime(2026, 11, 7)),
                new WorkOrder("F", "meter-6", WorkOrderStatus.InProgress, new DateTime(2026, 11, 19)),
            };

            var result = service.GetOverdueReport(orders, new DateTime(2026, 11, 17));

            Assert.Empty(result);
        }

        [Fact]
        public void UnsortedInput_GetOverdueReport_PreservesInputRecordsAndOrder()
        {
            var service = new WorkOrderReportService();
            var orders = new List<WorkOrder>
            {
                new WorkOrder("A", "scope-1", WorkOrderStatus.Pending, new DateTime(2026, 11, 14)),
                new WorkOrder("B", "meter-2", WorkOrderStatus.InProgress, new DateTime(2026, 11, 12)),
            };
            var snapshot = orders.ToArray();

            _ = service.GetOverdueReport(orders, new DateTime(2026, 11, 17));

            Assert.Equal(snapshot, orders);
        }
    }
}
