# actualizar-changelog.ps1

# Fuerza a PowerShell a usar UTF-8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$changelogPath = "CHANGELOG.md"
$hoy = Get-Date -Format "yyyy-MM-dd"
$commits = git log --pretty=format:"- %s" -n 20

$nuevaSeccion = @"
## [$hoy]

$commits

---
"@

# Si ya existe, guardamos el contenido viejo
$contenidoExistente = ""
if (Test-Path $changelogPath) {
    $contenidoExistente = Get-Content $changelogPath -Raw
}

# Unimos el nuevo contenido arriba
$contenidoCompleto = $nuevaSeccion + "`r`n" + $contenidoExistente

# Forzamos codificación UTF-8 con BOM usando .NET
$utf8WithBom = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($changelogPath, $contenidoCompleto, $utf8WithBom)

Write-Host "`n✅ CHANGELOG.md actualizado con codificación UTF-8 con BOM.`n"
Read-Host -Prompt "Presioná ENTER para cerrar"
