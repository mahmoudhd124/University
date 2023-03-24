﻿namespace Logic.ErrorHandlers.Errors;

public static class SubjectMaterialErrors
{
    public static readonly Error UnAuthorizedAdd = new("SubjectMaterialErrors.UnAuthorizedAdd",
        "You can not add a material to a subject you are not assigned to");

    public static readonly Error UnAuthorizedDelete = new("SubjectMaterialErrors.UnAuthorizedDelete",
        "You can not delete a material to a subject you are not assigned to");

    public static readonly Error WrongId = new("SubjectMaterialErrors.WrongId",
        "The id is wrong");
}