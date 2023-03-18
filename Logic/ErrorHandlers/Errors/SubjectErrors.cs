namespace Logic.ErrorHandlers.Errors;

public static class SubjectErrors
{
    public static readonly Error CodeAlreadyExists = new("Subject.CodeAlreadyExists",
        "A subject with the same code is already exists");
    
    public static readonly Error NameAlreadyExists = new("Subject.NameAlreadyExists",
        "A subject with the same name is already exists");

    public static readonly Error WrongId = new("Subject.WrongId",
        "The id is wrong");

    public static readonly Error InvalidYear = new("Subject.InvalidYear",
        "The year number you entered is invalid");

    public static readonly Error WrongName = new("Subject.WrongName",
        "The name is wrong");

}