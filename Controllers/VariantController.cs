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
        //return await db.Variants.Include(x => x.ProductVariants).ThenInclude(y => y.Product).ToListAsync();
        return await db.Variants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Variant>> GetVariant(String id)
    {
        //var v = await db.Variants.Include(x => x.ProductVariants).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.VariantId == id);
        var v = await db.Variants.FindAsync(id);

        if (v == null)
            return NotFound();

        return Ok(v);
    }

    [HttpPost]
    public ActionResult<String> PostVariant(VariantDTO variant)
    {
        var v = VariantInsert(variant);
        return Ok("posted");
    }

    [HttpPut]
    public ActionResult<String> PutVariant(int n)
    {
        var variant = new VariantDTO();
        return Ok("posted");
    }

    // [HttpPut]
    [HttpDelete]
    public async Task<ActionResult<String>> DeleteVariant(String id)
    {
        var v = db.Variants.Find(id);

        if (v == null)
            return NotFound();

        db.Variants.Remove(v);
        await db.SaveChangesAsync();

        return Ok("deleted");
    }


    protected async Task<ActionResult<Variant>> VariantInsert(VariantDTO variant)
    {
        var v = new Variant();
        //생각한다고 하면 끝도 없는데;;
        //shape, row, height, depth, color, weight, power, resolution, waist, material, gender,etc 
        string[] vrs = {"Shape", "Row", "Depth", "Resolution", "Waist", "Material", "Storage"};
        Random rnd = new Random();
        String vrchoice;
        String vrid;
        char[] vrtemp;

        var chkrandomcase = string.IsNullOrEmpty(variant.VariantName) && (variant.DisplayPosition == 0);
        if (chkrandomcase)
        {
            //variant.VariantId = "v" + rnd.Next(0, 100); //수정 필요;의미 구분 못함
            // variant.VariantName = vrs[rnd.Next(0, vrs.Length)];
            do
            {
                vrchoice = vrs[rnd.Next(0, vrs.Length)];
                vrtemp = vrchoice.ToCharArray();
                vrid = new string(vrtemp, 0, 3).ToUpper();
            } while (db.Variants.Find(vrid) == null);
            variant.VariantId = vrid;
            variant.VariantName = vrchoice;
            variant.DisplayPosition = db.Variants.Select(x => x.DisplayPosition).Max() + 1;//그냥 우선 가장 마지막 variant보다 1 더해서 DisplayPosition 잡기 
        }

        v.VariantId = variant.VariantId;
        v.VariantName = variant.VariantName;
        v.DisplayPosition = variant.DisplayPosition; //이게 사용자한테 받는게 맞는건가...?

        db.Variants.Add(v);
        await db.SaveChangesAsync();
        return v;
    }
}