﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Wox.Infrastructure.Storage;

namespace Microsoft.Plugin.Program.Storage
{
    internal class Win32ProgramFileSystemWatchers : IDisposable
    {
        public string[] PathsToWatch { get; set; }

        public List<FileSystemWatcherWrapper> FileSystemWatchers { get; set; }

        private bool _disposed = false;

        // This class contains the list of directories to watch and initializes the File System Watchers
        public Win32ProgramFileSystemWatchers()
        {
            PathsToWatch = GetPathsToWatch();
            SetFileSystemWatchers();
        }

        // Returns an array of paths to be watched
        private static string[] GetPathsToWatch()
        {
            string[] paths = new string[]
                            {
                               Environment.GetFolderPath(Environment.SpecialFolder.Programs),
                               Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms),
                               Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            };
            return paths;
        }

        // Initializes the FileSystemWatchers
        private void SetFileSystemWatchers()
        {
            FileSystemWatchers = new List<FileSystemWatcherWrapper>();
            for (int index = 0; index < PathsToWatch.Length; index++)
            {
                FileSystemWatchers.Add(new FileSystemWatcherWrapper());
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    for (int index = 0; index < PathsToWatch.Length; index++)
                    {
                        FileSystemWatchers[index].Dispose();
                    }

                    _disposed = true;
                }
            }
        }
    }
}
