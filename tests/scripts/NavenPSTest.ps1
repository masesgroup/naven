$file=$args[0]
$stage=$args[1]

$PSVersionTable.PSVersion
try
{
   Start-Launcher -File $file -Stage $stage
}
catch {
    Write-Host "An error occurred:"
    Write-Host $_.ScriptStackTrace
    Write-Host $_.Exception.ToString()
}
