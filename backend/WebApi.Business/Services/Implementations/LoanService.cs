using AutoMapper;
using WebApi.Business.Dto;
using WebApi.Business.RepoAbstractions;
using WebApi.Business.Services.Abstractions;
using WebApi.Domain.Entities;

namespace WebApi.Business.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepo _loanRepo;
        private readonly ILoanBookRepo _loanBookRepo;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepo loanRepo, ILoanBookRepo loanBookRepo, IMapper mapper)
        {
            _loanRepo = loanRepo;
            _loanBookRepo = loanBookRepo;
            _mapper = mapper;
        }
        
        public Loan PlaceLoan(Guid userId, IEnumerable<LoanBookDto> loanBookDtos)
        {
            var loan = new Loan {UserId = userId};
            _loanRepo.PlaceLoan(loan);
            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanBookDtos);
            var newList = new List<LoanBook>();
            foreach (var book in loanBooks)
            {
                book.LoanId = loan.Id;
                newList.Add(book);
            }
           var creatededLoanBooks =  _loanBookRepo.CreateLoanBook(newList.ToArray());
        //    var loan = new Loan{UserId = userId, LoanBooks = loanBooks.ToList()};
            loan.LoanBooks = creatededLoanBooks.ToList();
        //    _loanRepo.PlaceLoan(loan);
           return loan;
        }
    }
}