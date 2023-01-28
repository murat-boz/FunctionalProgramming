using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionalProgramming.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainProgram();
            MainProgramWithFunctional();
        }

        private static void MainProgramWithFunctional()
        {
            var selectBox =
                Disposible
                    .Using(
                        StreamFactory.GetStream,
                        stream => 
                            new byte[stream.Length]
                            .Tee(b => stream.Read(b, 0, (int)stream.Length)))
                    .Map(value => Encoding.UTF8.GetString(value))
                    .Split(new[] { Environment.NewLine, }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((s, ix) => Tuple.Create(ix, s))
                    .ToDictionary(k => k.Item1, v => v.Item2)
                    .Map(options => BuildSelectBoxWithFunctional(options, "theDoctors", true))
                    .Tee(System.Console.WriteLine);

            System.Console.ReadLine();
        }

        private static void MainProgram()
        {
            byte[] buffer;

            using (var stream = StreamFactory.GetStream())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }

            var options =
                Encoding
                    .UTF8
                    .GetString(buffer)
                    .Split(new[] { Environment.NewLine, }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((s, ix) => Tuple.Create(ix, s))
                    .ToDictionary(k => k.Item1, v => v.Item2);

            var selectBox = BuildSelectBox(options, "theDoctors", true);

            System.Console.WriteLine(selectBox);

            System.Console.ReadLine();
        }

        private static string BuildSelectBoxWithFunctional(IDictionary<int, string> options, string id, bool includeUnknown) =>
            new StringBuilder()
                    .AppendFormattedLine("<select id=\"{0}\" name=\"{0}\">", id)
                    .AppendLineWhen(
                        () => includeUnknown,
                        sb => sb.AppendLine("\t<option>Unknown</option>"))
                    .AppendSequence(
                        options,
                        (sb, opt) => sb.AppendFormattedLine("\t<option value=\"{0}\">{1}</option>", opt.Key, opt.Value))
                    .AppendLine("</select>")
                    .ToString();

        private static string BuildSelectBox(IDictionary<int, string> options, string id, bool includeUnknown)
        {
            var html = new StringBuilder();

            html.AppendFormat("<select id=\"{0}\" name=\"{0}\">", id);
            html.AppendLine();

            if (includeUnknown)
            {
                html.AppendLine("\t<option>Unknown</option>");
            }

            foreach (var option in options)
            {
                string value = String.Empty;

                options.TryGetValue(option.Key, out value);

                html.AppendFormat("\t<option value=\"{0}\">{1}</option>", option.Key, value);
                html.AppendLine();
            }

            html.AppendLine("</select>");

            return html.ToString();
        }
    }
}
