$apiKey = "AzurePipelines"
$nugetSource = "AzurePipelinesInterpolSystemArtifacts"

nuget push ".\bin\Release\*.nupkg" -Source $nugetSource -ApiKey $apiKey