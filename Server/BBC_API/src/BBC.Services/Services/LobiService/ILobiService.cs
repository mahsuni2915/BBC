﻿using BBC.Services.Services.Common.Base;
using BBC.Services.Services.LobiService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.LobiService
{
    public interface ILobiService : IBaseService
    {
        Task<List<LobiListDto>> GetAllLobis();
        Task<EditLobiDto> GetLobi(int Id);
        Task CreateLobi(CreateLobiDto input);
        Task EditLobi(EditLobiDto input);
        Task DeleteLobi(int Id);
    }
}
