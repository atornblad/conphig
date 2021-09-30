# conphig

Conphig is a .NET 5 package for loading configuration from JSON files, command line and environment variables.

## Getting started

After installing the `Conphig` package, create a simple POCO class and place your configuration items as properties inside.

For each property, use the [`JsonPropertyName`](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute?view=net-5.0), [`CommandLine`](https://atornblad.se/conphig#commandline-attribute), [`EnvironmentVariable`](https://atornblad.se/conphig#environmentvariable-attribute), and [`Default`](https://atornblad.se/conphig#default-attribute) attributes to control how that property is deserialized from JSON files, command line parameters and environment variables, and what the default value is.

Then, in your `Main` method *(or somewhere else that makes sense given your app startup code)*, add a call to [`Config.Load<T>`](https://atornblad.se/conphig#config-load-t) to create and populate an object containing your configuration items.

## Example configuration class

```
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace MyApp
{
    [Filename("app-configuration.json")]
    public class Settings
    {
        [Default("Untitled")]
        [JsonPropertyName("title")]
        [EnvironmentVariable("TITLE")]
        [CommandLine("-t", "--title")]
        public string Title { get; set; }

        [JsonPropertyName("Categories")]
        [CommandLine("-c", "--category")]
        public string[] Categories { get; set; }

        [EnvironmentVariable("API_KEY")]
        public string ApiKey { get; set; }
    }
}
```

## Read more

For more information, visit the [Project Website](https://atornblad.se/conphig)
