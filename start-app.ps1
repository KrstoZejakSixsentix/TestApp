function Start-Console-Process{
    param (
        $name
    )
    $psi = New-Object System.Diagnostics.ProcessStartInfo;
    $psi.FileName = "C:\ServerBytes\TestApp\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe"; #process file
    $psi.UseShellExecute = $false; #start the process from it's own executable file
    $psi.RedirectStandardInput = $true; #enable the process to read from standard input
    $p = [System.Diagnostics.Process]::Start($psi);
    Start-Sleep -s 5 #wait 2 seconds so that the process can be up and running
    $p.StandardInput.WriteLine($name); #StandardInput property of the Process is a .NET StreamWriter object
}
For ($i=0;$i -lt 10;$i++){
    $username = "my-username-"+$i.ToString();
    Start-Console-Process -name $username
}