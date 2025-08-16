using System;

namespace TheFirstProject.Utils;

public class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string Message) : base(Message) { }
}
