using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Application.Cards.DTOs;

namespace VipManagement.Application.Cards
{
    public interface ICardRepository
    {

        Task<CreateCardInputDto> GetCardByIdAsync(int id);
    }
}
