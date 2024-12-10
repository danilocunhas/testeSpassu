using FluentResults;
using DomainEntities = LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.Subject
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Result>
    {

        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Result> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = new DomainEntities.Subject(request.SubjectCode, request.Description);
                await _subjectRepository.CreateAsync(subject, cancellationToken);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
