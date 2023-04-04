using ConsoleAppFinallyProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Data
{
    public class Book:IEquatable<Book>
    {
        static int counter = 0;
        public Book()
        {
            counter++;
            this.Id = counter;
            AuthorId = counter;
        }
       
        public int Id { get; private set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public GenrEnum GenrEnum { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }


        public override string ToString()
        {
            return $" | Id: {Id} |\n | Kitabin Adi: {Name} |\n | Janri: {GenrEnum} |\n | Seyfe Sayi: {PageCount} |\n | Qiymeti: {Price}₼ |\n";
        }

        public bool Equals(Book? other)
        {
            return other?.Id == this.Id;
        }
        public static GenrEnum ReadGenre(string question)
        {
            Console.Clear();
            Console.WriteLine(question);
            Type type = typeof(GenrEnum);
            Console.Clear();
            Console.WriteLine("Janri secin:  ");
            foreach (var item in Enum.GetValues(type))
            {
                Console.WriteLine($"{((int)item).ToString().PadLeft(2, '0')}.{item}");
            }
            Console.WriteLine("================");
        lGenre:
            Console.WriteLine("Rejimi Secin:  ");
            if (!Enum.TryParse<GenrEnum>(Console.ReadLine(), out GenrEnum selectedGenre) || !Enum.IsDefined(type, selectedGenre))
            {
                Console.WriteLine("Duzgun Daxil Edilmeyib");
                goto lGenre;
            }
            return selectedGenre;

        }


    }

}
/*
              - Author CRUD
                

AuthorStructure :
                                    Id    +         number (++)
                                    Name  +    text
                                    Surname + text
               =========================
                - CREATE    (Add)
                - READ        (GetAll | FindByName | GetById)
                - UPDATE   (Edit)
                - DELETE    (Remove)

             - Book CRUD
                

BookStructure :
                                    Id                 number (++)
                                    Name          text
                                    AuthorId      number
                                    Genre          enum
                                    PageCount number
                                    Price            number(decimal)
               =========================
                - CREATE   (Add)
                - READ     (GetAll | FindByName | GetById)
                - UPDATE   (Edit)
                - DELETE   (Remove)
=================================================
 */