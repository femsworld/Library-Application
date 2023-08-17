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

        public IEnumerable<Loan> GetAllLoans()
        {
            return _loanRepo.GetAllLoans();
        }

        public IEnumerable<Loan> GetLoansByUserId(Guid userId)
        {
            return _loanRepo.GetLoansByUserId(userId);
        }

        public Loan PlaceLoan(Guid userId, LoanDto loanDto)
        {
            var loan = new Loan { UserId = loanDto.UserId };
            _loanRepo.PlaceLoan(loan);

            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanDto.LoanBooks);
            foreach (var book in loanBooks)
            {
                book.LoanId = loan.Id;
            }
            var createdLoanBooks = _loanBookRepo.CreateLoanBook(loanBooks.ToArray());
            loan.LoanBooks = createdLoanBooks.ToList();

            return loan;
        }

        public void ReturnLoan(Guid loanId)
        {
            var loan = _loanRepo.GetLoanById(loanId);
            if (loan == null)
            {
                return;
            }

            // var returnedBooks = loan.LoanBooks.ToList();
            var returnedBooks = loan.LoanBooks ?? new List<LoanBook>();
            _loanBookRepo.DeleteLoanBooks(returnedBooks);

            _loanRepo.RemoveLoan(loan);

            var returnedBookTitles = returnedBooks.Select(b => b.Book.Title).ToList();
        }
    }
}