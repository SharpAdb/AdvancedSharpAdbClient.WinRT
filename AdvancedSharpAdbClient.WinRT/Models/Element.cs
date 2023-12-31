﻿// <copyright file="Element.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using XmlDocument = Windows.Data.Xml.Dom.XmlDocument;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Implement of screen element, likes Selenium.
    /// </summary>
    public sealed class Element
    {
        internal readonly AdvancedSharpAdbClient.Element element;

        /// <summary>
        /// Contains element coordinates.
        /// </summary>
        public Cords Cords
        {
            get => Cords.GetCords(element.Cords);
            set => element.Cords = value.cords;
        }

        /// <summary>
        /// Gets the coordinates and size of the element.
        /// </summary>
        public Area Area => Area.GetArea(element.Area);

        /// <summary>
        /// Gets the children of this element.
        /// </summary>
        public IEnumerable<Element> Children => element.Children.Select(GetElement);

        /// <summary>
        /// Gets or sets element attributes.
        /// </summary>
        public IDictionary<string, string> Attributes => element.Attributes;

        /// <summary>
        /// Gets the <see cref="IXmlNode"/> of this element.
        /// </summary>
        public IXmlNode Node
        {
            get
            {
                XmlDocument doc = new();
                doc.LoadXml(element.Node.OuterXml);
                return doc.FirstChild;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="client">The current ADB client that manages the connection.</param>
        /// <param name="device">The current device containing the element.</param>
        /// <param name="cords">Contains element coordinates .</param>
        /// <param name="attributes">Gets or sets element attributes.</param>
        public Element(AdbClient client, DeviceData device, Cords cords, IDictionary<string, string> attributes) =>
            element = new(client.adbClient, device.deviceData, cords.cords, attributes.GetDictionary());

        internal Element(AdvancedSharpAdbClient.Element element) => this.element = element;

        internal static Element GetElement(AdvancedSharpAdbClient.Element element) => new(element);

        /// <summary>
        /// Gets the count of <see cref="Children"/> in this element.
        /// </summary>
        public int GetChildCount() => element.GetChildCount();

        /// <summary>
        /// Clicks on this coordinates.
        /// </summary>
        public void Click() => element.Click();

        /// <summary>
        /// Clicks on this coordinates.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClickAsync() => AsyncInfo.Run(element.ClickAsync);

        /// <summary>
        /// Send text to device. Doesn't support Russian.
        /// </summary>
        /// <param name="text">The text to send.</param>
        public void SendText(string text) => element.SendText(text);

        /// <summary>
        /// Send text to device. Doesn't support Russian.
        /// </summary>
        /// <param name="text">The text to send.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction SendTextAsync(string text) => AsyncInfo.Run((cancellationToken) => element.SendTextAsync(text, cancellationToken));

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInput(DeviceData, int)"/> if the element is focused.
        /// </summary>
        public void ClearInput() => element.ClearInput();

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClientAsync.ClearInputAsync(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClearInputAsync() => AsyncInfo.Run((cancellationToken) => element.ClearInputAsync(cancellationToken: cancellationToken));

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInput(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <param name="charCount">The length of text to clear.</param>
        public void ClearInput(int charCount) => element.ClearInput(charCount);

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClientAsync.ClearInputAsync(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <param name="charCount">The length of text to clear.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClearInputAsync(int charCount) => AsyncInfo.Run((cancellationToken) => element.ClearInputAsync(charCount, cancellationToken));
    }
}
