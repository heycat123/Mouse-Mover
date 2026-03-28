using System.Drawing;
using System.Runtime.InteropServices;
using System;

public class MouseMoverWin32
{
    // Mouse Event Constants
    public const int MOUSEEVENTF_LEFTDOWN = 0x02;
    public const int MOUSEEVENTF_LEFTUP = 0x04;
    
    // Virtual Key Code for Right Mouse Button
    public const int VK_RBUTTON = 0x02;

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);

    // Import to check button state
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    public static void SendMouseLeftClick(int x, int y)
    {
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        System.Threading.Thread.Sleep(50); 
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        Console.WriteLine($"Left Clicked at ({x}, {y})");
    }

    public static void MoveMouseInCircle(int x, int y, int radius)
    {
        var completeCircle = 360;
        var pointsOnCircle = 100;
        var angleIncrement = completeCircle / pointsOnCircle;
        int cx = x - radius;
        int cy = y;
        for (int angleDeg = 0; angleDeg < completeCircle; angleDeg += angleIncrement)
        {
            double angleRad = angleDeg * Math.PI / 180.0;
            int x1 = cx + (int)Math.Round(radius * Math.Cos(angleRad));
            int y1 = cy + (int)Math.Round(radius * Math.Sin(angleRad));
            SetCursorPos(x1, y1);
            Thread.Sleep(10);
 
        }   
        

    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Loop started. HOLD the RIGHT MOUSE BUTTON to stop.");

        // Loop runs as long as the high-order bit of GetAsyncKeyState is NOT set
        while ((GetAsyncKeyState(VK_RBUTTON) & 0x8000) == 0)
        {
            /*// Perform your click
            SendMouseLeftClick(1100, 657);*/
            MoveMouseInCircle(1400, 657, 30);
            // Small delay so it doesn't click 1000 times a second
            // and allows you time to press the stop button
            System.Threading.Thread.Sleep(50); 

            Console.WriteLine("Still looping... (Press Right Click to Exit)");
        }
        
        Console.WriteLine("Right click detected! Exiting...");
    }
}