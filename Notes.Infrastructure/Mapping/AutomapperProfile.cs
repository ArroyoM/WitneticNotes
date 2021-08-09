using AutoMapper;
using Notes.Core.DTOs;
using Notes.Core.Entities;

namespace Notes.Infrastructure.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserResponDTO>();
            CreateMap<UserResponDTO, User>();

            CreateMap< Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<Note, NoteDTO>();
            CreateMap<NoteDTO, Note>();

        }
    }
}
