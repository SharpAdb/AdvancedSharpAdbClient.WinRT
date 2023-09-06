| Issues | License | NuGet |
|--------|---------|-------|
[![Github Issues](https://img.shields.io/github/issues/yungd1plomat/AdvancedSharpAdbClient)](https://github.com/yungd1plomat/AdvancedSharpAdbClient/issues)|[![Github Issues](https://img.shields.io/github/license/yungd1plomat/AdvancedSharpAdbClient)](https://github.com/yungd1plomat/AdvancedSharpAdbClient/blob/main/LICENSE)|[![NuGet Status](https://img.shields.io/nuget/dt/AdvancedSharpAdbClient.WinRT.svg?style=flat)](https://www.nuget.org/packages/AdvancedSharpAdbClient.WinRT/)

# A WinRT client for adb, the Android Debug Bridge (AdvancedSharpAdbClient.WinRT)

AdvancedSharpAdbClient.WinRT is a WinRT library that allows WinRT applications to communicate with Android devices. 
It provides a WinRT implementation of the `adb` protocol, giving more flexibility to the developer than launching an 
`adb.exe` process and parsing the console output.

It's a WinRT package of [AdvancedSharpAdbClient](https://github.com/yungd1plomat/AdvancedSharpAdbClient), which is upgraded version of [SharpAdbClient](https://github.com/quamotion/madb).

## Support Platform
- UAP 10.0
- UAP 10.0.15138.0
- .NET 6.0 - Windows 10.0.17763.0

## Installation
To install AdvancedSharpAdbClient.WinRT install the [AdvancedSharpAdbClient.WinRT NuGetPackage](https://www.nuget.org/packages/AdvancedSharpAdbClient.WinRT).
If you're using Visual Studio, you can run the following command in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console):

```ps
PM> Install-Package AdvancedSharpAdbClient.WinRT
```

## Getting Started
AdvancedSharpAdbClient does not communicate directly with your Android devices, but uses the `adb.exe` server process as an intermediate. Before you can connect to your Android device, you must first start the `adb.exe` server.

You can do so by either running `adb.exe` yourself (it comes as a part of the ADK, the Android Development Kit), or you can use the `AdbServer.StartServer` method like this:

```cpp
if (!AdbServer::Instance().GetStatus().IsRunning())
{
    AdbServer server = AdbServer::AdbServer();
    StartServerResult result = server.StartServer(L"C:\\adb\\adb.exe", false);
    if (result != StartServerResult::Started)
    {
        printf("Can't start adb server");
    }
}
```

### Connecting to device
Before using all the methods, you must initialize the new AdbClient class and then connect to the device

If you want to automate 2 or more devices at the same time, you must remember: 1 device - 1 AdbClient class

You can look at the examples to understand more

```cpp
AdbClient client;

DeviceData device;

int main()
{
    client = AdbClient::AdbClient();
    client.Connect(L"127.0.0.1:62001");
    device = *client.GetDevices().First(); // Get first connected device
}
```

## Device automation

### Finding element
You can find the element on the screen by xpath

```cpp
AdbClient client;

DeviceData device;

int main()
{
    client = AdbClient::AdbClient();
    client.Connect(L"127.0.0.1:62001");
    device = *client.GetDevices().First();
    Element el = client.FindElement(device, L"//node[@text='Login']");
}
```

You can also specify the waiting time for the element

```cpp
Element el = client.FindElement(device, L"//node[@text='Login']", TimeSpan(5));
```

And you can find several elements

```cpp
IIterable<Element> els = client.FindElements(device, L"//node[@resource-id='Login']", TimeSpan(5));
```


### Getting element attributes
You can get all element attributes

```cpp
int main()
{
    ...
    Element el = client.FindElement(device, L"//node[@resource-id='Login']", TimeSpan(3));
    auto eltext = el.Attributes().Lookup(L"text");
    auto bounds = el.Attributes().Lookup(L"bounds");
    ...
}
```


### Clicking on an element
You can click on the x and y coordinates

```cpp
int main()
{
    ...
    client.Click(device, 600, 600); // Click on the coordinates (600;600)
    ...
}
```

Or on the element(need xpath)

```cpp
int main()
{
    ...
    Element el = client.FindElement(device, L"//node[@text='Login']", TimeSpan(3));
    el.Click();// Click on element by xpath //node[@text='Login']
    ...
}
```

The Click() method throw ElementNotFoundException if the element is not found

```cpp
try
{
    el.Click();
}
catch (exception ex)
{
    cout << "Can't click on the element:" << ex.what() << endl;
}
```


### Swipe
You can swipe from one element to another

```cpp
int main()
{
    ...
    Element first = client.FindElement(device, L"//node[@text='Login']");
    Element second = client.FindElement(device, L"//node[@text='Password']");
    client.Swipe(device, first, second, 100); // Swipe 100 ms
    ...
}
```

Or swipe by coordinates

```cpp
int main()
{
    ...
    device = *client.GetDevices().First();
    client.Swipe(device, 600, 1000, 600, 500, 100); // Swipe from (600;1000) to (600;500) on 100 ms
    ...
}
```

The Swipe() method throw ElementNotFoundException if the element is not found

```cpp
try
{
    client.Swipe(device, 0x2232323, 0x954, 0x9128, 0x11111, 200);
    ...
    client.Swipe(device, first, second, 200);
}
catch (exception ex)
{
    cout << "Can't swipe:" << ex.what() << endl;
}
```


### Send text
You can send any text except Cyrillic (Russian isn't supported by adb)

The text field should be in focus

```cpp
int main()
{
    ...
    client.SendText(device, L"text"); // Send text to device
    ...
}
```

You can also send text to the element (clicks on the element and sends the text)

```cpp
int main()
{
    ...
    client.FindElement(device, L"//node[@resource-id='Login']").SendText(L"text"); // Send text to the element by xpath //node[@resource-id='Login']
    ...
}
```

The SendText() method throw InvalidTextException if text is incorrect

```cpp
try
{
    client.SendText(device, L"");
}
catch (exception ex)
{
    cout << "Can't send text:" << ex.what() << endl;
}
```


### Clearing the input text

You can clear text input

The text field should be in focus

**Recommended**

```cpp
int main()
{
    ...
    client.ClearInput(device, 25); // The second argument is to specify the maximum number of characters to be erased
    ...
}
```

**It may work unstable**

```cpp
int main()
{
    ...
    client.FindElement(device, L"//node[@resource-id='Login']").ClearInput(); // Get element text attribute and remove text length symbols
    ...
}
```

### Sending keyevents

You can see keyevents here https://developer.android.com/reference/android/view/KeyEvent#constants

```cpp
int main()
{
    ...
    client.SendKeyEvent(device, L"KEYCODE_TAB");
    ...
}
```

The SendKeyEvent method throw InvalidKeyEventException if keyevent is incorrect

```cpp
try
{
    client.SendKeyEvent(device, "");
}
catch (exception ex)
{
    cout << "Can't send keyevent:" << ex.what() << endl;
}
```

### BACK and HOME buttons

```cpp
int main()
{
    ...
    client.BackBtn(device); // Click Back button
    ...
    client.HomeBtn(device); // Click Home button
    ...
}
```

## Device commands
**Some commands require Root**
### Install and Uninstall applications

```cpp
int main()
{
    ...
    PackageManager manager = PackageManager::PackageManager(client, device);
    manager.InstallPackage(L"C:\\Users\\me\\Documents\\mypackage.apk", false);
    manager.UninstallPackage(L"com.android.app");
    ...
}
```

Or you can use AdbClient.Install

```cpp
int main()
{
    ...
    client.Install(device, StorageFile::GetFileFromPathAsync(L"Application.apk").GetResults().OpenAsync(FileAccessMode::Read).GetResults(), {});
    ...
}
```

### Install multiple applications

```cpp
int main()
{
    ...
    PackageManager manager = PackageManager::PackageManager(client, device);
    manager.InstallMultiplePackage(L"C:\\Users\\me\\Documents\\base.apk", { L"C:\\Users\\me\\Documents\\split_1.apk", L"C:\\Users\\me\\Documents\\split_2.apk" }, false); // Install split app whith base app
    manager.InstallMultiplePackage({ L"C:\\Users\\me\\Documents\\split_3.apk", L"C:\\Users\\me\\Documents\\split_4.apk" }, L"com.android.app", false); // Add split app to base app which packagename is 'com.android.app'
    ...
}
```

Or you can use AdbClient.InstallMultiple

```cs
static void Main(string[] args)
{
    ...
    client.InstallMultiple(device, File.OpenRead("base.apk"), new Stream[] { File.OpenRead("split_1.apk"), File.OpenRead("split_2.apk") }); // Install split app whith base app
    client.InstallMultiple(device, new Stream[] { File.OpenRead("split_3.apk"), File.OpenRead("split_4.apk") }, "com.android.app"); // Add split app to base app which packagename is 'com.android.app'
    ...
}
```
### Start and stop applications

```cpp
int main()
{
    ...
    client.StartApp(device, L"com.android.app");
    client.StopApp(device, L"com.android.app"); // force-stop
    ...
}
```

### Getting a screenshot

```cs
static async void Main(string[] args)
{
    ...
    System.Drawing.Image img = client.GetFrameBufferAsync(device, CancellationToken.None).GetAwaiter().GetResult(); // synchronously
    ...
    System.Drawing.Image img = await client.GetFrameBufferAsync(device, CancellationToken.None); // asynchronously
    ...
}
```

### Getting screen xml hierarchy

```cpp
int main()
{
    ...
    XmlDocument screen = client.DumpScreen(device);
    ...
}
```

### Send or receive files

```cs
void DownloadFile()
{
    using (SyncService service = new SyncService(new AdbSocket(client.EndPoint), device))
    {
        using (Stream stream = File.OpenWrite(@"C:\MyFile.txt"))
        {
            service.Pull("/data/local/tmp/MyFile.txt", stream, null, CancellationToken.None);
        }
    }
}

void UploadFile()
{
    using (SyncService service = new SyncService(new AdbSocket(client.EndPoint), device))
    {
        using (Stream stream = File.OpenRead(@"C:\MyFile.txt"))
        {
            service.Push(stream, "/data/local/tmp/MyFile.txt", 777, DateTimeOffset.Now, null, CancellationToken.None);
        }
    }
}
```

### Run shell commands

```cpp
IAsyncAction Main()
{
    ...
    ConsoleOutputReceiver receiver = ConsoleOutputReceiver::ConsoleOutputReceiver();
    client.ExecuteRemoteCommand(L"echo Hello, World", device, receiver); // synchronously
    ...
    co_await client.ExecuteRemoteCommandAsync(L"echo Hello, World", device, receiver); // asynchronously
    ...
}
```

### Encoding
Default encoding is UTF8, if you want to change it, use

```cpp
AdbClient::SetEncoding(0);
```

## Contributors
[![Contributors](https://contrib.rocks/image?repo=yungd1plomat/AdvancedSharpAdbClient)](https://github.com/yungd1plomat/AdvancedSharpAdbClient/graphs/contributors)

## Consulting and Support
Please open an **issue** on if you have suggestions or problems.

## History
AdvancedSharpAdbClient is a fork of [SharpAdbClient](https://github.com/quamotion/madb) and [madb](https://github.com/camalot/madb) which in itself is a .NET port of the [ddmlib Java Library](https://android.googlesource.com/platform/tools/base/+/master/ddmlib/).

Credits:
https://github.com/camalot, https://github.com/quamotion