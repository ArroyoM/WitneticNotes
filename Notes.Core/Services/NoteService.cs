using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;
using Notes.Core.Interfaces;
using Notes.Core.Interfaces.IServices;

namespace Notes.Core.Services
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Note> GetNotes(int idBook)
        {
            try
            {
                return _unitOfWork.NoteRespository.GetAll(idBook);
            }
            catch (Exception)
            {
                throw new Exception("Error in server get all note ");
            }
        }
        public async Task<Note> GetNote(int id)
        {
            try
            {
                return await _unitOfWork.NoteRespository.GetById(id);
            }catch(Exception)
            {
                throw new Exception("Error in server get note ");
            }
        }
        public async Task InsertNote(Note note)
        {
            try
            {
                note.Created_time = DateTime.Now;
                note.Updated_time = DateTime.Now;
                await _unitOfWork.NoteRespository.Add(note);
                await _unitOfWork.SaveChangesAsysc();

            }catch(Exception ex)
            {
                throw new Exception("Error in insert note ");
            }
        }

        public async Task<bool> UpdateNote(Note note)
        {
            try
            {
                var exitingNote =  await _unitOfWork.NoteRespository.GetById(note.IdNote);

                if (exitingNote == null)
                {
                    return false;
                }
                exitingNote.Name = note.Name;
          
                _unitOfWork.NoteRespository.Update(exitingNote);
                await _unitOfWork.SaveChangesAsysc();
                return true;

            }catch(Exception ex)
            {
                throw new Exception("Error in server update note");
            }
        }
        public async Task<bool> DeleteNote(int id)
        {
            try
            {
                await _unitOfWork.NoteRespository.Delete(id);
                await _unitOfWork.SaveChangesAsysc();
                return true;

            }catch(Exception ex)
            {
                throw new Exception("Error in server delete note");
            }
        }
    }
}
