# conphig

Conphig.Help is a .NET 5 package for generating end-user help for correctly applying configuration based on Conphig.

## Getting started

First, see the [documentation for the Conphig package](https://atornblad.se/conphig) and create a configuration class with all the attributes you need. Add [Description](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.descriptionattribute?view=net-5.0) attributes to your properties to provide better documentation.

Then install the `Conphig.Help` package, and think about how you want to help your end-users use your system.

## Showing help on the command line

To show help on the command line, call [`ConfigHelp.WriteToConsole<T>`]. A nice thing to do is to add a `bool ShowHelp` property to your configuration class with the `-h` and `--help` command line switches.

``` csharp
// Configuration class
public class Settings
{
    [CommandLine("-h", "--help")]
    [Description("Shows this information")]
    public bool ShowHelp { get; set; }

    [CommandLine("-s", "--source")]
    [Description("The directory where your files are")]
    public string Source

    [CommandLine("-t", "--target")]
    [Description("The output filename")]
}

// The main method
public static void Main(string[] args)
{
    var settings = Config.Load<Settings>();
    if (settings.ShowHelp)
    {
        ConfigHelp.WriteToConsole<Settings>();
    }
}
```

If the user starts your program with the `-h` switch, this is what the output looks like:

``` bash
USAGE: program.exe [--help] [--source <directory>] [--target <filename>]

  --help                Shows this information
  
  --source <directory>  The directory where your files are

  --target <filename>   The output filename
```

## Read more

For more information, visit the [Project Website](https://atornblad.se/conphig)
