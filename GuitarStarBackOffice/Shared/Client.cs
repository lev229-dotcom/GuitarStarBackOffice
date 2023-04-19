using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GuitarStarBackOffice.Shared;

public class Client
{
    public Guid IdClient { get; set; }

    public string ClientName { get; set; }

    public string ClientLastName { get; set; }

    public string ClientPassword { get; set; }

    public string ClientEmail { get; set; }

    public string ClientNumber { get; set; }

    public string ClientAddress { get; set; }

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    /// <summary>
    /// Список избранного
    /// </summary>
    [Display(Name = "Список избранного")]
    public ICollection<WishlistElements> WishlistElements { get; set; } = new HashSet<WishlistElements>();
}
