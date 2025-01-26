using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models;

public class Report
{
    public int ReportId { get; set; }

    [Display(Name = "گروه خبری")]
    [Required(ErrorMessage = "{0} را وارد کنید!")]
    public int ReportGroupId { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید!")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    [Display(Name = "عنوان خبر")]
    public string? Title { get; set; }

    [MaxLength(1000, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    [Display(Name = "توضیح مختصر")]
    public string? Description { get; set; }

    [Display(Name = "متن خبر")]
    public string? FullText { get; set; }

    [Display(Name = "تصویر خبر")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? ReportImage { get; set; }

    [Display(Name = "توضیح تصویر")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? ReportImageAlt { get; set; }

    [Display(Name = "عنوان تصویر")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? ImageTitle { get; set; }

    [Display(Name = "منبع خبر")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? ReportSource { get; set; }

    [Display(Name = "برچسب ها")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? Tags { get; set; }

    [Display(Name = "تاریخ درج خبر")]
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [Display(Name = " نویسنده خبر")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    public string? Author { get; set; }

    [Display(Name = "بازدید")]
    public int ReportView { get; set; }

    [Display(Name = "اخبار داغ")]
    public bool IsHotNews { get; set; }

    [Display(Name = "زمان درج اخبار داغ")]
    public DateTime? HotNewsDate { get; set; }

    #region Relations

    [Display(Name = "گروه خبری")]
    public virtual ReportGroup? ReportGroup { get; set; }

    #endregion
}