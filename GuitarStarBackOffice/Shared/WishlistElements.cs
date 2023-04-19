using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GuitarStarBackOffice.Shared
{
    public class WishlistElements
    {
        public Guid IdWishlistElement { get; set; }

        #region Id Клиента

        /// <summary>
        /// Id заказа 
        /// </summary>
        [Display(Name = "Id клиента")]
        [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
        public Client Client { get; set; }

        public Guid ClientId { get; set; }

        #endregion

        #region Id товара

        /// <summary>
        /// Id товара 
        /// </summary>
        [Display(Name = "Id товара")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
        public Guid? ProductId { get; set; }

        #endregion
    }
}
