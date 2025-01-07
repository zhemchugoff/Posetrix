using Posetrix.Core.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Posetrix.Services;

class RuntimeInformationService : IRuntimeInformation
{
    public string AppVersion => $"Posetrix v{FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion}";
    public string NetVersion => $"App .NET version: {RuntimeInformation.FrameworkDescription}";
}
