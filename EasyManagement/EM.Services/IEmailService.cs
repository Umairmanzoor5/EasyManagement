using EM.DataRepository.Emails;

namespace EM.Services;

public interface IEmailService
{
    Task SendEmailTask(SendEmail email);
}

