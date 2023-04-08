using ConsoleAppFinallyProject.Data;

namespace ConsoleAppFinallyProject.Storage
{
    [Serializable]
    public class Database
    {
        public GenericStore<Author> Authors { get; set; }
        public GenericStore<Book> Books { get; set; }
    }
}
