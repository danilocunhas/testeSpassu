using FluentResults;
using MediatR;

namespace LibraryManager.Application.Commands.Subject
{
    public class CreateSubjectCommand : IRequest<Result>
    {
        public int SubjectCode { get; private set; }
        public string Description { get; private set; }

        public CreateSubjectCommand(int subjectCode, string description)
        {
            SubjectCode = subjectCode;
            Description = description;
        }
    }
}
