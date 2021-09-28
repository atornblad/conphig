using System;
namespace ATornblad.Conphig
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilenameAttribute : Attribute
    {
        public string Filename { get; private set; }

        public FilenameAttribute(string filename)
        {
            Filename = filename;
        }
    }
}
