var target = Argument("target", "Report");

#addin nuget:?package=Cake.Coverlet&version=2.5.1
#tool nuget:?package=ReportGenerator&version=4.0.4

/*  Projetos de teste a serem analisados: */
var testProjectsRelativePaths = new string[]
{
    "../DevIo.Testes/DevIo.Testes.csproj"
};

/* Assemblies que devem ser ignorados na cobertura: */
//var assembliesToIgnore = new List<string>()
//{
//    "[*Members.SharedProject*]*"
//};

//var classesToIgnore = new List<string>()
//{
//   "**/EntityInquiry.cs",
//   "**/EntityinquiryGrpc.cs"
//};

var parentDirectory = Directory("..");
var coverageDirectory = parentDirectory + Directory("code_coverage");
var coberturaFileName = "results";
var coberturaFileExtension = ".cobertura.xml";
var reportTypes = "Html";
var coverageFilePath = coverageDirectory + File(coberturaFileName + coberturaFileExtension);
var jsonFilePath = coverageDirectory + File(coberturaFileName + ".json");

Task("Clean")
    .Does(() =>
{
    if (!DirectoryExists(coverageDirectory))
        CreateDirectory(coverageDirectory);
    else
        CleanDirectory(coverageDirectory);
});

Task("Test")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var testSettings = new DotNetCoreTestSettings();

    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputDirectory = coverageDirectory,
        CoverletOutputName = coberturaFileName
        //Exclude = assembliesToIgnore
        //ExcludeByFile = classesToIgnore
    };

    if (testProjectsRelativePaths.Length == 1)
    {
        coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;
        DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);
    }
    else
    {
        DotNetCoreTest(testProjectsRelativePaths[0], testSettings, coverletSettings);

        coverletSettings.MergeWithFile = jsonFilePath;
        for (int i = 1; i < testProjectsRelativePaths.Length; i++)
        {
            if (i == testProjectsRelativePaths.Length - 1)
            {
                coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;
            }

            DotNetCoreTest(testProjectsRelativePaths[i], testSettings, coverletSettings);
        }
    }
});

Task("Report")
    .IsDependentOn("Test")
    .Does(() =>
{
    var reportSettings = new ReportGeneratorSettings
    {
        ArgumentCustomization = args => args.Append($"-reportTypes:{reportTypes}")
    };
    ReportGenerator(coverageFilePath, coverageDirectory, reportSettings);
});

RunTarget(target);

var htmlReportFile = System.IO.Path.GetFullPath(coverageDirectory.ToString() + "/index.htm");
if (IsRunningOnWindows())
{
    System.Diagnostics.Process.Start(htmlReportFile);
}