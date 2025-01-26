using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParsianNews.Models;

public class ReportGroup
{
    public int GroupId { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید!")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    [Display(Name = "گروه خبری")]
    public string?  GroupName { get; set; }

    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} باشد!")]
    [Display(Name = "تصویر گروه ")]
    public string? GroupImage { get; set; }

    #region Relations

    public virtual List<Report>? Reports { get; set; }

    #endregion
}