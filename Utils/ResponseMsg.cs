using System;

namespace TheFirstProject.Utils;

public class ResponseMsg<T>(int Code, bool Status, string Message, T Data)
{
    public int Code { get; set; } = Code;
    public bool Status { get; set; } = Status;
    public string Message { get; set; } = Message;
    public T Data { get; set; } = Data;

    public static ResponseMsg<T> Ok(T Data)
    {
        return new ResponseMsg<T>(200, true, "", Data);
    }

    public static ResponseMsg<T> Fail(T Data)
    {
        return new ResponseMsg<T>(200, false, "", Data);
    }
}
