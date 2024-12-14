﻿using Posetrix.Core.Models;

namespace Posetrix.Core.Interfaces;

public interface IDynamicViewModel
{
    string DisplayName { get; }

    int Seconds { get; set; }

    bool CanStart { get; }

    SessionTimer GetTimer();
}
