using ConsoleAppFinallyProject.Data;
using ConsoleAppFinallyProject.Enums;
using ConsoleAppFinallyProject.Helpers;
using ConsoleAppFinallyProject.Managers;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppFinallyProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
        #region Main
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            AuthorManager authormanager = new AuthorManager();
            BookManager bookmanager = new BookManager();

            MenuTypes selectedMenu;

            Author author;
            Book book;
            int id;
            string name;
            string surname;
            //==============
            string bookName;
           
        #endregion

        l1:
            Console.WriteLine("Edeceyiniz emelyyati secin: ");
            selectedMenu = Helper.ReadEnum<MenuTypes>("========= MENU ========= ");

            switch (selectedMenu)
            {
                #region AuthorAdd
                case MenuTypes.AuthorAdd:

                    author = new Author();
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
                    author = authormanager.GetById(id);

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
                    author = authormanager.GetById(id);
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
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                #endregion

                #region AuthorGetById
                case MenuTypes.AuthorGetById:
                    id = Helper.ReadInt("Muellif id");
                    author = authormanager.GetById(id);

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

                #region AuthorFindById
                case MenuTypes.AuthorFindById:
                    name = Helper.ReadString("Axtaris ucun min. 3 herf qeyd edin:  ");
                    var data = authormanager.FindByName(name);
                    if (data.Length == 0)
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
                    author = new Author();
                lBBName:
                    name = Helper.ReadString("Muelifin adini daxil edin: ");
                    if (Helper.IsNameOrSurname(name))
                    {
                        author.Name = name;
                    }
                    else
                    {
                        Console.WriteLine("Adin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lBBName;
                    }
                    Console.Clear();
                lBBSurname:
                    surname = Helper.ReadString("Muelifin soyadini daxil edin:  ");
                    if (Helper.IsNameOrSurname(surname))
                    {
                        author.Surname = surname;
                    }
                    else
                    {
                        Console.WriteLine("Soyadin icersinde reqem, simvol  olmamalidir ve min 3 herf olmalidir");
                        goto lBBSurname;
                    }
                    authormanager.Add(author);
                    Console.Clear();

                    //===========================================================================================
                    book = new Book();
                lBName:
                    bookName = Helper.ReadString("Kitabin adini daxil edin: ");
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
                    Console.WriteLine("Duzeltmek Istediyiniz Muellifi secin: ");
                    foreach (var item in bookmanager)
                    {
                        Console.WriteLine(item);
                    }

                lBEditId:
                    id = Helper.ReadInt("Book id");
                    book = bookmanager.GetById(id);

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
                    book = bookmanager.GetById(id);
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
                    foreach (var item in authormanager)
                    {
                        Console.WriteLine($" | Muellif | Id: {item}");
                    }
                    foreach (var item in bookmanager)
                    {
                        author = authormanager.GetById(item.AuthorId);
                        Console.WriteLine(item);
                        Console.WriteLine(" ====================");
                    }
                    Console.WriteLine(" ============ ==== ============ ");
                    goto l1;
                #endregion

                #region BookGetById
                case MenuTypes.BookGetById:
                    id = Helper.ReadInt("Kitab id");
                    book = bookmanager.GetById(id);

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

                #region BookFindById
                case MenuTypes.BookFindById:
                    bookName = Helper.ReadString("Axtaris ucun min. 3 herf qeyd edin:  ");
                    var dataBook = bookmanager.FindByName(bookName);
                    if (dataBook.Length == 0)
                    {
                        Console.WriteLine("Tapilmadi");
                    }
                    foreach (var item in dataBook)
                    {
                        Console.WriteLine(item);
                    }
                    goto l1;
                #endregion

                default:
                    break;
            }
        }

    }
}