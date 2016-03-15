# AzureAD-Reporting-Api build script
# V 1.0

# Get currect location
$invocation = (Get-Variable MyInvocation).Value;
if($Invocation.MyCommand.Path)
{
    $baseDir = Split-Path $Invocation.MyCommand.Path;
}
else
{
    $baseDir = $Invocation.InvocationName.Substring(0,$Invocation.InvocationName.LastIndexOf("\"));
}

$outputFolder  = "";
$releaseFolder = "";
$outputFolder  = $baseDir + "\output";
$releaseFolder = $baseDir + "\src\AzureADReportingApi\bin\Release";
$binDirMain    = $baseDir + "\src\AzureADReportingApi\bin";
$objDirMain    = $baseDir + "\src\AzureADReportingApi\obj";
$solution      = $baseDir + "\src\AzureADReportingApi.sln"
$msbuild       = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe";

# Remove old build
If (Test-Path $outputFolder){
	Remove-Item $outputFolder -Force -Recurse
}

$results = & $msbuild  $solution /target:Clean /target:Build /noconsolelogger /p:Configuration=Release /p:Platform='Any CPU' /fl /flp:logfile=Build.log
$results;

# Copy to output folder
If (Test-Path $releaseFolder)
{
    [System.IO.Directory]::Move($releaseFolder, $outputFolder)	
}

# Cleanup
If (Test-Path $binDirMain){
	Remove-Item $binDirMain -Force -Recurse
}
If (Test-Path $objDirMain){
	Remove-Item $objDirMain -Force -Recurse
}

Get-ChildItem -Path $outputFolder -Include *.xml -File -Recurse | foreach { $_.Delete()};
Get-ChildItem -Path $outputFolder -Include *.pdb -File -Recurse | foreach { $_.Delete()};
