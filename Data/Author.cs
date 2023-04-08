using ConsoleAppFinallyProject.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Data
{
    [Serializable]
    public class Author: IIdentity ,IEquatable<Author>
    {

        static int counter;
        public Author()
        {
            counter++;
            this.Id = counter;
        }
        public Author(int id)
        {
            counter = id;
            counter++;
            this.Id = counter;
        }
        public int Id { get; private set; }
        public string Name;
        public string Surname;

        public bool Equals(Author? other)
        {
            return other?.Id == this.Id;
        }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Surname} | ";
        }
    }
}
