var target = Argument("Target", "Default");
var nugetApiKey = EnvironmentVariable("NUGET_API_KEY");

var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") is object ? EnvironmentVariable("Configuration") :
    "Release";

var artefactsDirectory = Directory("./Artefacts");

Task("Clean")
    .Description("Cleans the artefacts, bin and obj directories.")
    .Does(() =>
    {
        CleanDirectory(artefactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/obj"), new DeleteDirectorySettings() { Force = true, Recursive = true });
    });

Task("Restore")
    .Description("Restores NuGet packages.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("Build")
    .Description("Builds the solution.")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(
            ".",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                NoRestore = true,
            });
    });

Task("Test")
    .Description("Runs unit tests and outputs test results to the artefacts directory.")
    .DoesForEach(GetFiles("./Tests/**/*.csproj"), project =>
    {
        DotNetCoreTest(
            project.ToString(),
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                Logger = $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                NoBuild = true,
                NoRestore = true,
                ResultsDirectory = artefactsDirectory,
                ArgumentCustomization = x => x
                    .Append("--blame")
                    .AppendSwitch("--logger", $"html;LogFileName={project.GetFilenameWithoutExtension()}.html")
                    .Append("--collect:\"XPlat Code Coverage\"")
                    .Append("--settings runsettings.xml"),
            });
    });

Task("Pack")
    .Description("Creates NuGet packages and outputs them to the artefacts directory.")
    .Does(() =>
    {
        DotNetCorePack(
            "./Source/Witness/Witness.csproj",
            new DotNetCorePackSettings()
            {
                Configuration = configuration,
                IncludeSymbols = true,
                MSBuildSettings = new DotNetCoreMSBuildSettings().WithProperty("SymbolPackageFormat", "snupkg"),
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = artefactsDirectory,
            });
            
        DotNetCorePack(
            "./Source/Witness.DependencyInjection/Witness.DependencyInjection.csproj",
            new DotNetCorePackSettings()
            {
                Configuration = configuration,
                IncludeSymbols = true,
                MSBuildSettings = new DotNetCoreMSBuildSettings().WithProperty("SymbolPackageFormat", "snupkg"),
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = artefactsDirectory,
            });
    });

Task("Publish")
    .IsDependentOn("Pack")
    .DoesForEach(GetFiles($"{artefactsDirectory}/*.nupkg"), file =>
    {
        NuGetPush(file.ToString(), new NuGetPushSettings {
            Source = "https://api.nuget.org/v3/index.json",
            ApiKey = nugetApiKey,
        });
    });

Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then creates NuGet packages.")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(target);
