﻿using System;

namespace TravelCompanion.Shared.Infrastructure.Modules
{
    public interface IModuleSerializer
    {
        byte[] Serialize<T>(T value);
        T Deserialize<T>(byte[] value);
        object Deserialize(byte[] value, Type type);
    }
}