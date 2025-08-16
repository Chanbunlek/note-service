using System;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TheFirstProject.Utils;

public class Responses
{
    public static ActionResult<ResponseMsg<T>> Ok<T>(T Data)
    {
        return new ResponseMsg<T>(200, true, "", Data);
    }

    public static ActionResult<ResponseMsg<T>> Fail<T>(T Data)
    {
        return new ResponseMsg<T>(200, false, "", Data);
    }
}
