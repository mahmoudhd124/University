namespace Logic.ErrorHandlers.Errors;

public static class SubjectErrors
{
    public static readonly Error NameAlreadyExists = new("Subject.NameAlreadyExists",
        "A subject with the same name is already exists");

    public static readonly Error WrongId = new("Subject.WrongId",
        "The id is wrong");
}