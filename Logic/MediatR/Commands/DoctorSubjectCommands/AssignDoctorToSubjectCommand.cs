using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.DoctorSubjectCommands;

public record AssignDoctorToSubjectCommand(string DoctorId, int SubjectId) : IRequest<Response<bool>>;