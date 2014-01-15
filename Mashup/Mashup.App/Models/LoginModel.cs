namespace Mashup.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Niewłaściwy format adresu email")]
        [Display(Name = "Adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        /// <summary>
        /// Określenie, czy logowany użytkownik ma być zapamiętany
        /// </summary>
        public bool RememberMe { get; set; }
    }
}