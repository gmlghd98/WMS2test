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
    public async Task<ActionResult<String>> PostProductVariant(ProductVariantDTO productvariant)
    {
        var pv = await ProductVariantInsert(productvariant);

        return Ok("posted");
    }

    [HttpPut]
    public async Task<ActionResult<String>> PutProductVariant(int n)
    {
        var productvariant = new ProductVariantDTO();
        for (int i = 0; i < n; i++)
        {
            productvariant = new ProductVariantDTO();
            await ProductVariantInsert(productvariant);
        } // 수정 필요;쓸데없이 t를 만들어서 넘김
        return Ok("posted");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> DeleteProductVariant(String id)
    {
        var pv = await db.ProductVariants.FindAsync(id);
        if (pv == null)
            return NotFound();

        db.ProductVariants.Remove(pv);
        db.SaveChanges();

        //return NoContent();
        return Ok("deleted");
    }



    protected async Task<ActionResult<ProductVariant>> ProductVariantInsert(ProductVariantDTO productvariant)
    {
        var pv = new ProductVariant();

        var chkRandomCase = string.IsNullOrEmpty(productvariant.VariantId) && string.IsNullOrEmpty(productvariant.ProductId);
        if (chkRandomCase)
        {
            productvariant = VariantChoice(productvariant);
        }
        pv.ProductVariantId = productvariant.ProductVariantId;
        pv.ProductId = productvariant.ProductId;
        pv.VariantId = productvariant.VariantId;

        db.ProductVariants.Add(pv);
        await db.SaveChangesAsync();
        return pv;
    }

    protected ProductVariantDTO VariantChoice(ProductVariantDTO productVariant)
    {
        // var pds = db.Products.Select(x => x.ProductId).ToArray();
        // var vrs = db.Variants.Select(x => x.VariantId).ToArray();

        // Random rnd = new Random(DateTime.UtcNow.Microsecond);

        // productVariant.ProductId = pds[rnd.Next(0, 30)];
        // String ChosenVariant;
        
        // do
        // {
        //     ChosenVariant= vrs[rnd.Next(0, vrs.Length)];
        // }while(db.ProductVariants.Find(ChosenVariant) == null);
        // id는 제일 마지막에 product와 variant로 조합해서 만들 것!
        return productVariant;
    }
}