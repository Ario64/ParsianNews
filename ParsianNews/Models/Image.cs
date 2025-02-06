using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models
{
    public class Image
    {
        public int  ImageId { get; set; }

        [Display(Name = "نام گالری")]
        [Required(ErrorMessage = "{0} را وارد کنید.")]
        public int GalleryId { get; set; }

        [Display(Name = "نام تصویر")]
        [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از  {1} کاراکتر باشد .")]
        public string? ImageName { get; set; }

        [Display(Name = "تاریخ درج تصویر")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Relations

        [Display(Name = "نام گالری")]
        public Gallery? Gallery { get; set; }

        #endregion

    }
}
