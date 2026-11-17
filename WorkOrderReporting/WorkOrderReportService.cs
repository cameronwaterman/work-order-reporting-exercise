namespace NationalInstruments.WorkOrderReporting
{
    public sealed class WorkOrderReportService
    {
        public IReadOnlyList<OverdueWorkOrder> GetOverdueReport(
            IReadOnlyList<WorkOrder> orders,
            DateTime today)
        {
            var report = new List<OverdueWorkOrder>();

            foreach (var order in orders)
            {
                if (order.Status != WorkOrderStatus.Completed && order.DueDate.Date <= today.Date)
                {
                    report.Add(new OverdueWorkOrder(
                        order.Id,
                        order.EquipmentId,
                        (today.Date - order.DueDate.Date).Days));
                }
            }

            return report
                .OrderBy(order => order.DaysOverdue)
                .ToArray();
        }
    }
}
