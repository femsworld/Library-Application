using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public Loan PlaceLoan(Guid userId, IEnumerable<LoanBook> loanBookDtos)
        {
           var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanBookDtos);
           var creatededLoanBooks =  _loanBookRepo.CreateLoanBook(loanBooks.ToArray());
           var loan = new Loan{UserId = userId, LoanBooks = loanBooks.ToList()};
           _loanRepo.PlaceLoan(loan);
           return loan;
            
        }
    }
}