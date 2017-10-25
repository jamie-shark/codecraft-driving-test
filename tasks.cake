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

Task("Run-Unit-Tests").Does(() => {
    RunTestsWhere("cat != Acceptance");
});

Task("Run-Acceptance-Tests").Does(() => {
    RunTestsWhere("cat == Acceptance");
});

void RunTestsWhere(string where) {
    NUnit3(tests, new NUnit3Settings {
        Where = where
    });
}
