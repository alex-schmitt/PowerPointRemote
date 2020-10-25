using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PowerPointRemote.WebApi.Extensions
{
    public static class ModalStateExtensions
    {
        public static IDictionary<string, string[]> ConvertErrorsToDictionary(this ModelStateDictionary modelState)
        {
            var dictionary = new Dictionary<string, string[]>();
            foreach (var (key, value) in modelState)
            {
                if (value.Errors == null || value.Errors.Count <= 0) continue;
                dictionary.Add(key.ToCamelCase(), value.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            return dictionary;
        }
    }
}