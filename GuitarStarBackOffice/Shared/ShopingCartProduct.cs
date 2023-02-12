using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

public class ShoppingCartProduct
{
    public Guid Id { get; set; }

    public ShoppingCart ShoppingCart { get; set; }

    public Guid ShoppingCartId { get; set; }

    public Product Product { get; set; }

    public Guid ProductId { get; set; }
}
