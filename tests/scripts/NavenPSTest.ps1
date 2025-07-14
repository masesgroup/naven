$file=[System::String]::Empty
$stage=[System::String]::Empty
$arguments=[System::String]::Empty

for ([Int32](i=0); i < $args.Length - 1; i++)
{
    if ($args[i] == "-f" || $args[i] == "--file")
    {
        $file = $args[++i];
    }
    else
    {
        $arguments += $args[i] + " ";
    }
}

$stage=$args[$args.Length - 1];

$PSVersionTable.PSVersion
try
{
   Start-Launcher -File $file -Stage $stage -Arguments $arguments
}
catch {
    Write-Host "An error occurred:"
    Write-Host $_.ScriptStackTrace
    Write-Host $_.Exception.ToString()
}
