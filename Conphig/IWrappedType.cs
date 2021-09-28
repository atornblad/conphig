using System;
namespace ATornblad.Conphig
{
    public interface IWrappedType
    {
        bool WasKeyLoadedFromFile(string key);
    }
}
