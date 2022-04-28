﻿using MediatR;
using AutoMapper;
using Application.Contracts.Persistence;
using Domain.Models.Roles;
using Application.DtoModels.Admin;

namespace Application.Features.Admins.Commands
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, AdminDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateAdminCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AdminDto> Handle(CreateAdminCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var admin = _mapper.Map<Admin>(command.Admin);
                await _unitOfWork.Admins.CreateAdminAsync(admin);
                await _unitOfWork.CommitAsync(cancellationToken);
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }

            return command.Admin;
        }
    }
}
