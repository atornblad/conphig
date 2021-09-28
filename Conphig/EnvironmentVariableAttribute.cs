using System;
namespace ATornblad.Conphig
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnvironmentVariableAttribute : Attribute
    {
        public string VariableName { get; set; }

        public EnvironmentVariableAttribute(string variableName)
        {
            VariableName = variableName;
        }
    }
}
