param(
      [string] $FirstName,
      [string] $LastName,
      [string] $LogonName,
      [string] $DisplayName,
      [string] $EMail,
      [string] $MobileNR,
      [string] $TelephoneNR,
      [string] $Description,
      [string] $Company,
      [string] $Title,
      [string] $Kostenstelle,
      [string] $Department,
      [string] $Office,
      [string] $Manager,
      [string] $Container,
      [string] $Street,
      [string] $City,
      [string] $ZIP,
      [string] $WebPage,
      [string[]] $Gruppenmitgliedschaft,
      [DateTime] $ExpirationDate,
      [string] $License,
      [string] $Accounttype,
      [string] $Password
)

Write-Output "Start LOCAL Test Script"
Write-Output $FirstName
Write-Output $LastName
Write-Output $LogonName
Write-Output $DisplayName

if ($null -ne $Gruppenmitgliedschaft) {
    foreach($ele in $Gruppenmitgliedschaft) {
        Write-Output $ele
    }
}

if ($null -ne $ExpirationDate) {
    Write-Output $ExpirationDate.ToString("dd. MM. yyyy")
}
throw "Custom Script Error Message"
