using Microsoft.AspNetCore.Http;
using System;

namespace Book_Borrowing_System.Service
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        void RemoveImage(string fileName);
    }
}