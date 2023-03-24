using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.IO;
using Newtonsoft.Json;


namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly Wms2TestContext db;

    public ProductController()
    {
        db = new Wms2TestContext();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await db.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(String id)
    {
        var item = await db.Products.FindAsync(id);
        if (item == null)
            return NotFound();

        return item; // 수정 필요
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
    {
        var item = await ProductInsert(product);

        return Ok(item);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> PutProduct(int n)
    {
        var item = new ProductDTO();

        for (int i = 0; i < n; i++)
        {
            item = new ProductDTO();
            await ProductInsert(item);
        }
        return Ok("posted");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(String id)
    {
        var item = await db.Products.FindAsync(id);
        if (item == null)
            return NotFound();

        db.Products.Remove(item);
        await db.SaveChangesAsync();

        //return NoContent();
        return Ok("deleted");
    }

    protected async Task<Product> ProductInsert(ProductDTO product)
    {
        var item = new Product();

        string[] names = GetArray(); // 수정 필요
        string[] categorys = new string[] { "BF", "CTH", "ELT", "FN", "MT", "P_BLY", "PK", "SST", "T" };

        var chkRandomCase = string.IsNullOrEmpty(product.ProductName) && string.IsNullOrEmpty(product.ItemCode);
        if (chkRandomCase)
        {
            Random rnd = new Random(DateTime.UtcNow.Microsecond);
            bool chkDupleCase = true;
            String tempId = "id" + rnd.Next(); //수정 필요; 비효율적 ㅠㅠ
            while (chkDupleCase) 
            {
                chkDupleCase = (db.Products.Find(tempId) != null) ? true : false; 
                tempId = "T" + rnd.Next();
            }
            product.ProductId = tempId;
            product.ProductName = names[rnd.Next(0, names.Length)] + " " + rnd.Next(); // 수정 필요
            product.ItemCode = "code" + rnd.Next();
            product.ProductCategoryId = categorys[rnd.Next(0, categorys.Length)];
            product.BillingUnit = null;
            product.BillingQty = rnd.Next(0, 50);
        }
        item.ProductId = product.ProductId;
        item.ProductName = product.ProductName;
        item.ProductCategoryId = product.ProductCategoryId;
        item.ItemCode = product.ItemCode;
        item.BillingQty = product.BillingQty;
        item.BillingUnit = product.BillingUnit;

        db.Products.Add(item);
        await db.SaveChangesAsync();
        return item;
    }






    private string[] GetArray()
    {
        return new string[] {
"Muffin - Mix - Creme Brule 15l",
"Tart Shells - Sweet, 4",
"Lemonade - Black Cherry, 591 Ml",
"Corn - Cream, Canned",
"Butter - Salted, Micro",
"Mushroom - Enoki, Dry",
"Magnotta Bel Paese Red",
"Alize Red Passion",
"Onions - White",
"Pears - Bartlett",
"Cheese - Valancey",
"Irish Cream - Baileys",
"Cheese - Oka",
"Container Clear 8 Oz",
"Lotus Rootlets - Canned",
"Flour - Rye",
"Veal - Slab Bacon",
"Anchovy Fillets",
"Wine - Spumante Bambino White",
"Beef - Cow Feet Split",
"Brandy Cherry - Mcguinness",
"Nut - Almond, Blanched, Sliced",
"Pastry - Apple Large",
"Garbag Bags - Black",
"Bread - Flat Bread",
"Pepper - Black, Crushed",
"Chef Hat 20cm",
"Beef - Kobe Striploin",
"Nut - Walnut, Pieces",
"Fruit Salad Deluxe",
"Soup - Campbells, Butternut",
"Bandage - Flexible Neon",
"Noodles - Cellophane, Thin",
"Bread - Italian Roll With Herbs",
"Sprouts - Peppercress",
"Vinegar - Raspberry",
"Wine - White, Gewurtzraminer",
"Muffin - Mix - Strawberry Rhubarb",
"Cumin - Whole",
"Dill Weed - Dry",
"Eggwhite Frozen",
"Creme De Cacao White",
"Compound - Pear",
"Juice - Lime",
"Daves Island Stinger",
"Beef - Tender Tips",
"Pastry - Cheese Baked Scones",
"Steampan - Lid For Half Size",
"Flour - Teff",
"Longos - Greek Salad",
"Pail - 4l White, With Handle",
"Pepper - Paprika, Spanish",
"Pepper - Pablano",
"Lettuce - Mini Greens, Whole",
"Pepper - Black, Ground",
"Table Cloth 62x120 Colour",
"Longos - Penne With Pesto",
"Aspic - Clear",
"Eggplant - Regular",
"Clementine",
"Beef - Outside, Round",
"Chocolate - Pistoles, Lactee, Milk",
"Huck Towels White",
"Saskatoon Berries - Frozen",
"Bread - Flat Bread",
"Guava",
"Sole - Iqf",
"Cake Sheet Combo Party Pack",
"Veal - Liver",
"Soupcontfoam16oz 116con",
"Steel Wool S.o.s",
"The Pop Shoppe Pinapple",
"Pasta - Shells, Medium, Dry",
"Onions - Dried, Chopped",
"Pasta - Fettuccine, Dry",
"Beef - Texas Style Burger",
"Ginger - Fresh",
"White Fish - Filets",
"Sherry - Dry",
"Cheese - Brie",
"Table Cloth 62x120 White",
"Spic And Span All Purpose",
"Rhubarb",
"Fish - Halibut, Cold Smoked",
"Pate - Peppercorn",
"Pepper - Cayenne",
"Pineapple - Canned, Rings",
"Capon - Whole",
"Boogies",
"Pancetta",
"Beer - Alexander Kieths, Pale Ale",
"Flour - Strong",
"Wine - Tribal Sauvignon",
"Pancetta",
"Wiberg Cure",
"Bagel - Sesame Seed Presliced",
"Juice - Mango",
"Napkin - Dinner, White",
"Creamers - 10%",
"Vaccum Bag - 14x20"
    };
    }
}
