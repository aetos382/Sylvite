using System;

namespace Sylvite.Transport;

[Serializable]
public readonly record struct TextSpan(
    int Start,
    int End);
