using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Tests
{
    internal class Extensions
    {
    }

    public static class StringAssertExtensions
    {
        public static void ShouldContain(this string actual, string expected)
        {
            if (!actual.Contains(expected))
                throw new AssertFailedException(
                    $"""
                    Expected string to contain: "{expected}"
                    String was "{actual}"
                    """);
        }
    }
}
