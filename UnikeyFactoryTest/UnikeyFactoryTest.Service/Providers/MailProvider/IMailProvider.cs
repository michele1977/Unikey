namespace UnikeyFactoryTest.Service.Providers.MailProvider
{
    public interface IMailProvider
    {
        bool SendMail(string user, string UserName, string URL);
    }
}