﻿// <copyright file="IAdbClient.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.Exceptions;
using AdvancedSharpAdbClient.WinRT.Logs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// A common interface for any class that allows you to interact with the
    /// adb server and devices that are connected to that adb server.
    /// </summary>
    public interface IAdbClient
    {
        /// <summary>
        /// Gets the <see cref="string"/> of the <see cref="EndPoint"/> at which the Android Debug Bridge server is listening.
        /// </summary>
        string EndPoint { get; }

        /// <summary>
        /// Ask the ADB server for its internal version number.
        /// </summary>
        /// <returns>The ADB version number.</returns>
        int GetAdbVersion();

        /// <summary>
        /// Ask the ADB server to quit immediately. This is used when the
        /// ADB client detects that an obsolete server is running after an
        /// upgrade.
        /// </summary>
        void KillAdb();

        /// <summary>
        /// Gets the devices that are available for communication.
        /// </summary>
        /// <returns>A list of devices that are connected.</returns>
        /// <example>
        /// <para>
        /// The following example list all Android devices that are currently connected to this PC:
        /// </para>
        /// <code>
        /// var devices = AdbClient.Instance.GetDevices();
        /// 
        /// foreach(var device in devices)
        /// {
        ///     Console.WriteLine(device.Name);
        /// }
        /// </code>
        /// </example>
        IEnumerable<DeviceData> GetDevices();

        /// <summary>
        /// Asks the ADB server to forward local connections from <paramref name="local"/>
        /// to the <paramref name="remote"/> address on the <paramref name="device"/>.
        /// </summary>
        /// <param name="device">The device on which to forward the connections.</param>
        /// <param name="local">
        /// <para>
        /// The local address to forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt;
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt;
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="remote">
        /// <para>
        /// The remote address to forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>jdwp:&lt;pid&gt;</c>: JDWP thread on VM process &lt;pid&gt; on device.
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="allowRebind">If set to <see langword="true"/>, the request will fail if there is already a forward
        /// connection from <paramref name="local"/>.</param>
        /// <returns>If your requested to start forwarding to local port TCP:0, the port number of the TCP port
        /// which has been opened. In all other cases, <c>0</c>.</returns>
        int CreateForward(DeviceData device, string local, string remote, bool allowRebind);

        /// <summary>
        /// Asks the ADB server to forward local connections from <paramref name="local"/>
        /// to the <paramref name="remote"/> address on the <paramref name="device"/>.
        /// </summary>
        /// <param name="device">The device on which to forward the connections.</param>
        /// <param name="local">
        /// <para>
        /// The local address to forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt;
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt;
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="remote">
        /// <para>
        /// The remote address to forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>jdwp:&lt;pid&gt;</c>: JDWP thread on VM process &lt;pid&gt; on device.
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="allowRebind">If set to <see langword="true"/>, the request will fail if there is already a forward
        /// connection from <paramref name="local"/>.</param>
        /// <returns>If your requested to start forwarding to local port TCP:0, the port number of the TCP port
        /// which has been opened. In all other cases, <c>0</c>.</returns>
        [DefaultOverload]
        int CreateForward(DeviceData device, ForwardSpec local, ForwardSpec remote, bool allowRebind);

        /// <summary>
        /// Asks the ADB server to reverse forward local connections from <paramref name="remote"/>
        /// to the <paramref name="local"/> address on the <paramref name="device"/>.
        /// </summary>
        /// <param name="device">The device on which to reverse forward the connections.</param>
        /// <param name="remote">
        /// <para>
        /// The remote address to reverse forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>jdwp:&lt;pid&gt;</c>: JDWP thread on VM process &lt;pid&gt; on device.
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="local">
        /// <para>
        /// The local address to reverse forward. This value can be in one of:
        /// </para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt;
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt;
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="allowRebind">If set to <see langword="true"/>, the request will fail if if the specified socket
        /// is already bound through a previous reverse command.</param>
        /// <returns>If your requested to start reverse to remote port TCP:0, the port number of the TCP port
        /// which has been opened. In all other cases, <c>0</c>.</returns>
        int CreateReverseForward(DeviceData device, string remote, string local, bool allowRebind);

        /// <summary>
        /// Remove a reverse port forwarding between a remote and a local port.
        /// </summary>
        /// <param name="device">The device on which to remove the reverse port forwarding</param>
        /// <param name="remote">Specification of the remote that was forwarded</param>
        void RemoveReverseForward(DeviceData device, string remote);

        /// <summary>
        /// Removes all reverse forwards for a given device.
        /// </summary>
        /// <param name="device">The device on which to remove all reverse port forwarding</param>
        void RemoveAllReverseForwards(DeviceData device);

        /// <summary>
        /// Remove a port forwarding between a local and a remote port.
        /// </summary>
        /// <param name="device">The device on which to remove the port forwarding.</param>
        /// <param name="localPort">Specification of the local port that was forwarded.</param>
        void RemoveForward(DeviceData device, int localPort);

        /// <summary>
        /// Removes all forwards for a given device.
        /// </summary>
        /// <param name="device">The device on which to remove the port forwarding.</param>
        void RemoveAllForwards(DeviceData device);

        /// <summary>
        /// List all existing forward connections from this server.
        /// </summary>
        /// <param name="device">The device for which to list the existing forward connections.</param>
        /// <returns>A <see cref="ForwardData"/> entry for each existing forward connection.</returns>
        IEnumerable<ForwardData> ListForward(DeviceData device);

        /// <summary>
        /// List all existing reverse forward connections from this server.
        /// </summary>
        /// <param name="device">The device for which to list the existing reverse forward connections.</param>
        /// <returns>A <see cref="ForwardData"/> entry for each existing reverse forward connection.</returns>
        IEnumerable<ForwardData> ListReverseForward(DeviceData device);

        /// <summary>
        /// Executes a command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="receiver">The receiver which will get the command output.</param>
        void ExecuteRemoteCommand(string command, DeviceData device, IShellOutputReceiver receiver);

        /// <summary>
        /// Executes a command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="receiver">The receiver which will get the command output.</param>
        /// <param name="encoding">The encoding to use when parsing the command output.</param>
        void ExecuteRemoteCommand(string command, DeviceData device, IShellOutputReceiver receiver, int encoding);

        /// <summary>
        /// Gets a <see cref="Framebuffer"/> which contains the framebuffer data for this device. The framebuffer data can be refreshed,
        /// giving you high performance access to the device's framebuffer.
        /// </summary>
        /// <param name="device">The device for which to get the framebuffer.</param>
        /// <returns>A <see cref="Framebuffer"/> object which can be used to get the framebuffer of the device.</returns>
        Framebuffer CreateRefreshableFramebuffer(DeviceData device);

        /// <summary>
        /// Gets the frame buffer from the specified end point.
        /// </summary>
        /// <param name="device">The device for which to get the framebuffer.</param>
        /// <returns>The raw frame buffer.</returns>
        /// <exception cref="AdbException">failed asking for frame buffer</exception>
        /// <exception cref="AdbException">failed nudging</exception>
        Framebuffer GetFrameBuffer(DeviceData device);

        /// <summary>
        /// Runs the event log service on a device.
        /// </summary>
        /// <param name="device">The device on which to run the event log service.</param>
        /// <param name="messageSink">A callback which will receive the event log messages as they are received.</param>
        /// <param name="logNames">Optionally, the names of the logs to receive.</param>
        void RunLogService(DeviceData device, MessageSink messageSink, [ReadOnlyArray] params LogId[] logNames);

        /// <summary>
        /// Reboots the specified adb socket address.
        /// </summary>
        /// <param name="device">The device to reboot.</param>
        void Reboot(DeviceData device);

        /// <summary>
        /// Reboots the specified device in to the specified mode.
        /// </summary>
        /// <param name="into">The mode into which to reboot the device.</param>
        /// <param name="device">The device to reboot.</param>
        void Reboot(string into, DeviceData device);

        /// <summary>
        /// Pair with a device for secure TCP/IP communication.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <param name="code">The pairing code.</param>
        /// <returns>The results from adb.</returns>
        string Pair(string host, string code);

        /// <summary>
        /// Pair with a device for secure TCP/IP communication.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <param name="port">The port of the remote device.</param>
        /// <param name="code">The pairing code.</param>
        /// <returns>The results from adb.</returns>
        string Pair(string host, int port, string code);

        /// <summary>
        /// Connect to a device via TCP/IP.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <returns>The results from adb.</returns>
        string Connect(string host);

        /// <summary>
        /// Connect to a device via TCP/IP.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <param name="port">The port of the remote device.</param>
        /// <returns>The results from adb.</returns>
        string Connect(string host, int port);

        /// <summary>
        /// Disconnects a remote device from this local ADB server.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <returns>The results from adb.</returns>
        string Disconnect(string host);

        /// <summary>
        /// Disconnects a remote device from this local ADB server.
        /// </summary>
        /// <param name="host">The host address of the remote device.</param>
        /// <param name="port">The port of the remote device.</param>
        /// <returns>The results from adb.</returns>
        string Disconnect(string host, int port);

        /// <summary>
        /// Restarts the ADB daemon running on the device with root privileges.
        /// </summary>
        /// <param name="device">The device on which to restart ADB with root privileges.</param>
        void Root(DeviceData device);

        /// <summary>
        /// Restarts the ADB daemon running on the device without root privileges.
        /// </summary>
        /// <param name="device">The device on which to restart ADB without root privileges.</param>
        void Unroot(DeviceData device);

        /// <summary>
        /// Installs an Android application on an device.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="apk">A <see cref="IInputStream"/> which represents the application to install.</param>
        void Install(DeviceData device, IInputStream apk);

        /// <summary>
        /// Installs an Android application on an device.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="apk">A <see cref="IInputStream"/> which represents the application to install.</param>
        /// <param name="arguments">The arguments to pass to <c>adb install</c>.</param>
        void Install(DeviceData device, IInputStream apk, [ReadOnlyArray] params string[] arguments);

        /// <summary>
        /// Push multiple APKs to the device and install them.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="splitAPKs"><see cref="IInputStream"/>s which represents the split APKs to install.</param>
        /// <param name="packageName">The package name of the base APK to install.</param>
        void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName);

        /// <summary>
        /// Push multiple APKs to the device and install them.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="splitAPKs"><see cref="IInputStream"/>s which represents the split APKs to install.</param>
        /// <param name="packageName">The package name of the base APK to install.</param>
        /// <param name="arguments">The arguments to pass to <c>adb install-create</c>.</param>
        void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName, [ReadOnlyArray] params string[] arguments);

        /// <summary>
        /// Push multiple APKs to the device and install them.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="baseAPK">A <see cref="IInputStream"/> which represents the base APK to install.</param>
        /// <param name="splitAPKs"><see cref="IInputStream"/>s which represents the split APKs to install.</param>
        [DefaultOverload]
        void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs);

        /// <summary>
        /// Push multiple APKs to the device and install them.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="baseAPK">A <see cref="IInputStream"/> which represents the base APK to install.</param>
        /// <param name="splitAPKs"><see cref="IInputStream"/>s which represents the split APKs to install.</param>
        /// <param name="arguments">The arguments to pass to <c>adb install-create</c>.</param>
        [DefaultOverload]
        void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs, [ReadOnlyArray] params string[] arguments);

        /// <summary>
        /// Like "install", but starts an install session.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <returns>The session ID of this install session.</returns>
        string InstallCreate(DeviceData device);

        /// <summary>
        /// Like "install", but starts an install session.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="arguments">The arguments to pass to <c>adb install-create</c>.</param>
        /// <returns>The session ID of this install session.</returns>
        string InstallCreate(DeviceData device, [ReadOnlyArray] params string[] arguments);

        /// <summary>
        /// Like "install", but starts an install session.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="packageName">The package name of the baseAPK to install.</param>
        /// <returns>The session ID of this install session.</returns>
        [DefaultOverload]
        string InstallCreate(DeviceData device, string packageName);

        /// <summary>
        /// Like "install", but starts an install session.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="packageName">The package name of the baseAPK to install.</param>
        /// <param name="arguments">The arguments to pass to <c>adb install-create</c>.</param>
        /// <returns>The session ID of this install session.</returns>
        string InstallCreate(DeviceData device, string packageName, [ReadOnlyArray] params string[] arguments);

        /// <summary>
        /// Write an apk into the given install session.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="apk">A <see cref="IInputStream"/> which represents the application to install.</param>
        /// <param name="apkName">The name of the application.</param>
        /// <param name="session">The session ID of the install session.</param>
        void InstallWrite(DeviceData device, IInputStream apk, string apkName, string session);

        /// <summary>
        /// Commit the given active install session, installing the app.
        /// </summary>
        /// <param name="device">The device on which to install the application.</param>
        /// <param name="session">The session ID of the install session.</param>
        void InstallCommit(DeviceData device, string session);

        /// <summary>
        /// Lists all features supported by the current device.
        /// </summary>
        /// <param name="device">The device for which to get the list of features supported.</param>
        /// <returns>A list of all features supported by the current device.</returns>
        IEnumerable<string> GetFeatureSet(DeviceData device);

        /// <summary>
        /// Gets the current device screen snapshot.
        /// </summary>
        /// <param name="device">The device for which to get the screen snapshot.</param>
        /// <returns>A <see cref="string"/> containing current hierarchy.
        /// Failed if start with <c>ERROR</c> or <c>java.lang.Exception</c>.</returns>
        string DumpScreenString(DeviceData device);

        /// <summary>
        /// Gets the current device screen snapshot.
        /// </summary>
        /// <param name="device">The device for which to get the screen snapshot.</param>
        /// <returns>Xml containing current hierarchy.</returns>
        XmlDocument DumpScreen(DeviceData device);

        /// <summary>
        /// Clicks on the specified coordinates.
        /// </summary>
        /// <param name="device">The device on which to click.</param>
        /// <param name="cords">The <see cref="Cords"/> to click.</param>
        void Click(DeviceData device, Cords cords);

        /// <summary>
        /// Clicks on the specified coordinates.
        /// </summary>
        /// <param name="device">The device on which to click.</param>
        /// <param name="x">The X co-ordinate to click.</param>
        /// <param name="y">The Y co-ordinate to click.</param>
        void Click(DeviceData device, int x, int y);

        /// <summary>
        /// Generates a swipe gesture from first element to second element Specify the speed in ms.
        /// </summary>
        /// <param name="device">The device on which to swipe.</param>
        /// <param name="first">The start element.</param>
        /// <param name="second">The end element.</param>
        /// <param name="speed">The time spent in swiping.</param>
        void Swipe(DeviceData device, Element first, Element second, long speed);

        /// <summary>
        /// Generates a swipe gesture from co-ordinates x1,y1 to x2,y2 with speed Specify the speed in ms.
        /// </summary>
        /// <param name="device">The device on which to swipe.</param>
        /// <param name="x1">The start X co-ordinate.</param>
        /// <param name="y1">The start Y co-ordinate.</param>
        /// <param name="x2">The end X co-ordinate.</param>
        /// <param name="y2">The end Y co-ordinate.</param>
        /// <param name="speed">The time spent in swiping.</param>
        void Swipe(DeviceData device, int x1, int y1, int x2, int y2, long speed);

        /// <summary>
        /// Check if the app is running in foreground.
        /// </summary>
        /// <param name="device">The device on which to check.</param>
        /// <param name="packageName">The package name of the app to check.</param>
        /// <returns><see langword="true"/> if the app is running in foreground; otherwise, <see langword="false"/>.</returns>
        bool IsCurrentApp(DeviceData device, string packageName);

        /// <summary>
        /// Check if the app is running in background.
        /// </summary>
        /// <param name="device">The device on which to check.</param>
        /// <param name="packageName">The package name of the app to check.</param>
        /// <returns><see langword="true"/> if the app is running in background; otherwise, <see langword="false"/>.</returns>
        bool IsAppRunning(DeviceData device, string packageName);

        /// <summary>
        /// Get the <see cref="AppStatus"/> of the app.
        /// </summary>
        /// <param name="device">The device on which to get status.</param>
        /// <param name="packageName">The package name of the app to check.</param>
        /// <returns>The <see cref="AppStatus"/> of the app. Foreground, stopped or running in background.</returns>
        AppStatus GetAppStatus(DeviceData device, string packageName);

        /// <summary>
        /// Get element by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get element.</param>
        /// <returns>The <see cref="Element"/> of <c>hierarchy/node</c>.</returns>
        Element FindElement(DeviceData device);

        /// <summary>
        /// Get element by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get element.</param>
        /// <param name="xpath">The xpath of the element.</param>
        /// <returns>The <see cref="Element"/> of <paramref name="xpath"/>.</returns>
        [DefaultOverload]
        Element FindElement(DeviceData device, string xpath);

        /// <summary>
        /// Get element by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get element.</param>
        /// <param name="timeout">The timeout for waiting the element.
        /// Only check once if <see langword="default"/> or <see cref="TimeSpan.Zero"/>.</param>
        /// <returns>The <see cref="Element"/> of <c>hierarchy/node</c>.</returns>
        Element FindElement(DeviceData device, TimeSpan timeout);

        /// <summary>
        /// Get element by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get element.</param>
        /// <param name="xpath">The xpath of the element.</param>
        /// <param name="timeout">The timeout for waiting the element.
        /// Only check once if <see langword="default"/> or <see cref="TimeSpan.Zero"/>.</param>
        /// <returns>The <see cref="Element"/> of <paramref name="xpath"/>.</returns>
        Element FindElement(DeviceData device, string xpath, TimeSpan timeout);

        /// <summary>
        /// Get elements by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get elements.</param>
        /// <returns>The <see cref="Array"/> of <see cref="Element"/> has got.</returns>
        IEnumerable<Element> FindElements(DeviceData device);

        /// <summary>
        /// Get elements by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get elements.</param>
        /// <param name="xpath">The xpath of the elements.</param>
        /// <returns>The <see cref="Array"/> of <see cref="Element"/> has got.</returns>
        [DefaultOverload]
        IEnumerable<Element> FindElements(DeviceData device, string xpath);

        /// <summary>
        /// Get elements by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get elements.</param>
        /// <param name="timeout">The timeout for waiting the elements.
        /// Only check once if <see langword="default"/> or <see cref="TimeSpan.Zero"/>.</param>
        /// <returns>The <see cref="Array"/> of <see cref="Element"/> has got.</returns>
        IEnumerable<Element> FindElements(DeviceData device, TimeSpan timeout);

        /// <summary>
        /// Get elements by xpath. You can specify the waiting time in timeout.
        /// </summary>
        /// <param name="device">The device on which to get elements.</param>
        /// <param name="xpath">The xpath of the elements.</param>
        /// <param name="timeout">The timeout for waiting the elements.
        /// Only check once if <see langword="default"/> or <see cref="TimeSpan.Zero"/>.</param>
        /// <returns>The <see cref="Array"/> of <see cref="Element"/> has got.</returns>
        IEnumerable<Element> FindElements(DeviceData device, string xpath, TimeSpan timeout);

        /// <summary>
        /// Send key event to specific. You can see key events here https://developer.android.com/reference/android/view/KeyEvent.
        /// </summary>
        /// <param name="device">The device on which to send key event.</param>
        /// <param name="key">The key event to send.</param>
        void SendKeyEvent(DeviceData device, string key);

        /// <summary>
        /// Send text to device. Doesn't support Russian.
        /// </summary>
        /// <param name="device">The device on which to send text.</param>
        /// <param name="text">The text to send.</param>
        void SendText(DeviceData device, string text);

        /// <summary>
        /// Clear the input text. The input should be in focus. Use <see cref="Element.ClearInput(int)"/> if the element isn't focused.
        /// </summary>
        /// <param name="device">The device on which to clear the input text.</param>
        /// <param name="charCount">The length of text to clear.</param>
        void ClearInput(DeviceData device, int charCount);

        /// <summary>
        /// Start an Android application on device.
        /// </summary>
        /// <param name="device">The device on which to start an application.</param>
        /// <param name="packageName">The package name of the application to start.</param>
        void StartApp(DeviceData device, string packageName);

        /// <summary>
        /// Stop an Android application on device.
        /// </summary>
        /// <param name="device">The device on which to stop an application.</param>
        /// <param name="packageName">The package name of the application to stop.</param>
        void StopApp(DeviceData device, string packageName);

        /// <summary>
        /// Click BACK button.
        /// </summary>
        /// <param name="device">The device on which to click BACK button.</param>
        void BackBtn(DeviceData device);

        /// <summary>
        /// Click HOME button.
        /// </summary>
        /// <param name="device">The device on which to click HOME button.</param>
        void HomeBtn(DeviceData device);
    }
}
