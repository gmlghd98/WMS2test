using Models;

public partial class ProductDTO
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ProductCategoryId { get; set; }

    public string? ItemCode { get; set; }

    public int? BillingUnit { get; set; }

    public int? BillingQty { get; set; }
}

public partial class TransactionDTO
{
    public string TransactionId { get; set; } = null!;

    public string? InventoryId { get; set; }

    public string? TransactionType { get; set; }

    public string? BoardId { get; set; }

    public int? ProductQty { get; set; }

    public string? ProductId { get; set; }

    public DateTime? TransactionDate { get; set; }
}

public partial class VariantDTO
{
    public string VariantId { get; set; } = null!;

    public string? VariantName { get; set; }

    public int? DisplayPosition { get; set; }
}

public partial class VariantValueDTO
{
    public string VariantValueId { get; set; } = null!;

    public string? VariantId { get; set; }

    public string? Value { get; set; }

    public int? DisplayPosition { get; set; }
}

public partial class ProductVariantDTO
{
    public string ProductVariantId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? VariantId { get; set; }

    public int? DisplayPosition { get; set; }
}

public partial class ProductVariantComplexDTO
{
    public string VariantComplexId { get; set; } = null!;

    public string? ProductVariantId { get; set; }

    public string? VariantValueId { get; set; }
}

public class InventoryDTO
{
    public string InventoryId { get; set; } = null!;

    public string? Sku { get; set; }

    public string? Barcode { get; set; }

    public int? CurrentQty { get; set; }

    public string? ProductId { get; set; }
    public IEnumerable<VariantInfo?>?  Variants {get;set;}
}


public class VariantInfo {
    public string? VariantId{get;set;}
    public string VariantName{get;set;}
    public string Value{get;set;}
        public string? VariantValueId{get;set;}
}