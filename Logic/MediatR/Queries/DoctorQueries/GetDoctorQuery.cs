﻿using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.DoctorQueries;

public record GetDoctorQuery(string Id, string UserRequestId) : IRequest<Response<DoctorDto>>;