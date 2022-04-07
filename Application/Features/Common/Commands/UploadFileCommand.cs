using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Application.Features.Common.Commands;
public class UploadFileCommand : IRequest<string>
{

    public IFormFile FormFile { get; set; }
    public string Path { get; set; }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        
        public async Task<string> Handle(UploadFileCommand command, CancellationToken cancellationToken)
        {
            var extension = "." + command.FormFile.FileName.Split('.')[command.FormFile.FileName.Split('.').Length - 1];

            var fileName = DateTime.Now.Ticks + extension;
            command.Path = command.Path;
            if (!Directory.Exists(command.Path))
            {
                Directory.CreateDirectory(command.Path);
            }
            using (FileStream stream = new(command.Path + @"\" + fileName, FileMode.Create))
            {
                await command.FormFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.Path)
                .NotEmpty()
                .WithMessage("Path should be not empty!");
        }
    }

}
