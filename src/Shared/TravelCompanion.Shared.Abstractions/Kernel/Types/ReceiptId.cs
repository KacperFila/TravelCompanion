using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class ReceiptId : TypeId
{
    public ReceiptId(Guid value) : base(value)
    {
    }
    public static implicit operator ReceiptId(Guid id) => new (id);
}