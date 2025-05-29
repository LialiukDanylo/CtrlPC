# CtrlPC

CtrlPC is a two-part application that allows you to remotely manage the power state of any PC within your local network from your smartphone. You can shut down, restart, or put a PC to sleep directly from the mobile app.

## Features

* ✨ Simple local network control of multiple PCs
* ⏳ One-tap commands: Sleep, Restart, Shutdown
* 📶 Automatic discovery of PCs in the network

## How It Works

* **Desktop App (Server):** Runs silently in the background on Windows PCs. It listens for incoming UDP commands and executes them.
* **Mobile App (Client):** Scans the local network, lists available PCs, and allows the user to send commands.

## Architecture Overview

```
+----------------+       UDP (Port 5151)      +------------------+
| CtrlPC Mobile  |  <---------------------->  | CtrlPC Desktop   |
| (.NET MAUI)    |                            | (.NET Framework) |
+----------------+                            +------------------+
```

## Getting Started

### Desktop App (Windows)

Option 1: **Run from source**

1. Open the solution in Visual Studio 2022.
2. Build the `CtrlPC.Desktop` project using .NET Framework 4.8.
3. Run the application. It will automatically register to run at Windows startup.

Option 2: **Use prebuilt executable**

1. Download the latest `CtrlPC.exe` from the [Releases](https://github.com/LialiukDanylo/CtrlPC/releases) section.
2. Run it once — the app will start listening in the background and auto-start with Windows.

### Mobile App (Android)

Option 1: **Run from source**

1. Open the solution in Visual Studio 2022 with .NET MAUI 8.0 installed.
2. Build and deploy the `CtrlPC.Mobile` project to your Android device.

Option 2: **Use prebuilt APK**

1. Download the latest APK from the [Releases](https://github.com/LialiukDanylo/CtrlPC/releases) section.
2. Install the APK on your Android phone.

## Mobile App Interface

The mobile app scans your local network every 30 seconds to update the list of available PCs. You can also press the refresh button to manually trigger a scan.

Once devices are listed, simply tap on any PC to select it, then use one of the available action buttons — Sleep, Shutdown, or Restart — to control it remotely.

## Command Protocol

* `SLEEP`: Puts the PC into sleep mode
* `SHUTDOWN`: Powers off the PC
* `RESTART`: Reboots the system
