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
        var vv = await db.VariantValues.FindAsync(id);

        if (vv == null)
            return NotFound();

        return Ok(vv);
    }

    [HttpPost]
    public async Task<ActionResult<String>> PostVariant(VariantValueDTO variantvalue)
    {
        var vv = await VariantValueInsert(variantvalue);

        return Ok("posted");
    }

    [HttpPut]
    public async Task<ActionResult<String>> PutVariant(int n)
    {
        var variantvalue = new VariantValueDTO();

        await VariantValueInsert(variantvalue);

        return Ok("posted");
    }

    // [HttpPut]
    [HttpDelete]
    public async Task<ActionResult<string>> DeleteVariant(String id)
    {
        var v = db.VariantValues.Find(id);

        if (v == null)
            return NotFound();

        db.VariantValues.Remove(v);
        await db.SaveChangesAsync();

        return Ok("deleted");
    }


    protected async Task<ActionResult<VariantValue>> VariantValueInsert(VariantValueDTO variantvalue)
    {
        var v = new VariantValue();
        int[] exampleData = new int[] {10, 20, 30};

        v.VariantValueId = variantvalue.VariantValueId;
        v.VariantId = variantvalue.VariantId;
        v.Value = variantvalue.Value;
        
        db.VariantValues.Add(v);
        await db.SaveChangesAsync();
        return v;
    }
}