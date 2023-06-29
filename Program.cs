using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // Create a PowerShell process
        Process powerShellProcess = new Process();
        powerShellProcess.StartInfo.FileName = "powershell.exe";
        powerShellProcess.StartInfo.Verb = "runas"; // Run as administrator
        powerShellProcess.StartInfo.RedirectStandardOutput = true;
        powerShellProcess.StartInfo.RedirectStandardError = true;
        powerShellProcess.StartInfo.UseShellExecute = false;
        powerShellProcess.StartInfo.CreateNoWindow = true;

        // Set the PowerShell script
        string powerShellScript = @"
            # Get the touchscreen device
            $touchscreenDevice = Get-PnpDevice | Where-Object { $_.FriendlyName -like '*touch screen*' }
        
            # Check if the touchscreen is currently enabled or disabled
            $touchscreenEnabled = $touchscreenDevice.Status -eq 'OK'
        
            # Toggle touchscreen functionality
            if ($touchscreenEnabled) {
                # Disable touchscreen
                $touchscreenDevice | Disable-PnpDevice -Confirm:$false
                Write-Host 'Touchscreen has been disabled.'
            } else {
                # Enable touchscreen
                $touchscreenDevice | Enable-PnpDevice -Confirm:$false
                Write-Host 'Touchscreen has been enabled.'
            }";

        // Set the PowerShell script as the command to execute
        powerShellProcess.StartInfo.Arguments = $"-Command \"{powerShellScript}\"";

        try
        {
            // Start the PowerShell process
            powerShellProcess.Start();

            // Read the output and error streams
            string output = powerShellProcess.StandardOutput.ReadToEnd();
            string error = powerShellProcess.StandardError.ReadToEnd();

            // Wait for the process to exit
            powerShellProcess.WaitForExit();

            // Check if there was any output or error
            if (!string.IsNullOrEmpty(output))
                Console.WriteLine("Output: " + output);

            if (!string.IsNullOrEmpty(error))
                Console.WriteLine("Error: " + error);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing PowerShell script: " + ex.Message);
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}