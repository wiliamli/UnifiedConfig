using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Interfaces.Params
{
    public class SqlInjectAttribute: ValidationAttribute
    {
        private static readonly Regex RegSystemThreats =
        new Regex(@"\s?or\s*|\s?;\s?|\s?drop\s|\s?grant\s|^'|\s?--|\s?union\s|\s?delete\s|\s?truncate\s|" +
            @"\s?sysobjects\s?|\s?xp_.*?|\s?syslogins\s?|\s?sysremote\s?|\s?sysusers\s?|\s?sysxlogins\s?|\s?sysdatabases\s?|\s?aspnet_.*?|\s?exec\s?",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public override bool IsValid(object value)
        {
            string str = value as string;
            return RegSystemThreats.IsMatch(str);
        }
    }
}
