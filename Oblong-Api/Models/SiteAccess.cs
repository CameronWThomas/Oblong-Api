using System.ComponentModel.DataAnnotations.Schema;

namespace Oblong_Api.Models
{
    [Table("site_access")]
    public class SiteAccess
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? IP { get; set; }
        public string? Token { get; set; }
        public DateTime? TimeEntered { get; set; }
        public DateTime? TimeExited { get; set; }
    }
}
