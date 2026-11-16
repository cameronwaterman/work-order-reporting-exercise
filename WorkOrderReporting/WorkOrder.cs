namespace NationalInstruments.WorkOrderReporting
{
    public sealed record WorkOrder(
        string Id,
        string EquipmentId,
        WorkOrderStatus Status,
        DateTime DueDate);
}
