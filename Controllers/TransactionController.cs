using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

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
        //var CurrentQty = Calculator(id);
        int sum = Sum(id);
        return Ok("CurrentQty = " + sum);

        //return await db.Transactions.Where(x => x.ProductId == id).ToListAsync();

        // var t = await db.Transactions.FindAsync(id);
        // if (t == null)
        //     return NotFound();
        // return t;
    }


    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transaction)
    {
        await TransactionCreate(transaction);
        // if()
        //     return BadRequest("post failed");
        return Ok("posted");
    }

    [HttpPut] 
    public async Task<ActionResult<Transaction>> PutTransaction(int Times)
    {
        var transaction = new TransactionDTO();

        for (int i = 0; i < Times; i++)
        {
            transaction = new TransactionDTO();
            await TransactionCreate(transaction);
        } // 수정 필요;쓸데없이 transaction을 만들어서 넘김
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




    protected async Task<ActionResult<Transaction>> TransactionCreate(TransactionDTO transaction)
    {
        var t = new Transaction();
        var pds = db.Products.Select(x => x.ProductId).ToArray();
        string[] Type = new string[] {"OUT"};
        //string[] Type = new string[] { "IN", "OUT" };

        var chkRandomCase = string.IsNullOrEmpty(transaction.TransactionType) && string.IsNullOrEmpty(transaction.ProductId);
        Random rnd = new Random(DateTime.UtcNow.Microsecond);

        if (chkRandomCase)
        {   // put을 할 때, Random의 범위를 지정할 수 있게 해보자!!!
            transaction.ProductId = pds[rnd.Next(0, 30)]; //(pds.Length - 950)
            bool chkDupleCase;
            String tempId;
            do
            {
                tempId = "T" + rnd.Next(1, 1000);
                chkDupleCase = (db.Transactions.Find(tempId) != null) ? true : false;
            }while (chkDupleCase);
            transaction.TransactionId = tempId;
            transaction.ProductQty = rnd.Next(1, 50);
            transaction.TransactionType = Type[rnd.Next(0, Type.Length)];
        }
        t.TransactionId = transaction.TransactionId;
        t.ProductId = transaction.ProductId;
        t.ProductQty = transaction.ProductQty;
        t.TransactionType = transaction.TransactionType;

        if ((t.TransactionType == "OUT" && t.ProductQty <= Calculator(t.ProductId)) || (t.TransactionType == "IN"))
        {
            db.Transactions.Add(t);
            await db.SaveChangesAsync();
        }
        
        return Ok(t);
    }

    protected int Sum(String id)
    {
        // var InTs = db.Transactions.Where(x=>x.ProductId == id && x.TransactionType == "IN").Sum(x=>x.ProductQty);
        // var OutTs = db.Transactions.Where(x=>x.ProductId == id && x.TransactionType == "Out").Sum(x=>x.ProductQty);
        // var trxsum = ((InTs - OutTs) ?? 0);
        var qty = db.Transactions
            .Where(x=>x.ProductId == id)
            .Sum(x=>x.TransactionType == "IN" ? (x.ProductQty ?? 0) : (x.ProductQty ?? 0 * -1));
        return qty;
    }
    protected int Calculator(String id)
    {
        //In인 것들과 Out인 것들을 서로 다른 배열로 분리해서 해야할까..?
        var ts = db.Transactions.Where(x => x.ProductId == id).ToArray();
        int CurrentQty = 0;



        for (int i = 0; i < ts.Length; i++)
        {
            if (ts[i].ProductQty == null)
                continue;   //null인 경우에는 계산에 포함 x 
            else
                CurrentQty += ts[i].TransactionType == "IN" ? (int)ts[i].ProductQty : ((int)ts[i].ProductQty * (-1));
        }

        if (CurrentQty < 0)
            return 0;

        return CurrentQty;
    }
}