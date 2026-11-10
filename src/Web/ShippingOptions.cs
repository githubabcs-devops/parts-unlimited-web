namespace GhAdoE2eDemo.Web;

public record ShippingOptions(bool IsDemo, string Currency, IReadOnlyList<ShippingOption> Options)
{
    public static ShippingOptions Demo => new(true, "USD",
    [
        new("standard", "Standard", 4.99m, 3, 5),
        new("express", "Express", 9.99m, 1, 2)
    ]);
}

public record ShippingOption(string Id, string Name, decimal Cost, int MinBusinessDays, int MaxBusinessDays);
