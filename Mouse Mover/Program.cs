using System.Runtime.InteropServices;
using System;
using System.Threading;

/// <summary>
/// Provides functionality to simulate mouse movements and clicks using Win32 API calls.
/// This tool is designed to move the mouse cursor in patterns or perform automated clicks.
/// </summary>
public class MouseMoverWin32
{
    /// <summary>
    /// Event constant for the left mouse button being pressed down.
    /// </summary>
    public const int MOUSEEVENTF_LEFTDOWN = 0x02;

    /// <summary>
    /// Event constant for the left mouse button being released (up).
    /// </summary>
    public const int MOUSEEVENTF_LEFTUP = 0x04;
    
    /// <summary>
    /// Virtual Key Code for the Right Mouse Button (VK_RBUTTON).
    /// Used for monitoring keyboard/mouse button states.
    /// </summary>
    public const int VK_RBUTTON = 0x02;

    /// <summary>
    /// Sets the cursor's position to the specified screen coordinates.
    /// </summary>
    /// <param name="x">The new x-coordinate of the cursor, in screen coordinates.</param>
    /// <param name="y">The new y-coordinate of the cursor, in screen coordinates.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// Synthesizes mouse motion and button clicks.
    /// </summary>
    /// <param name="dwFlags">Flags that specify various aspects of mouse motion and button clicking.</param>
    /// <param name="dx">The mouse's absolute position along the x-axis or its amount of motion since the last mouse event was generated.</param>
    /// <param name="dy">The mouse's absolute position along the y-axis or its amount of motion since the last mouse event was generated.</param>
    /// <param name="cButtons">The number of buttons to simulate.</param>
    /// <param name="dwExtraInfo">An additional value associated with the mouse event.</param>
    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);

    /// <summary>
    /// Determines whether a key is up or down at the time the function is called, 
    /// and whether the key was pressed after a previous call to GetAsyncKeyState.
    /// </summary>
    /// <param name="vKey">The virtual-key code.</param>
    /// <returns>The return value specifies whether the key was pressed since the last call to GetAsyncKeyState, 
    /// and whether the key is currently up or down.</returns>
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    /// <summary>
    /// Moves the mouse to the specified coordinates and performs a left-click.
    /// </summary>
    /// <param name="x">The x-coordinate of the target position.</param>
    /// <param name="y">The y-coordinate of the target position.</param>
    public static void SendMouseLeftClick(int x, int y)
    {
        // Move the cursor to the target coordinates
        SetCursorPos(x, y);

        // Simulate a left button down event
        mouse_event((uint)MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);

        // Pause briefly to ensure the application registers the click
        Thread.Sleep(50); 

        // Simulate a left button up event
        mouse_event((uint)MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);

        Console.WriteLine($"Left Clicked at ({x}, {y})");
    }

    /// <summary>
    /// Moves the mouse cursor in a circular pattern around a specific point.
    /// </summary>
    /// <param name="x">The x-coordinate of the center of the circle.</param>
    /// <param name="y">The y-coordinate of the center of the circle.</param>
    /// <param name="radius">The radius of the circle in pixels.</param>
    public static void MoveMouseInCircle(int x, int y, int radius)
    {
        const int completeCircle = 360;
        const int pointsOnCircle = 100;
        const int angleIncrement = completeCircle / pointsOnCircle;

        // Calculate the circle center relative to the start point (optional adjustment logic)
        // Note: The logic below creates a circle centered at (x - radius, y)
        int cx = x - radius;
        int cy = y;

        for (int angleDeg = 0; angleDeg < completeCircle; angleDeg += angleIncrement)
        {
            // Convert degrees to radians for trigonometric functions
            double angleRad = angleDeg * Math.PI / 180.0;

            // Calculate new coordinates on the circumference
            int x1 = cx + (int)Math.Round(radius * Math.Cos(angleRad));
            int y1 = cy + (int)Math.Round(radius * Math.Sin(angleRad));

            // Move the cursor to the calculated point
            SetCursorPos(x1, y1);

            // Small delay to make the movement look smooth
            Thread.Sleep(10);
        }   
    }

    /// <summary>
    /// The main entry point of the application.
    /// Starts a loop that moves the mouse in a circle until the right mouse button is held down.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        Console.WriteLine("Mouse Mover Started.");
        Console.WriteLine("--------------------");
        Console.WriteLine("HOLD the RIGHT MOUSE BUTTON to stop the program.");

        // Loop runs as long as the high-order bit of GetAsyncKeyState(VK_RBUTTON) is NOT set (key is UP)
        // 0x8000 is the bitmask for the "currently down" state
        while ((GetAsyncKeyState(VK_RBUTTON) & 0x8000) == 0)
        {
            // Move the mouse in a circular motion at specified coordinates
            // Default center: 1400, 657 with a radius of 30 pixels
            MoveMouseInCircle(1400, 657, 30);

            // Brief delay to manage CPU usage and provide a window for user input (right-click)
            Thread.Sleep(50); 

            Console.WriteLine("Looping... (Hold Right Click to Exit)");
        }
        
        Console.WriteLine("\nRight click detected! Exiting program...");
    }
}