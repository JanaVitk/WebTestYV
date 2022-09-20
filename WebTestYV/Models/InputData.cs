using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebTestYV.Models
{
    public class InputData
    {
        public enum PeriodType {None, Year, Month, ThreeMonths};

        [Required(ErrorMessage = "Укажите AccountId")]
        [RegularExpression(@"[1-9]\d*", ErrorMessage = "Не верный формат. Укажите AccountId")]
        public long AccountId { set; get; }

        
        [Range(1,3, ErrorMessage = "Укажите Period")]
        public PeriodType Period { set; get; }

        [FromHeader]
        public string Accept { set; get; }

    }
}
