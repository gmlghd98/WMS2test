using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly Wms2TestContext db;

    public InventoryController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetInventories()
    {
        var _ivsql = db.Inventories.AsQueryable();
        _ivsql = _ivsql.Where(x => x.InventoryComplexes.Count(s =>
            s.VariantComplex.ProductVariant.VariantId == "CT"
            && s.VariantComplex.VariantValueId == "3IN"
        ) > 0);
        //return await db.Inventories.ToListAsync();
        var iv = await _ivsql
            .Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.ProductVariant.Variant)
            .Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.VariantValue)
            .Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.ProductVariant.Product)
            .ToListAsync();

        var data = await _ivsql.Select(x => new InventoryDTO
        {
            InventoryId = x.InventoryId,
            ProductId = x.ProductId,
            Barcode = x.Barcode,
            Sku = x.Sku,
            CurrentQty = x.CurrentQty,
            Variants = x.InventoryComplexes.Select(s => new VariantInfo
            {
                VariantId = s.VariantComplex.ProductVariant.VariantId,
                VariantName = s.VariantComplex.ProductVariant.Variant.VariantName,
                VariantValueId = s.VariantComplex.VariantValueId,
                Value = s.VariantComplex.VariantValue.Value
            })
        }).ToArrayAsync();

        //return JsonConvert.SerializeObject(data);
        return Ok(iv);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Inventory>> GetInventory(String id)
    {
        var iv = await db.Inventories.Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.ProductVariant.Variant)
            .Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.VariantValue)
            .Include(x => x.InventoryComplexes)
            .ThenInclude(x => x.VariantComplex.ProductVariant.Product)
            .FirstOrDefaultAsync(x => x.InventoryId == id);

        var data = new InventoryDTO
        {
            InventoryId = iv.InventoryId,
            ProductId = iv.ProductId,
            Barcode = iv.Barcode,
            Sku = iv.Sku,
            CurrentQty = iv.CurrentQty,
            Variants = iv.InventoryComplexes.Select(s => new VariantInfo{
                VariantId = iv.
            })
        };

        if (data == null)
            return NoContent();

        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult<String>> PostInventory(InventoryDTO inventory)
    {
        await InventoryInsert(inventory);
        return Ok("posted");
    }

    // [HttpPut("{id}")]
    //public ActionResult<String> PutInventory(String id, InventoryDTO inventory)
    // [HttpPut]
    // public ActionResult<String> PutInventory(int n)
    // {
    //     // var iv = db.Inventories.Find(id);
    //     // if(iv == null)
    //     //     return NoContent();
    //     // iv.Sku = inventory.Sku;
    //     // iv.Barcode = inventory.Barcode;
    //     // iv.CurrentQty = inventory.CurrentQty;
    //     // iv.ProductId = inventory.ProductId;

    //     // db.SaveChanges();
    //     // return Ok("Updated");

    //     var inventory = new InventoryDTO();
    //     for(int i = 0; i< n;i++)
    //     {
    //         InventoryInsert(inventory);
    //     }
    //     return Ok("posted");
    // }

    [HttpDelete("{id}")]
    public ActionResult<String> DeleteInventory(String id)
    {
        var iv = db.Inventories.Find(id);
        if (iv == null)
            return NoContent();

        db.Inventories.Remove(iv);
        db.SaveChanges();
        return Ok("deleted");
    }



    protected async Task InventoryInsert(InventoryDTO inventory)
    {
        var iv = new Inventory();
        iv.Sku = inventory.Sku;
        iv.Barcode = inventory.Barcode;
        iv.CurrentQty = inventory.CurrentQty;
        iv.ProductId = inventory.ProductId;
        iv.InventoryId = inventory.InventoryId;

        db.Inventories.Add(iv);
        await db.SaveChangesAsync();
    }
}