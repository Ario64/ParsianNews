using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models;

public class Hashtag
{
    public int HashtagId { get; set; }

    [DisplayName("هشتگ")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد.")]
    [Required(ErrorMessage = "{0} را وارد کنید .")]
    public string HashtagName { get; set; } = default!;
}