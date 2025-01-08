using Posetrix.Core.Interfaces;
using System.Runtime.InteropServices;

namespace Posetrix.Services;

class RuntimeInformationService : IRuntimeInformation
{
    public string NetVersion => $".NET version: {RuntimeInformation.FrameworkDescription}";
}