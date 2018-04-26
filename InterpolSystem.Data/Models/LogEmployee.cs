namespace InterpolSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class LogEmployee
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(LoggerValuesMaxLength)]
        public string  IpAddress { get; set; }

        [Required]
        [MaxLength(LoggerValuesMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(LoggerValuesMaxLength)]
        public string ControllerName { get; set; }

        [Required]
        [MaxLength(LoggerValuesMaxLength)]
        public string ActionName { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
