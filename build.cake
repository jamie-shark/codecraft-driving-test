#l "tasks.cake"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solution = "ProNet/ProNet.sln";
var tests = new[] {$"ProNet/ProNet.Test/bin/{configuration}/ProNet.Test.dll"};
var excludeAcceptanceTests = true;

Task("Default")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

RunTarget(target);
