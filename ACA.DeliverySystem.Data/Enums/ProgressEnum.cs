using System.Runtime.Serialization;

public enum ProgressEnum
{
    [EnumMember(Value = "Created")]
    Created = 0,

    [EnumMember(Value = "InProgress")]
    InProgress = 1,

    [EnumMember(Value = "Cancel")]
    Cancel = 2,

    [EnumMember(Value = "Completed")]
    Completed = 3
}
