namespace NationalInstruments.WorkOrderReporting
{
    public sealed record OverdueWorkOrder(
        string Id,
        string EquipmentId,
        int DaysOverdue);
}
