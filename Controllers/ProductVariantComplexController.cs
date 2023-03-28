using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductVariantComplexController : ControllerBase
{
    private readonly Wms2TestContext db;

    public ProductVariantComplexController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVariantComplex>>> GetProductVariantComplexes()
    {
        //return await db.ProductVariantComplexes.ToListAsync();
        return await db.ProductVariantComplexes.
        Include(x => x.VariantValue).ThenInclude(x => x.Variant).
        Include(x => x.ProductVariant).ThenInclude(x => x.Product).ToListAsync();
    }

    [HttpGet("{id}")]
    public ActionResult<ProductVariantComplex> GetProductVariantComplex(String id)
    {
        if (db.ProductVariantComplexes.Select(x => x.VariantComplexId == id) == null)
            return NoContent();

        //var pvc = db.ProductVariantComplexes.FindAsync(id);
        var pvc = db.ProductVariantComplexes.
        Include(x => x.VariantValue).ThenInclude(x => x.Variant).
        Include(x=> x.ProductVariant).ThenInclude(x=> x.Product).Where(x => x.VariantComplexId == id);

        return Ok(pvc);
    }

    [HttpPost]
    public ActionResult<String> PostProductVariantComplex(ProductVariantComplexDTO productvariantcomplex)
    {
        ProductVariantComplexInsert(productvariantcomplex);
        return Ok("posted");
    }


    [HttpPut]
    public ActionResult<String> PutProductVariantComplex(int n)
    {
        var productvariantcomplex = new ProductVariantComplexDTO();

        for (int i = 0; i < n; i++)
        {
            ProductVariantComplexInsert(productvariantcomplex);
        }
        return Ok("posted");
    }

    [HttpDelete("{id}")]
    public ActionResult<ProductVariantComplex> DeleteProductVariantComplex(String id)
    {
        var pvc = db.ProductVariantComplexes.Find(id);
        if (pvc == null)
            return NoContent();

        db.ProductVariantComplexes.Remove(pvc);
        db.SaveChanges();
        return Ok("deleted");
    }


    protected void ProductVariantComplexInsert(ProductVariantComplexDTO productvariantcomplex)
    {
        //Random data 넣는 건 차후에 진행
        InsertData(productvariantcomplex);
    }

    protected void InsertData(ProductVariantComplexDTO productvariantcomplex)
    {
        var pvc = new ProductVariantComplex();
        pvc.ProductVariantId = productvariantcomplex.ProductVariantId;
        pvc.VariantValueId = productvariantcomplex.VariantValueId;
        pvc.VariantComplexId = productvariantcomplex.VariantComplexId;

        db.ProductVariantComplexes.Add(pvc);
        db.SaveChanges();
    }
}