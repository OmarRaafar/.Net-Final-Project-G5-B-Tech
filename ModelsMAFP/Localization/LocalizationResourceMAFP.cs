using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsMAFP.Localization
{
    public class LocalizationResourceMAFP
    {
        [Key]
        public string Key { get; set; }
        public string ArabicValue { get; set; }
        public string EnglishValue { get; set; }
    }
}
