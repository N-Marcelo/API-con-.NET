    public class HelloworldService : IHelloWorldService
    {
        public string GetHelloWorld()
        {
            return "Hello world!";
        }
    }

    public interface IHelloWorldService
    {
        string GetHelloWorld();
    }