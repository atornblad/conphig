using System;
using System.Text.Json.Serialization;
using ATornblad.Conphig;

namespace ConphigTests
{
    public class EnumConfiguration
    {
        [JsonPropertyName("logLevel")]
        [CommandLine("-l", "--log-level")]
        public LogLevel LogLevel { get; set; } = LogLevel.Default;

        [JsonPropertyName("fileRights")]
        [CommandLine("-r", "--file-rights")]
        public FileRights FileRights { get; set; } = FileRights.None;

        [JsonPropertyName("permissions")]
        [CommandLine("-p", "--permissions")]
        public Permissions[] Permissions { get; set; } = Array.Empty<Permissions>();
    }

    public enum LogLevel
    {
        Default,
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    [Flags]
    public enum FileRights
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }

    public enum Permissions
    {
        CreatePage,
        EditPage,
        DeletePage,
        CreateUser,
        EditUser,
        DeleteUser,
        CreateGroup,
        EditGroup,
        DeleteGroup
    }
}
