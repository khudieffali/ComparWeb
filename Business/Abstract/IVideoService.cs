using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVideoService
    {
        Task<List<Video>> GetVideos();
        Task<Video> GetByIdVideos(int? id);
        Task AddVideos(Video video);
        Task UpdateVideos(Video video);
    }
}
