using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantController : ControllerBase
{
    private readonly Wms2TestContext db;

    public VariantController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Variant>>> GetVariants()
    {
        return await db.Variants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Variant>> GetVariant(String id)
    {
        var v = await db.Variants.FindAsync(id);

        if (v == null)
            return NotFound();

        return Ok(v);
    }

    protected async Task<ActionResult<Variant>> VariantInsert(VariantDTO variant)
    {
        var v = new Variant();
        //생각한다고 하면 끝도 없는데;;
        //shape, row, height, depth, color, weight, power, resolution, waist, material, gender, 
        string[] vrs = {"Shape", "Row", "Depth", "Resolution", "Waist", "Material", "Gender", "Storage"};
        Random rnd = new Random();
        string[] temp;

        var chkrandomcase = string.IsNullOrEmpty(variant.VariantName) && (variant.DisplayPosition == 0);
        if(chkrandomcase)
        {
            variant.VariantId = "v" + rnd.Next(0, 100); //수정 필요;의미 구분 못함
            variant.VariantName = vrs[rnd.Next(0, vrs.Length)];
            variant.DisplayPosition = db.Variants.; //그냥 우선 가장 마지막 variant보다 1 더해서 DisplayPosition 잡기 
        }

        v.VariantId = variant.VariantId;
        v.VariantName = variant.VariantName;
        v.DisplayPosition = variant.DisplayPosition;

        db.Variants.Add(v);
        await db.SaveChangesAsync();
        return v;
    }

    [HttpPost]
    public async Task<ActionResult<String>> PostVariant(VariantDTO variant)
    {
        var v = await VariantInsert(variant);

        return Ok("posted");
    }

    [HttpPut]
    public async Task<ActionResult<String>> PutVariant(int n)
    {
        var variant = new VariantDTO();

        return Ok("posted");
    }

    // [HttpPut]

    [HttpDelete]
    public async Task<ActionResult<Variant>> DeleteVariant(String id)
    {
        var v = db.Variants.Find(id);

        if (v == null)
            return NotFound();

        db.Variants.Remove(v);
        await db.SaveChangesAsync();

        return NoContent();
    }
}