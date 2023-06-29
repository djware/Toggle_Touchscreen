# Toggle_Touchscreen
Program to toggle the touchscreen on your Windows devices. 

Touchscreen.exe needs to be ran as an administrator which you can setup in the properties of the file. If you want you can download just the Touchscreen.ps1 file and run that as an administrator to do the same. 
![image](https://github.com/djware/Toggle_Touchscreen/assets/85318457/e0db6926-6aab-40c1-b3cf-f1c09912109e)

```
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
```
Please note this disables 'HID-compliant touch screen' driver and doing so will cause it to not work till you enable it again. Make sure to have an alternative input control before disabling your touch screen. Use at your own risk. 
