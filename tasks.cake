#tool "nuget:?package=NUnit.ConsoleRunner"
#r "xbuild"

Task("Restore").Does(() => {
    NuGetRestore(solution);
});

Task("Build").Does(() => {
    XBuild(solution, config => {
        config.SetConfiguration(configuration);
    });
});

Task("Test").Does(() => {
    var exclude = excludeAcceptanceTests ? "/exclude:Acceptance" : "";
    NUnit3(tests, new NUnit3Settings ());
});
