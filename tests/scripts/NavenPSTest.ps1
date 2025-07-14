$file=[System.String]::Empty
$stage=[System.String]::Empty
$arguments=[System.String]::Empty

$last=$args.Length - 1

for ($i=0; $i -lt $last; $i++)
{
    if (($args[$i] -eq "-f") -or ($args[$i] -eq "--file"))
    {
        $file = $args[++$i]
    }
    else
    {
        $arguments += $args[$i] + " "
    }
}

$stage=$args[$last]

Write-Host $file
Write-Host $stage

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
