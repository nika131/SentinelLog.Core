using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecLog.Models
{
    [Table("SecurityEvents")]
    public class SecurityEvent
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string SourceSystem { get; set; }

        [Required]
        [StringLength(50)]
        public string EventType { get; set; }

        [Required]
        public SeverityLevel Severity { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [StringLength(45)]
        public string IpAddress { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public enum SeverityLevel
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Critical = 3
    }
}
