using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class LoanBookService : ILoanBookService
    {   
        private readonly ILoanBookRepo  _loanBookRepo;
        private readonly IMapper _mapper;

        public LoanBookService(ILoanBookRepo loanBookRepo, IMapper mapper)
        {
            _loanBookRepo = loanBookRepo;
            _mapper = mapper;
        }

        public async Task DeleteLoanBooksAsync(IEnumerable<LoanBook> loanBooks)
        {
            await _loanBookRepo.DeleteLoanBooksAsync(loanBooks);
        }

        public async Task<IEnumerable<LoanBook>> CreateLoanBookAsync(params LoanBookDto[] loanBookDto)
        {
            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanBookDto);
            return await _loanBookRepo.CreateLoanBookAsync(loanBooks.ToArray());
        }
    }
}
