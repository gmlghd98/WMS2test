using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantValueController : ControllerBase
{
    private readonly Wms2TestContext db;

    public VariantValueController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VariantValue>>> GetVariantValues()
    {
        return await db.VariantValues.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VariantValue>> GetVariantValue(String id)
    {
        var v = await db.VariantValues.FindAsync(id);

        if (v == null)
            return NotFound();

        return Ok(v);
    }

    [HttpPost]
    public async Task<ActionResult<String>> PostVariant(VariantValueDTO variant)
    {
        var v = await VariantValueInsert(variant);

        return Ok("posted");
    }

    [HttpPut]
    public async Task<ActionResult<String>> PutVariant(int n)
    {
        var variant = new VariantValueDTO();

        await VariantValueInsert(variant);

        return Ok("posted");
    }

    // [HttpPut]
    [HttpDelete]
    public async Task<ActionResult<string>> DeleteVariant(String id)
    {
        var v = db.Variants.Find(id);

        if (v == null)
            return NotFound();

        db.Variants.Remove(v);
        await db.SaveChangesAsync();

        return Ok("deleted");
    }


    protected async Task<ActionResult<VariantValue>> VariantValueInsert(VariantValueDTO variant)
    {
        var v = new VariantValue();

        db.VariantValues.Add(v);
        await db.SaveChangesAsync();
        return v;
    }
}