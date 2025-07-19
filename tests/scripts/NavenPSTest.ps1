$file=[System.String]::Empty
$stage=[System.String]::Empty
$arguments=[System.String]::Empty
$verbose=$false

$last=$args.Length - 1

for ($i=0; $i -lt $last; $i++)
{
    if (($args[$i] -eq "-f") -or ($args[$i] -eq "--file"))
    {
        $file = $args[++$i]
    }
    elseif ($args[$i] -eq "-Verbose")
    {
        $verbose=$true
    }
    else
    {
        $arguments += $args[$i] + " "
    }
}

$stage=$args[$last]

if ($verbose)
{
    $PSVersionTable.PSVersion
    Write-Host "File:" $file
    Write-Host "Stage:" $stage
}

try
{
   Start-Launcher -File $file -Stage $stage -Arguments $arguments
}
catch {
    if ($verbose)
    {
        Write-Host "An error occurred:"
        Write-Host $_.ScriptStackTrace
        Write-Host $_.Exception.ToString()
    }
}
