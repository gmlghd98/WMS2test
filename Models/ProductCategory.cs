﻿using System;
using System.Collections.Generic;

namespace Models;

public partial class ProductCategory
{
    public string ProductCategoryId { get; set; } = null!;

    public string? ProductCategoryName { get; set; }

    public string? Description { get; set; }

    public string? ParentProductCategoryId { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
