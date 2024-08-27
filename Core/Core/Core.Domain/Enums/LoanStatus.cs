using System.Runtime.Serialization;

namespace Core.Domain.Enums;

public enum LoanStatus
{
    [EnumMember(Value = "PENDING")] PENDING,
    [EnumMember(Value = "APPROVED")] APPROVED,
    [EnumMember(Value = "REJECTED")] REJECTED
}