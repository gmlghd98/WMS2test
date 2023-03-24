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
    public async Task<ActionResult<ProductVariant>> PostProductVariant(ProductVariantDTO productvariant)
    {

        var pv = await ProductVariantInsert(productvariant);

        return Ok(pv);
    }

    [HttpPut]
    public async Task<ActionResult<ProductVariant>> PutProductVariant(int n)
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



    protected async Task<ActionResult<ProductVariant>> ProductVariantInsert(ProductVariantDTO productvariant)
    {
        var pv = new ProductVariant();
        var pds = db.Products.Select(x => x.ProductId).ToArray();
        var vts = db.Variants.Select(x => x.VariantId).ToArray();

        var chkRandomCase = string.IsNullOrEmpty(productvariant.VariantId) && string.IsNullOrEmpty(productvariant.ProductId);
        if (chkRandomCase)
        {
            Random rnd = new Random(DateTime.UtcNow.Microsecond);

            productvariant.ProductId = pds[rnd.Next(0, pds.Length)];
            productvariant.VariantId = vts[rnd.Next(0, vts.Length)];

            String tempId = productvariant.ProductId + "_" + productvariant.VariantId;
            bool chkDupleCase = true;
            while (chkDupleCase) // 만들어진 ProductId와 VariantId를 토대로 ProductVariantId를 만든다.
            {
                // 수정 필요;중복 코드 발생
                chkDupleCase = (db.ProductVariants.Find(tempId) != null) ? true : false;    
                productvariant.ProductId = pds[rnd.Next(0, pds.Length)];    //둘 중 하나만 새로 받아서 id 만들고 싶은데....
                productvariant.VariantId = vts[rnd.Next(0, vts.Length)];
                tempId = productvariant.ProductId + "_" + productvariant.VariantId;
            }
            productvariant.ProductVariantId = tempId;
        }
        pv.ProductVariantId = productvariant.ProductVariantId;
        pv.ProductId = productvariant.ProductId;
        pv.VariantId = productvariant.VariantId;

        db.ProductVariants.Add(pv);
        await db.SaveChangesAsync();
        return pv;
    }
}