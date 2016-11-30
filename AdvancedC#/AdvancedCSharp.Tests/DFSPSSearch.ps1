function TraverseDirs
(
[string] $dir,
[string] $startWith,
[bool] $recursive
)

{
    $dirs = Get-ChildItem "$dir" -Directory | % {
                if ($_.Name.StartsWith($startWith)) {
                    $global:results += $_.FullName
                }

                #$global:results += $recursive

                if ($recursive -eq $True) {
                    TraverseDirs $_.FullName -startWith $startWith -recursive $True
                }
            }

    $files = Get-ChildItem "$dir" -File | % {
        if ($_.Name.StartsWith($startWith)) {
            $global:results += $_.FullName
        }
    }
}

$global:results = @();

$intCount = $args.count;
if($intCount -ne 3){
    Write-Host ('I want 3 params, u gave me ' + $intCount);
    exit;
}

$strPath = $args[0];
$strSearch = $args[1].ToLower();
$isRecursive = !!$args[2];

#$global:results += $isRecursive

if((Test-Path -path $strPath) -eq $False){
    echo ('Invalid path: ' + $strPath.ToString());
    exit;
}

TraverseDirs $strPath -startWith $strSearch -recursive $isRecursive

$global:results | % {
    Write-Host $_
}