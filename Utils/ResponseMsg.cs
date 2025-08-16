using System;

namespace TheFirstProject.Utils;

public class ResponseMsg<T>
{
    public int Code { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public ResponseMsg(int Code, bool Status, string Message, T Data)
    {
        this.Code = Code;
        this.Status = Status;
        this.Message = Message;
        this.Data = Data;
    }

    public ResponseMsg(int Code, bool Status, string Message)
    {
        this.Code = Code;
        this.Status = Status;
        this.Message = Message;
    }

    public static ResponseMsg<T> Ok(T Data)
    {
        return new ResponseMsg<T>(200, true, "", Data);
    }

    public static ResponseMsg<T> Fail(T Data)
    {
        return new ResponseMsg<T>(200, false, "", Data);
    }
}
