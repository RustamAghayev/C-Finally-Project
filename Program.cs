using ConsoleAppFinallyProject.Data;
using ConsoleAppFinallyProject.Enums;
using ConsoleAppFinallyProject.Helpers;
using ConsoleAppFinallyProject.Managers;
using ConsoleAppFinallyProject.Storage;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ConsoleAppFinallyProject
{
    internal class Program
    {
        const string database = "database.dat";

        static GenericStore<Author> authormanager = new GenericStore<Author>();
        static GenericStore<Book> bookmanager = new GenericStore<Book>();
        static void Main(string[] args)
        {
            int maxBookId=0;
            int maxAuthorId=0;

            using (FileStream fileStream = File.Open(database, FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var db = (Database)bf.Deserialize(fileStream);

                    if (db != null)
                    {
                        authormanager = db.Authors;
                        bookmanager = db.Books;
                    }
                    if (db.Books.Length!=0)
                    {
                    maxBookId = db.Books.Max(book => book.Id);
                    }
                    if (db.Authors.Length!=0)
                    {
                        maxAuthorId = db.Authors.Max(author => author.Id);
                    }

                }
                catch (SerializationException ex)
                {
                }
            }
            #region Main
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
 
   
            MenuTypes selectedMenu;

            Author author;
            Book book;
            int id;
            string name;
            string surname;
            string bookName;

        #endregion

        l1:
            Console.WriteLine("Edeceyiniz emelyyati secin: ");
            selectedMenu = Helper.ReadEnum<MenuTypes>("========= MENU ========= ");

            switch (selectedMenu)
            {
                #region AuthorAdd
                case MenuTypes.AuthorAdd:

                    author = new Author(maxAuthorId);
                lName:
                    name = Helper.ReadString("Muelifin adini daxil edin: ");
                    if (Helper.IsNameOrSurname(name))
                    {
                        author.Name = name;
                    }
                    else
                    {
                        Console.WriteLine("Adin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lName;
                    }
                lSurname:
                    surname = Helper.ReadString("Muelifin soyadini daxil edin:  ");
                    if (Helper.IsNameOrSurname(surname))
                    {
                        author.Surname = surname;
                    }
                    else
                    {
                        Console.WriteLine("Soyadin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lSurname;
                    }
                    authormanager.Add(author);

                    Console.Clear();
                    goto l1;
                #endregion

                #region AuthorEdit
                case MenuTypes.AuthorEdit:

                    Console.WriteLine("Duzeltmek Istediyiniz Muellifi secin: ");
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine(item);
                    }

                lEditId:
                    id = Helper.ReadInt("Muellif id");
                    author = authormanager.Find(id);

                    if (author == null)
                    {
                        Console.WriteLine("ID-e gore istifadeci tapilmadi. Yeniden daxil edin");
                        goto lEditId;
                    }

                lEditName:
                    name = Helper.ReadString("Muelifin adini daxil edin: ");
                    if (Helper.IsNameOrSurname(name))
                    {
                        author.Name = name;
                    }
                    else
                    {
                        Console.WriteLine("Adin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lEditName;
                    }
                lEditSurname:
                    surname = Helper.ReadString("Muelifin soyadini daxil edin:  ");
                    if (Helper.IsNameOrSurname(surname))
                    {
                        author.Surname = surname;
                    }
                    else
                    {
                        Console.WriteLine("Soyadin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lEditSurname;
                    }
                    Console.WriteLine("Muellif ugurla deyisdirildi");
                    Console.Clear();
                    goto case MenuTypes.AuthorGetAll;
                #endregion

                #region AuthorRemove
                case MenuTypes.AuthorRemove:
                    Console.WriteLine("Silmek Istediyiniz Muellifi secin: ");
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine(item);
                    }
                    id = Helper.ReadInt("Muellif id");
                    author = authormanager.Find(id);
                    if (author == null)
                    {
                        Console.Clear();
                        goto case MenuTypes.AuthorEdit;
                    }
                    authormanager.Remove(author);
                    Console.Clear();
                    Console.WriteLine("======= Authors =======");
                    goto case MenuTypes.AuthorGetAll;
                #endregion

                #region AuthorGetAll
                case MenuTypes.AuthorGetAll:
                    Console.Clear();
                    Console.WriteLine(" ======= Authors ======= ");
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                #endregion

                #region AuthorGetById
                case MenuTypes.AuthorGetById:
                    id = Helper.ReadInt("Muellif id");
                    author = authormanager.Find(id);

                    if (id == 0)
                        goto l1;

                    if (author == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Tapilmadi...");
                        goto case MenuTypes.AuthorGetById;
                    }
                    Console.WriteLine(author);
                    goto l1;
                #endregion

                #region AuthorFindByName
                case MenuTypes.AuthorFindByName:
                    name = Helper.ReadString("Axtaris ucun min. 3 herf qeyd edin:  ");
                    var data = authormanager.Where(x => x.Name == name);
                    if (data == null)
                    {
                        Console.WriteLine("Tapilmadi");
                    }
                    foreach (var item in data)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                #endregion

                #region BookAdd
                case MenuTypes.BookAdd:
                t1:
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine(item);
                    }
                    int authorid = Helper.ReadInt("Muellif id daxil edin: ");
                    author = authormanager.Find(authorid);
                    if (author==null)
                    {
                        goto t1;
                    }
                     book = new Book(maxBookId);
                lBName:
                    bookName = Helper.ReadString("Kitabin adini daxil edin: ");
                    book.AuthorId = authorid;
                    if (Helper.IsNameOrSurname(bookName))
                    {
                        book.Name = bookName;
                    }
                    else
                    {
                        Console.WriteLine("Kitabin adinda reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lBName;
                    }
                    Console.Clear();
                    book.PageCount = Helper.ReadInt("Kitabin Seyfe sayini daxil edin: ");
                    Console.Clear();
                    book.GenrEnum = Book.ReadGenre("Kitabin Janrini secin");
                    Console.Clear();
                    book.Price = Helper.ReadInt("Kitabin Qiymetini daxil ");
                    Console.Clear();
                    bookmanager.Add(book);
                    Console.Clear();
                    goto case MenuTypes.BookGetAll;

                #endregion

                #region BookEdit
                case MenuTypes.BookEdit:
                    Console.WriteLine("Duzeltmek Istediyiniz Kitabi secin: ");
                    foreach (var item in bookmanager)
                    {
                        Console.WriteLine(item);
                    }

                lBEditId:
                    id = Helper.ReadInt("Book id");
                    book = bookmanager.Find(id);

                    if (book == null)
                    {
                        Console.WriteLine("ID-e gore Kitab tapilmadi. Yeniden daxil edin");
                        goto lBEditId;
                    }

                lBEditName:
                    bookName = Helper.ReadString("Kitabin adini daxil edin: ");
                    if (Helper.IsNameOrSurname(bookName))
                    {
                        book.Name = bookName;
                    }
                    else
                    {
                        Console.WriteLine("Kitabin adinda reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lBEditName;
                    }
                    Console.WriteLine("Kitab ugurla deyisdirildi");
                    Console.Clear();
                    book.PageCount = Helper.ReadInt("Kitabin Seyfe sayini daxil edin: ");
                    Console.Clear();
                    book.GenrEnum = Book.ReadGenre("Kitabin Janrini secin");
                    Console.Clear();
                    book.Price = Helper.ReadInt("Kitabin Qiymetini daxil ");
                    Console.Clear();
                    bookmanager.Add(book);
                    Console.Clear();
                    goto case MenuTypes.BookGetAll;
                #endregion

                #region BookRemove
                case MenuTypes.BookRemove:
                    Console.WriteLine("Silmek Istediyiniz Muellifi secin: ");
                    foreach (var item in bookmanager)
                    {
                        Console.WriteLine(item);
                    }
                    id = Helper.ReadInt("Kitab id");
                    book = bookmanager.Find(id);
                    if (book == null)
                    {
                        Console.Clear();
                        goto case MenuTypes.AuthorEdit;
                    }
                    bookmanager.Remove(book);
                    Console.Clear();
                    Console.WriteLine("======= Authors =======");
                    goto case MenuTypes.BookGetAll;
                #endregion

                #region BookGetAll
                case MenuTypes.BookGetAll:
                    Console.Clear();
                    Console.WriteLine(" ======= Books ======= ");
                    foreach (var item  in bookmanager)
                    {
                        author = authormanager.Find(item.AuthorId);
                        Console.WriteLine($"{item} | Muellifin Adi: {author.Name} |\n | Muellifin Soyadi: {author.Surname} |");
                        Console.WriteLine(" ====================");
                    }
                    Console.WriteLine(" ============ ==== ============ ");
                    goto l1;
                #endregion

                #region BookGetById
                case MenuTypes.BookGetById:
                    id = Helper.ReadInt("Kitab id");
                    book = bookmanager.Find(id);

                    if (id == 0)
                        goto l1;

                    if (book == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Tapilmadi...");
                        goto case MenuTypes.BookGetById;
                    }
                    Console.WriteLine(book);
                    goto l1;
                #endregion

                #region BookFindByName
                case MenuTypes.BookFindByName:
                    bookName = Helper.ReadString("Axtaris ucun min. 3 herf qeyd edin:  ");
                    var dataBook = bookmanager.Where(x => x.Name == bookName);
                    if (dataBook == null)
                    {
                        Console.WriteLine("Tapilmadi");
                    }
                    foreach (var item in dataBook)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                #endregion

                #region SaveAndExit
                case MenuTypes.SaveAndExit:

                    Database db = new Database();
                    db.Authors = authormanager;
                    db.Books = bookmanager;

                    FileStream fileStream = File.Create(database);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fileStream, db);
                    fileStream.Flush();
                    fileStream.Close();
                    Console.WriteLine("Yaddasda Saxlanildi. Cixis edin.");
                    break;
                #endregion

                default:
                    break;
            }
        }

    }
}