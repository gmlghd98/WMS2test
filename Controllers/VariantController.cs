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

    protected async Task<ActionResult<Variant>> VariantInsert(int flag, VariantDTO variant)
    {
        var v = new Variant();

        // 나중에 Attach로 구현하기
        v.VariantId = variant.VariantId;
        v.VariantName = variant.VariantName;
        v.DisplayPosition = variant.DisplayPosition;

        // switch (flag)
        // {
        //     case 0:
        //         {
        //             Random rnd = new Random(DateTime.UtcNow.Microsecond);

        //             String tempId = "" + rnd.Next();
        //             if (db.Variants.Find(tempId) != null)
        //                 goto case 0; //성능 저하되지 않을까?

        //             variant.VariantId = tempId;
                    

        //             goto default;
        //         }
        //     case 1:
        //         {
        //             goto default;
        //         }
        //     default:
        //         {
        //             v.VariantId = variant.VariantId;
        //             v.VariantName = variant.VariantName;
        //             v.DisplayPosition = variant.DisplayPosition;

        //             break;
        //         }
        // }

        db.Variants.Add(v);
        await db.SaveChangesAsync();
        return v;
    }

    [HttpPost]
    public async Task<ActionResult<Variant>> PostVariant(VariantDTO variant)
    {
        var v = await VariantInsert(1, variant);

        return Ok(v);
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