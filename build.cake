#tool "nuget:?package=xunit.runner.console&version=2.4.1"
#addin Cake.Curl
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var build = Argument("build", "1.0.0");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var isAppVeyorBuild = AppVeyor.IsRunningOnAppVeyor;

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
	.IsDependentOn("Clean")
	.Does(() =>
{
	NuGetRestore("./src/Example.sln");
});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
{
	if(IsRunningOnWindows())
	{
	  // Use MSBuild
	  MSBuild("./src/Example.sln", new MSBuildSettings().SetConfiguration(configuration));
	}
	else
	{
	  // Use XBuild
	  XBuild("./src/Example.sln", settings =>
		settings.SetConfiguration(configuration));
	}
});

Task("Connect-ReportPortal")
	.IsDependentOn("Build")
	.Does(() =>
{
	CurlDownloadFile(new Uri("https://github.com/reportportal/agent-net-xunit/releases/download/1.1.0/reportportal-1.1.0-net452.zip"), new CurlDownloadSettings{FollowRedirects = true});

	DirectoryPath exePath = Context.Tools.Resolve("xunit.console.exe").GetDirectory();

	DeleteFiles(exePath + "/ReportPortal*");
	Unzip("reportportal-1.1.0-net452.zip", exePath);

	Console.WriteLine(exePath);
});

Task("Run-Unit-Tests")
	.IsDependentOn("Connect-ReportPortal")
	.ContinueOnError()
	.Does(() =>
{
	XUnit2("./src/Example/bin/" + configuration + "/net452/Example.dll", new XUnit2Settings {ArgumentCustomization = args=>args.Append("-reportportal")});
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
