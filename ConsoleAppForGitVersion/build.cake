
#tool nuget:?package=GitVersion.CommandLine&version=4.0.0

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var packageVersion = "0.1.0";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Version")
	.Does(() =>
{
	var version = GitVersion();

	var semVer = version.SemVer;
	Information($"SemVer = {version.SemVer}");
	
	var nuGetVersion = version.NuGetVersion;
	Information($"NuGetVersion = {nuGetVersion}");
	
	var informationalVersion = version.InformationalVersion;
	Information($"Corresponding package version {informationalVersion.Substring(0, 25)}");

	//if (!BuildSystem.IsLocalBuild)
	{
		GitVersion(new GitVersionSettings
		{
			UpdateAssemblyInfo = true
		});
	}
});

Task("Default")
.Does(() => {
	Information("Hello Cake!");
});

RunTarget(target);