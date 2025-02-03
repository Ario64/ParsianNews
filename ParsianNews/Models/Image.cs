using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models
{
    public class Image
    {
        public int  ImageId { get; set; }

        public int GalleryId { get; set; }

        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از  {1} کاراکتر باشد .")]
        public string ImageName { get; set; } = default!;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Relations

        [Display(Name = "نام گالری")]
        public Gallery? Gallery { get; set; }

        #endregion
    }
}
