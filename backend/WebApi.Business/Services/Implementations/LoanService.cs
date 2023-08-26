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
            var loan = new Loan { UserId = loanDto.UserId };
            await _loanRepo.PlaceLoanAsync(loan);

            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanDto.LoanBooks);
            foreach (var book in loanBooks)
            {
                book.LoanId = loan.Id;
            }
            var createdLoanBooks = await _loanBookRepo.CreateLoanBookAsync(loanBooks.ToArray());
            loan.LoanBooks = createdLoanBooks.ToList();

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
