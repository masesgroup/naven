$file=$args[0]
$stage=$args[1]

$PSVersionTable.PSVersion
Start-Launcher -File $file -Stage $stage
$e = $_.Exception
$msg = $e.Message
while ($e.InnerException) {
  $e = $e.InnerException
  $msg += "`n" + $e.Message
}
$msg
