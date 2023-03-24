// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Models;

// namespace Controllers;

// [Route("api/[controller]")]
// [ApiController]
// public class VariantValueController : ControllerBase
// {
//     //private readonly Wmsdbv2IsaacContext db;
//     private readonly Wms2TestContext db;

//     public VariantValueController()
//     {
//         //db = new Wmsdbv2IsaacContext();
//         db = new Wms2TestContext();
//     }

//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<VariantValue>>> GetVariantValues()
//     {
//         return await db.VariantValues.ToListAsync();
//     }

//     [HttpGet("{id}")]
//     public async Task<ActionResult<VariantValue>> GetVariantValue(String id)
//     {
//         var t = await db.VariantValues.FindAsync(id);
//         if (t == null)
//             return NotFound();
//         return t;
//     }

//     [HttpPost]
//     public async Task<ActionResult<VariantValue>> PostVariantValue(VariantValueDTO variantvalue)
//     {
//         var vv = await VariantValueInsert(1, variantvalue);

//         return Ok(vv);
//     }

//     //[HttpPut]

//     [HttpDelete("{id}")]
//     public async Task<ActionResult<VariantValue>> DeleteTransaction(String id)
//     {
//         var t = await db.VariantValues.FindAsync(id);
//         if (t == null)
//             return NotFound();

//         db.VariantValues.Remove(t);
//         db.SaveChanges();

//         //return NoContent();
//         return BadRequest("deleted");
//     }


//     protected async Task<ActionResult<VariantValue>> VariantValueInsert(int flag, VariantValueDTO variantvalue)
//     {
//         var vv = new VariantValue();
//         var vrs = db.Variants.Select(x => x.VariantId).ToArray();

//         // 나중에 Attach로 구현하기
//         vv.VariantValueId = variantvalue.VariantValueId;
//         vv.VariantId = variantvalue.VariantId;
//         vv.Value = variantvalue.Value;
//         vv.DisplayPosition = variantvalue.DisplayPosition;

//         db.VariantValues.Add(vv);
//         await db.SaveChangesAsync();
//         return vv;
//     }
// }