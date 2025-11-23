using ConsoleApp.Services;

namespace ConsoleApp.Utilities
{
    internal class ConsoleParametersParser
    {
        internal static void AskForParameters(IList<Parameter> parameters)
        {
            for (int i = 0, n = parameters.Count; i < n;)
            {
                if (AskForParameter(parameters[i], out var value))
                {
                    parameters[i] = parameters[i] with { Value = value };
                    i++;
                }
            }
        }

        private static bool AskForParameter(Parameter parameterDescription, out object value)
        {
            Console.WriteLine($"Input {parameterDescription.Name}:");
            var input = Console.ReadLine();
            try
            {
                value = ConvertParameter(input!, parameterDescription.Type);
                return true;
            }
            catch
            {
                Console.WriteLine($"Invalid {parameterDescription.Name}. Please enter a valid {parameterDescription.Type.Name}.");

                value = null!;
                return false;
            }
        }

        private static object ConvertParameter(string input, Type targetType) => Convert.ChangeType(input, targetType);
    }
}
