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

        public void DeleteLoanBooks(IEnumerable<LoanBook> loanBooks)
        {
            // throw new NotImplementedException();
            _loanBookRepo.DeleteLoanBooks(loanBooks);
            
        }

        IEnumerable<LoanBook> ILoanBookService.CreateLoanBook(params LoanBookDto[] loanBookDto)
        {
            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanBookDto);
            return _loanBookRepo.CreateLoanBook(loanBooks.ToArray());
        }
        
    }
}