using Posetrix.Core.Models;

namespace Posetrix.Core.Interfaces;

public interface IConfigService
{
    FileExtensionConfig LoadConfig();
}
