using System;
using System.Collections.Generic;
using System.Text;

namespace AAEICS.Services.AppConfiguration
{
    public sealed class AppConfig
    {
        public KeysSection Keys { get; set; } = new();
        public PathSection Path { get; set; } = new();
    }

    public sealed class KeysSection
    {
        public string Syncfusion { get; set; } = "";
    }
        
    public sealed class PathSection
    {
        public string DbPath { get; set; } = "";
        public string AssetsPath { get; set; } = "";
        public string DbBackupPath { get; set; } = "";
        public string LogsPath { get; set; } = "";
    }
}
