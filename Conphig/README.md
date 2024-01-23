# conphig

Conphig is a .NET 6 package for loading configuration from JSON files, command line and environment variables.

## Getting started

After installing the `Conphig` package, create a simple POCO class and place your configuration items as properties inside.

For each property, use the [`JsonPropertyName`](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute?view=net-6.0), [`CommandLine`](https://atornblad.se/conphig#commandline-attribute), and [`EnvironmentVariable`](https://atornblad.se/conphig#environmentvariable-attribute) attributes to control how that property is deserialized from JSON files, command line parameters, and environment variables.

Then, in your `Main` method *(or somewhere else that makes sense given your app startup code)*, add a call to [`Config.Load<T>`](https://atornblad.se/conphig#config-load-t) to create and populate an object containing your configuration items.

## Example configuration class

``` csharp
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace MyApp
{
    [Filename("app-configuration.json")]
    public class Settings
    {
        [JsonPropertyName("title")]
        [EnvironmentVariable("TITLE")]
        [CommandLine("-t", "--title")]
        public string Title { get; set; } = "Untitled";

        [JsonPropertyName("Categories")]
        [CommandLine("-c", "--category")]
        public string[] Categories { get; set; }

        [EnvironmentVariable("API_KEY")]
        public string ApiKey { get; set; }

        [CommandLine("-v", "--verbose")]
        public bool VerboseOutput { get; set; }

        [JsonPropertyName("permissions")]
        [CommandLine("-p", "--permissions")]
        public PermissionLevels Permissions { get; set; } = PermissionLevels.None;
    }

    [Flags]
    public enum PermissionLevels
    {
        None = 0,
        Create = 1,
        Read = 2,
        Edit = 4,
        Delete = 8
    }
}
```

## Example of loading

``` csharp
using System;
using ATornblad.Conphig;
namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var settings = Config.Load<Settings>();
            if (settings.VerboseOutput)
            {
                Console.WriteLine($"Title: {settings.Title}");
            }
        }
    }
}
```

## Example of configuration file

``` json
{
    "title": "Title from JSON file",
    "categories": [
        "API",
        "Programming",
        "C#"
    ],
    "permissions": "Create,Read,Edit"
}
```

## Example of command line arguments

``` bash
myapp --verbose -t "Title from Command Line" -c API -c Programming -c bash -p Read,Edit
```

## Read more

For more information, visit the [Project Website](https://atornblad.se/conphig)
