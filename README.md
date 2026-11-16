# Overdue work-order report

A small C# library for reporting overdue equipment work orders, with xUnit tests.
It uses in-memory data and has no database, network, or real-clock dependency.

## Setup

Requires Git, the .NET 10 SDK, a C# editor, and internet for package restore.
Use VS Code with C# Dev Kit, Visual Studio 2026 (18.0+).

Open `WorkOrderReporting.slnx` in your IDE, or the repository folder in Visual Studio This is a library, not an executable; use the tests to run its behavior.

## Contract

- `GetOverdueReport(orders, today)` takes orders with `Id`, `EquipmentId`,
  `Status` (Pending, InProgress, Completed, Cancelled), and `DateTime DueDate`.
- `today` is a `DateTime`. Dates use the same time zone; ignore time of day.
- Include only Pending/InProgress orders with `DueDate.Date < today.Date`.
- Return one `OverdueWorkOrder` per match, preserving `Id`/`EquipmentId`, with
  `DaysOverdue = (today.Date - DueDate.Date).Days`.
- Sort by `DaysOverdue` descending (most overdue first). Ties may be in any order.
- No matches: empty collection. Inputs are not modified.

## Example

With `today = new DateTime(2026, 11, 17)`:

| Id | EquipmentId | Status | DueDate |
| --- | --- | --- | --- |
| A | scope-1 | Pending | 2026-11-14 |
| B | meter-2 | InProgress | 2026-11-12 |
| C | scope-3 | Completed | 2026-11-07 |
| D | meter-4 | Cancelled | 2026-11-09 |
| E | scope-5 | Pending | 2026-11-17 |
| F | meter-6 | InProgress | 2026-11-19 |

Expected `(Id, EquipmentId, DaysOverdue)`:
`(B, meter-2, 5)`, `(A, scope-1, 3)`, in that order.

All example data is synthetic.
