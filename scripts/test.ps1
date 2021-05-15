param([Parameter(Mandatory=$true)]
      [string] $FirstName,
      [Parameter(Mandatory=$true)]
      [string] $LastName,
      [Parameter(Mandatory=$true)]
      [string] $LogonName,
      [Parameter(Mandatory=$true)]
      [string] $DisplayName,
      [Parameter(Mandatory=$true)]
      [string] $EMail,
      [string] $PasswordResetNR,
      [string] $TelephoneNR,
      [string] $Description,
      [string] $Company,
      [string] $Title,
      [string] $Kostenstelle,
      [string] $Department,
      [string] $Office,
      [Parameter(Mandatory=$true)]
      [string] $Manager,
      [Parameter(Mandatory=$true)]
      [string] $Container,
      [string] $Street,
      [string] $City,
      [string] $ZIP,
      [string] $WebPage,
      [string] $Gruppenmitgliedschaft,
      [DateTime] $ExpirationDate,
      [string] $License,
      [Parameter(Mandatory=$true)]
      [string] $ExchangeServer,
      [Parameter(Mandatory=$true)]
      [string] $Password
)

Write-Output "Start LOCAL Test Script"
Write-Output $FirstName
Write-Output $LastName
Write-Output $LogonName
Write-Output $DisplayName
if ($null -ne $ExpirationDate) {
    Write-Output $ExpirationDate.ToString("dd. MM. yyyy")
}
throw "alsdkfj"