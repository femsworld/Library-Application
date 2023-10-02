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
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepo loanRepo, ILoanBookRepo loanBookRepo, IBookRepo bookRepo, IMapper mapper)
        {
            _loanRepo = loanRepo;
            _loanBookRepo = loanBookRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepo.GetAllLoansAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _loanRepo.GetLoansByUserIdAsync(userId);
        }

        public async Task<Loan> PlaceLoanAsync(Guid userId, LoanDto loanDto)
        {
            var loan = new Loan { UserId = userId };
            var loanBooks = new List<LoanBook>();

            foreach (var loanBookDto in loanDto.LoanBooks)
            {
                var book = await _bookRepo.GetBookByIdAsync(loanBookDto.BookId);
                
                if (book != null)
                {
                    var loanBook = new LoanBook
                    {
                        BookId = loanBookDto.BookId,
                        Amount = loanBookDto.Amount, 
                        Book = book,
                        BookTitle = book.Title
                    };

                    loanBooks.Add(loanBook);
                }
            }

            loan.LoanBooks = loanBooks;

            await _loanRepo.PlaceLoanAsync(loan);
            return loan;
        }

        public async Task ReturnLoanAsync(Guid loanId)
        {
            var loan = await _loanRepo.GetLoanByIdAsync(loanId);
            if (loan == null)
            {
                return;
            }

            var returnedBooks = loan.LoanBooks ?? new List<LoanBook>();
            await _loanBookRepo.DeleteLoanBooksAsync(returnedBooks);

            await _loanRepo.RemoveLoanAsync(loan);
        }
    }
}
