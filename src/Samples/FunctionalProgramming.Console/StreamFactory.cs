using System;
using System.IO;
using System.Text;

namespace FunctionalProgramming.Console
{
    public static class StreamFactory
    {
        public static Stream GetStream()
        {
            var doctors =
                String.Join(
                    Environment.NewLine,
                    new[] {
                        "Hartnell", "Troughton", "Pertwee", "T. Baker",
                        "Davison", "C. Baker", "McCoy", "McGann", "Hurt",
                        "Eccleston", "Tennant", "Smith", "Capaldi" });

            var buffer = Encoding.UTF8.GetBytes(doctors);

            var stream = new MemoryStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Position = 0L;

            return stream;
        }
    }
}
