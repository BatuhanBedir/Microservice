using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "il")]
        public string Province { get; set; }
        [Display(Name = "ilçe")]
        public string District { get; set; }
        [Display(Name = "sokak")]
        public string Street { get; set; }
        [Display(Name = "posta kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "adres")]
        public string Line { get; set; }

        [Display(Name = "Kart isim soyisim")]
        public string CardName { get; set; }
        [Display(Name = "Kart numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "son kullanma tarihi(ay/yıl)")]
        public string Expiration { get; set; }
        [Display(Name = "CVV/CVC2 numarası")]
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
