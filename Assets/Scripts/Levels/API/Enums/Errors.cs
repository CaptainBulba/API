using System.ComponentModel;

public enum Errors
{
    None,
    Null, 
    LongName,
    NotInteger,
    ObjectExists,
    NotJson,
    WrongVariable
}

public static class ErrorDescriptions
{
    public static string GetErrorDescription(this Errors error)
    {
        switch (error)
        {
            case Errors.Null:
                return "is null";
            case Errors.LongName:
                return "is too long";
            case Errors.NotInteger:
                return "is not an integer";
            case Errors.ObjectExists:
                return "already exists";
            case Errors.NotJson:
                return "is not valid";
            case Errors.WrongVariable:
                return "does not exist";
            default:
                return "Unknown error";
        }
    }
}


