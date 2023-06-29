# Get the touchscreen device
$touchscreenDevice = Get-PnpDevice | Where-Object { $_.FriendlyName -like '*touch screen*' }

# Check if the touchscreen is currently enabled or disabled
$touchscreenEnabled = $touchscreenDevice.Status -eq 'OK'

# Toggle touchscreen functionality
if ($touchscreenEnabled) {
    # Disable touchscreen
    $touchscreenDevice | Disable-PnpDevice -Confirm:$false
    Write-Host "Touchscreen has been disabled."
} else {
    # Enable touchscreen
    $touchscreenDevice | Enable-PnpDevice -Confirm:$false
    Write-Host "Touchscreen has been enabled."
}