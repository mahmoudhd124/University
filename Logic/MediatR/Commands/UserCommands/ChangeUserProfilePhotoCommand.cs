using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.UserCommands;

public record ChangeUserProfilePhotoCommand(string Id,string Name,Stream Stream):IRequest<Response<bool>>;