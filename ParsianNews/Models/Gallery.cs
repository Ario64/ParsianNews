using System;
using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models
{
    public class Gallery
    {
        public int GalleryId { get; set; }

        [Display(Name = "نام گالری")]
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از  {1} کاراکتر باشد .")]
        public string GalleryName { get; set; } = default!;

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [MaxLength(600, ErrorMessage = "{0} نباید بیشتر از  {1} کاراکتر باشد .")]
        public string? Description { get; set; }

        #region Relations

        public List<Image>? Images { get; set; }

        #endregion
    }
}
