using AutoMapper;
using BookStoreLibrary.Entities.Models;
using BookStoreLibrary.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreLibrary.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API

            //Mapping Books with their Authors 
            CreateMap<Book, BookWithAuthorsResource>()
                .ForMember(br => br.AddedBy, opt => opt.MapFrom(b => b.User.UserName))
                .ForMember(br => br.Price, opt => opt.MapFrom(b => b.Price))
                .ForMember(br => br.Quantity, opt => opt.MapFrom(b => b.Quantity))
                .ForMember(br => br.Authors,
                opt => opt.MapFrom(b =>
                b.Authors.Select(ba => new IdNamePair { ID = ba.Author.ID, Name = ba.Author.Name })));

            //Mapping Authors with their Books
            CreateMap<Author, AuthorWithBooksResource>()
                .ForMember(br => br.AddedBy, opt => opt.MapFrom(b => b.User.UserName))
                .ForMember(br => br.Books,
                opt => opt.MapFrom(a =>
                a.Books.Select(ba => new BookResource
                { ID = ba.Book.ID, Name = ba.Book.Name, Price = ba.Book.Price })));

            CreateMap<Invoice, InvoiceResource>()
                .ForMember(ir => ir.AddedBy, opt => opt.MapFrom(i => i.User.UserName))
                .ForMember(ir => ir.Books,
                opt => opt.MapFrom(i => i.Books.Select(ib => new InvoiceBookResource
                {
                    BookID = ib.BookID,
                    BookName = ib.Book.Name,
                    BookPrice = ib.BookPrice,
                    NumberOfItems = ib.NumberOfItems,
                    BookDiscount = ib.BookDiscount,
                    TotalPrice = ib.TotalPrice
                })));

            //API to Domain
            CreateMap<AuthorSaveResource, Author>()
                .ForMember(a => a.ID, opt => opt.Ignore())
                .ForMember(a => a.Books, opt => opt.Ignore())
                .AfterMap((ar, a) =>
                {
                    var removedBooks = a.Books.Where(b => !ar.Books.Contains(b.BookID)).ToList();
                    foreach (var b in removedBooks)
                        a.Books.Remove(b);

                    var addedBooks = ar.Books.Where(bId => !a.Books.Any(b => b.BookID == bId))
                    .Select(bId => new AuthorBooks { BookID = bId }).ToList();
                    foreach (var b in addedBooks)
                        a.Books.Add(b);
                });

            CreateMap<BookSaveResource, Book>()
                .ForMember(b => b.ID, opt => opt.Ignore())
                .ForMember(b => b.Price, opt => opt.MapFrom(br => br.Price))
                .ForMember(b => b.Quantity, opt => opt.MapFrom(br => br.Quantity))
                .ForMember(b => b.Authors, opt => opt.Ignore())
                .AfterMap((br, b) =>
                {
                    var removedAuthors = b.Authors.Where(b => !br.Authors.Contains(b.AuthorID)).ToList();
                    foreach (var a in removedAuthors)
                        b.Authors.Remove(a);

                    var addedAuthors = br.Authors.Where(aId => !b.Authors.Any(b => b.AuthorID == aId))
                    .Select(aId => new AuthorBooks { AuthorID = aId }).ToList();
                    foreach (var a in addedAuthors)
                        b.Authors.Add(a);
                });

            CreateMap<InvoiceSaveResource, Invoice>()
                .ForMember(i => i.ID, opt => opt.Ignore())
                .ForMember(i => i.Price, opt => opt.MapFrom(isr => isr.Price))
                .ForMember(i => i.Discount, opt => opt.MapFrom(isr => isr.Discount))
                .ForMember(i => i.TotalPrice, opt => opt.MapFrom(isr => isr.TotalPrice))
                .ForMember(i => i.Books, opt => opt.MapFrom(ir => ir.Books.Select(ibr => new InvoiceBooks
                {
                    BookID = ibr.BookID,
                    BookPrice = ibr.BookPrice,
                    BookDiscount = ibr.BookDiscount,
                    NumberOfItems = ibr.NumberOfItems,
                    TotalPrice = ibr.TotalPrice
                })));
        }
    }
}
