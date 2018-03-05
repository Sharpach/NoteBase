using AutoMapper;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.PL.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRO.Task.NoteBase.PL.WebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();

            CreateMap<Note, NoteViewModel>();
            CreateMap<NoteViewModel, Note>();
        }
    }
}
