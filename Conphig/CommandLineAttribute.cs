using System;
namespace ATornblad.Conphig
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandLineAttribute : Attribute
    {
        public string[] SwitchNames { get; set; }

        public CommandLineAttribute(params string[] switchNames)
        {
            SwitchNames = switchNames;
        }
    }
}
