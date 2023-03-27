using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductVariantController : ControllerBase
{
    private readonly Wms2TestContext db;

    public ProductVariantController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVariant>>> GetProductVariants()
    {
        return await db.ProductVariants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVariant>> GetProductVariant(String id)
    {
        var pv = await db.ProductVariants.FindAsync(id);

        if (pv == null)
            return NotFound();

        return Ok(pv);
    }

    [HttpPost]
    public ActionResult<String> PostProductVariant(ProductVariantDTO productvariant)
    {
        ProductVariantInsert(productvariant);
        return Ok("posted");
    }

    [HttpPut]
    public ActionResult<String> PutProductVariant(int n)
    {
        var productvariant = new ProductVariantDTO();

        for (int i = 0; i < n; i++)
        {
            productvariant = new ProductVariantDTO();
            ProductVariantInsert(productvariant);
        } // 수정 필요;쓸데없이 t를 만들어서 넘김
        return Ok("posted");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductVariant>> DeleteProductVariant(String id)
    {
        var pv = await db.ProductVariants.FindAsync(id);
        if (pv == null)
            return NotFound();

        db.ProductVariants.Remove(pv);
        db.SaveChanges();

        //return NoContent();
        return Ok("deleted");
    }



    protected void ProductVariantInsert(ProductVariantDTO productvariant)
    {
        var pv = new ProductVariant();

        var chkRandomCase = string.IsNullOrEmpty(productvariant.VariantId) && string.IsNullOrEmpty(productvariant.ProductId);
        if (chkRandomCase)
        {
            productvariant = MakeRandom(productvariant);
            return ;
        }
        InsertData(productvariant);
    }

    protected void InsertData(ProductVariantDTO productvariant)
    {
        var pv = new ProductVariant();
        pv.ProductVariantId = productvariant.ProductVariantId;
        pv.ProductId = productvariant.ProductId;
        pv.VariantId = productvariant.VariantId;

        //add하고 save까지 해버리자
        db.ProductVariants.Add(pv);
        db.SaveChanges();
    }
    protected ProductVariantDTO MakeRandom(ProductVariantDTO productvariant)
    {
        //variant 3개 골라서, Variant 고를 때마다 전체적으로 만들고 Insert 호출
        Random rnd = new Random();
        string[] pds = db.Products.Select(x => x.ProductId).ToArray();
        string[] vrs = db.Variants.Select(x => x.VariantId).ToArray();
        int Insert3times = 1;

        productvariant.ProductId = pds[rnd.Next(0, pds.Length)];
        while(Insert3times < 4)
        {
            do
            {
                productvariant.VariantId = vrs[rnd.Next(0, vrs.Length)];
            } while (db.ProductVariants.Where(x => x.VariantId == productvariant.VariantId) == null);
            String tempId = productvariant.ProductId + "_" + productvariant.VariantId;
            if(db.ProductVariants.Find(tempId) != null)
                continue;
            productvariant.ProductVariantId = tempId;
            
            InsertData(productvariant);
            Insert3times++;
        }
        return productvariant;
    }
}