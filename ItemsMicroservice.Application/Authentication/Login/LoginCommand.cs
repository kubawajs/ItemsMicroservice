﻿using MediatR;

namespace ItemsMicroservice.Application.Authentication.Login;

public sealed record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;