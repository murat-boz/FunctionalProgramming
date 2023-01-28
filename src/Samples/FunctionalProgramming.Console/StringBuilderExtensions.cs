using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionalProgramming.Console
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendFormattedLine(
            this StringBuilder @this,
            string format,
            params object[] args) =>
                @this.AppendFormat(format, args)
                     .AppendLine();

        public static StringBuilder AppendLineWhen(
            this StringBuilder @this,
            Func<bool> predicate,
            Func<StringBuilder, StringBuilder> func) =>
                predicate()
                    ? func(@this)
                    : @this;

        public static StringBuilder AppendSequence<T>(
            this StringBuilder @this,
            IEnumerable<T> sequence,
            Func<StringBuilder, T, StringBuilder> func) =>
                sequence.Aggregate(@this, func);
    }
}
