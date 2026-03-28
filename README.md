# Mouse Mover

A lightweight Windows utility written in C# that simulates mouse movements and clicks using the Win32 API. This program is designed to move the cursor in a circular pattern, which can be useful for keeping a session active or automating simple repetitive tasks.

## 🚀 Features

- **Circular Mouse Movement**: Moves the cursor smoothly in a circle.
- **Automated Clicks**: Includes functionality to perform left-clicks at specific coordinates (currently optional in code).
- **Graceful Termination**: The program monitors the right mouse button state. Simply **hold the Right Mouse Button** to exit the loop and close the application.
- **Low Resource Usage**: Uses native Win32 API calls for efficiency.

## 🛠️ Prerequisites

- **Operating System**: Windows (Required for Win32 API calls).
- **Runtime**: [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later.

## 💻 How to Run

1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/yourusername/MouseMover.git
    cd MouseMover
    ```

2.  **Build and Run**:
    You can run the project using the .NET CLI:
    ```bash
    dotnet run --project "Mouse Mover/Mouse Mover.csproj"
    ```
    Or open the solution (`Mouse Mover.sln`) in **JetBrains Rider** or **Visual Studio** and press `F5`.

## 📖 Usage

Once started, the program will begin moving your mouse cursor in a circle centered at `(1400, 657)` with a radius of `30` pixels.

- **To Stop**: Hold down the **Right Mouse Button** for a moment. The program detects the button state and will exit the loop safely.

## 🔍 Code Structure

- **`Program.cs`**: Contains the main logic and Win32 API imports (`user32.dll`).
  - `SetCursorPos`: Updates the mouse coordinates.
  - `mouse_event`: Simulates clicks.
  - `GetAsyncKeyState`: Monitors the exit condition (Right Click).
  - `MoveMouseInCircle`: Calculates trigonometric points for smooth circular motion.

## 📝 Documentation Style

This project follows professional C# documentation standards:
- **XML Documentation**: Every method and class includes `<summary>`, `<param>`, and `<returns>` tags.
- **Inline Comments**: Explains complex logic (e.g., bitwise operations for key states).
- **Clean Code**: Constants are used for magic numbers, and method names are descriptive.

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).
