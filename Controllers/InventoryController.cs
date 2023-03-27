using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvetoryController : ControllerBase
{
    private readonly Wms2TestContext db;

    public InvetoryController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
    {
        return await db.Inventories.ToListAsync();
    }

    [HttpGet("{id}")]
    public ActionResult<InventoryDTO> GetInventory(String id)
    {
        var iv = db.Inventories.Include(x=>x.InventoryComplexes).ThenInclude(x=>x.VariantComplex.ProductVariant).FirstOrDefault(x=>x.InventoryId == id);
        
        if (iv == null)
            return NoContent();

        InventoryDTO _dto = new InventoryDTO{
            InventoryId = iv.InventoryId,
            Sku = iv.Sku,
            pvariants = iv.InventoryComplexes.Select(s=>s.VariantComplex.ProductVariant).ToArray()
        };
        return _dto;
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

        //이거 두 개를 그냥 따로 빼면, 이걸 가지고 update도 가능하겠는데....?
        //한다면 inventory를 반환하고, 호출한 곳에서 반환값을 Add 및 Savechanges 하면 될 듯...
        db.Inventories.Add(iv);
        await db.SaveChangesAsync();
    }
}