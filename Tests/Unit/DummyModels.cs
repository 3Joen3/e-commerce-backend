using Core.Interfaces.Contracts;

namespace Tests.Unit;

public class TestAttributeInput : IVariantAttributeCreate
{
    public string Title { get; set; } = "";
    public string Value { get; set; } = "";
}