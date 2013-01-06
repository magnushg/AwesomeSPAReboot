namespace AwesomeSPAReboot.Services
{
    public interface ISampleService
    {
        string[] Hello();
    }

    public class SampleService : ISampleService
    {
        public string[] Hello()
        {
            return new string[]
                       {
                           "Hello World!",
                           "Goodbye world!!"
                        };
        }
    }
}
