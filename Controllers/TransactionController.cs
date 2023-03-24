using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    //private readonly Wmsdbv2IsaacContext db;
    private readonly Wms2TestContext db;

    public TransactionController()
    {
        //db = new Wmsdbv2IsaacContext();
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return await db.Transactions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<String>> GetTransaction(String id) //이거를 productID를 받는 걸 전제로 해야할까? 
    {
        var ts = await db.Transactions.Where(x => x.ProductId == id).ToArrayAsync(); //In인 것들과 Out인 것들을 서로 다른 배열로 분리해서 해야할까..?
        int CurrentQty = 0;
        // 그냥 In한 Qty에 Out한 Qty 빼면 되지 않을까? 그리고 두 개 비교해서 .....?
        for (int i = 0; i < ts.Length; i++)
        {
            if (ts[i].ProductQty == null) // 혹시라도 ProductQty가 null일 경우 대비
                continue;
            else
            {
                if (ts[i].TransactionType == "IN")
                    CurrentQty += (int)ts[i].ProductQty;
                else if (ts[i].TransactionType == "OUT")
                    CurrentQty -= (int)ts[i].ProductQty;
            }
        }

        if(CurrentQty < 0)
            return BadRequest("Transaction Fail");

        return Ok("CurrentQty = " + CurrentQty);

        //return await db.Transactions.Where(x => x.ProductId == id).ToListAsync();

        // var t = await db.Transactions.FindAsync(id);
        // if (t == null)
        //     return NotFound();
        // return t;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transaction)
    {
        var t = await TransactionInsert(transaction);

        return Ok(t);
    }

    [HttpPut]
    public async Task<ActionResult<Transaction>> PutTransaction(int n)
    {
        var transaction = new TransactionDTO();

        for (int i = 0; i < n; i++)
        {
            transaction = new TransactionDTO();
            await TransactionInsert(transaction);
        } // 수정 필요;쓸데없이 t를 만들어서 넘김
        return Ok("posted");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Transaction>> DeleteTransaction(String id)
    {
        var t = await db.Transactions.FindAsync(id);
        if (t == null)
            return NotFound();

        db.Transactions.Remove(t);
        db.SaveChanges();

        //return NoContent();
        return Ok("deleted");
    }

    protected async Task<ActionResult<Transaction>> TransactionInsert(TransactionDTO transaction)
    {
        var t = new Transaction();
        var pds = db.Products.Select(x => x.ProductId).ToArray();
        string[] Type = new string[] { "IN", "OUT" };

        var chkRandomCase = string.IsNullOrEmpty(transaction.TransactionType) && string.IsNullOrEmpty(transaction.ProductId);
        if (chkRandomCase)
        {
            Random rnd = new Random(DateTime.UtcNow.Microsecond);
            bool chkDupleCase = true;
            String tempId = "T" + rnd.Next();
            while (chkDupleCase)
            {
                chkDupleCase = (db.Transactions.Find(tempId) != null) ? true : false;
                tempId = "T" + rnd.Next();
            }
            transaction.TransactionId = tempId;
            transaction.ProductId = pds[rnd.Next(0, pds.Length)];
            transaction.ProductQty = rnd.Next(0, 50);
            transaction.TransactionType = Type[rnd.Next(0, Type.Length)];
        }
        t.TransactionId = transaction.TransactionId;
        t.ProductId = transaction.ProductId;
        t.ProductQty = transaction.ProductQty;
        t.TransactionType = transaction.TransactionType;

        db.Transactions.Add(t);
        await db.SaveChangesAsync();
        return t;
    }
}