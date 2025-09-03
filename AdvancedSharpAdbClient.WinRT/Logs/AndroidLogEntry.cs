// <copyright file="AndroidLogEntry.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT.Logs
{
    internal partial class AndroidLogEntry : ILogEntry
    {
        internal readonly AdvancedSharpAdbClient.Logs.AndroidLogEntry androidLogEntry;

        /// <summary>
        /// Gets or sets the process ID of the code that generated the log message.
        /// </summary>
        public int ProcessId
        {
            get => androidLogEntry.ProcessId;
            set => androidLogEntry.ProcessId = value;
        }

        /// <summary>
        /// Gets or sets the thread ID of the code that generated the log message.
        /// </summary>
        public int ThreadId
        {
            get => androidLogEntry.ThreadId;
            set => androidLogEntry.ThreadId = value;
        }

        /// <summary>
        /// Gets or sets the date and time at which the message was logged.
        /// </summary>
        public DateTimeOffset TimeStamp
        {
            get => androidLogEntry.TimeStamp;
            set => androidLogEntry.TimeStamp = value;
        }

        /// <summary>
        /// Gets or sets the log id (v3) of the payload effective UID of logger (v2);
        /// this value is not available for v1 entries.
        /// </summary>
        public uint Id
        {
            get => androidLogEntry.Id;
            set => androidLogEntry.Id = value;
        }

        /// <summary>
        /// Gets or sets the entry's payload.
        /// </summary>
        public byte[] Data
        {
            get => androidLogEntry.Data;
            set => androidLogEntry.Data = value;
        }

        /// <summary>
        /// Gets or sets the priority of the log message.
        /// </summary>
        public Priority Priority
        {
            get => (Priority)androidLogEntry.Priority;
            set => androidLogEntry.Priority = (AdvancedSharpAdbClient.Logs.Priority)value;
        }

        /// <summary>
        /// Gets or sets the log tag of the message. Used to identify the source of a log message.
        /// It usually identifies the class or activity where the log call occurred.
        /// </summary>
        public string Tag
        {
            get => androidLogEntry.Tag;
            set => androidLogEntry.Tag = value;
        }

        /// <summary>
        /// Gets or sets the message that has been logged.
        /// </summary>
        public string Message
        {
            get => androidLogEntry.Message;
            set => androidLogEntry.Message = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidLogEntry"/> class.
        /// </summary>
        public AndroidLogEntry() => androidLogEntry = new();

        internal AndroidLogEntry(AdvancedSharpAdbClient.Logs.AndroidLogEntry androidLogEntry) => this.androidLogEntry = androidLogEntry;

        /// <inheritdoc/>
        public override string ToString() => androidLogEntry.ToString();
    }
}
