using System.Collections.Generic;

namespace NuGen.Options.Start
{
    public static class StartOptionsExtensions
    {
        public static ValidationResult Validate(this StartOptions options)
        {
            var messages = new List<string>();
            if (options.From != null && options.To != null && options.To > options.From)
            {
                return new ValidationResult(true);
            }
            messages.Add("Ошибка во входных параметрах:");
            if (options.From == null)
            {
                messages.Add("Нужно указать начальный номер --from или -f");
            }

            if (options.To == null)
            {
                messages.Add("Нужно указать конечный номер --to или -t");
            }

            if (options.To <= options.From)
            {
                messages.Add("Конечный номер должен быть больше начального");
            }

            return new ValidationResult(false, string.Join('\n',messages));
        }
    }
}