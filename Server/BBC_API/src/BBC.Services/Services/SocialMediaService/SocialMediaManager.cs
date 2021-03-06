﻿using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.LobiService.Dto;
using BBC.Services.Services.SocialMediaService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.SocialMediaService
{
    public class SocialMediaManager : BaseService, ISocialMediaService
    {
        private readonly IRepositoryBase<BBCContext, SocialMedia, int> _socialMediaRepository;

        public SocialMediaManager(IRepositoryBase<BBCContext, SocialMedia, int> socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task CreateSocialMedia(CreateSocialMediaDto input)
        {
            var socialMedia = _mapper.Map<SocialMedia>(input);
            await _socialMediaRepository.InsertAsync(socialMedia);
        }

        public async Task DeleteSocialMedia(int Id)
        {
            await _socialMediaRepository.DeleteAsync(Id);
        }

        public async Task EditSocialMedia(EditSocialMediaDto input)
        {
            var socialMedia = await _socialMediaRepository.FindAsync(input.Id);
            _mapper.Map(socialMedia, input);
            await _socialMediaRepository.UpdateAsync(socialMedia);
        }

        public async Task<List<SocialMediaDto>> GetAllSocialMedias()
        {
            var socialMedias = await _socialMediaRepository.GetListAsync();
            var result = _mapper.Map<List<SocialMediaDto>>(socialMedias);
            return result;
        }

        public async Task<EditSocialMediaDto> GetSocialMedia(int Id)
        {
            var socialMedia = await _socialMediaRepository.GetAsync(Id);
            var result = _mapper.Map<EditSocialMediaDto>(socialMedia);
            return result;
        }

        public async Task<EditSocialMediaDto> GetSocialMediaForEdit(int socialMediaId)
        {
            EditSocialMediaDto result = new EditSocialMediaDto();
            var data = await _socialMediaRepository.GetQueryable()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == socialMediaId);
            if(data != null)
            {
                result.Id = data.Id;
                result.FacebookUrl = data.FacebookUrl;
                result.InstagramUrl = data.InstagramUrl;
                result.TwitterUrl = data.TwitterUrl;
            }
            return result;
        }
    }
}
