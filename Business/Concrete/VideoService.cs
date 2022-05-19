using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VideoService : IVideoService
    {
        private readonly IVideoDal _dal;

        public VideoService(IVideoDal dal)
        {
            _dal = dal;
        }

        public async Task AddVideos(Video video)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            video.CreatedDate = custDateTime;
            _dal.Add(video);
        }

        public async Task<Video> GetByIdVideos(int? id)
        {
            return await _dal.GetByIdInclude(x => x.Id == id);
        }

        public async Task<List<Video>> GetVideos()
        {
           return await _dal.GetAllInclude(x=>!x.IsDeleted);
        }

        public async Task UpdateVideos(Video video)
        {
           _dal.Update(video);
        }
    }
}
