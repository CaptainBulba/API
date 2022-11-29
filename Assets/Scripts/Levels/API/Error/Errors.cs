public enum Errors
{
    None,
    Null, 
    LongName,
    NotInteger,
    ObjectExists,
    NotJson,
    WrongValue,
    TooFar,
    WrongToken,
    Boolean,
    ObjectNotExists,
    NoPermission
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
            case Errors.WrongValue:
                return "has wrong value";
            case Errors.TooFar:
                return "is too far";
            case Errors.WrongToken:
                return "is wrong";
            case Errors.Boolean:
                return "is not boolean";
            case Errors.ObjectNotExists:
                return "does not exists";
            case Errors.NoPermission:
                return "You do not have a permission";
            default:
                return "Unknown error";
        }
    }
}


