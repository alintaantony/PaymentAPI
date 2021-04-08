using PaymentAPI.Models;
using PaymentAPI.Models.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Repository
{
    public interface IPayRepos
    {
        IEnumerable<Payments> GetAllPayments();
        Payments GetPaymentsbyId(int id);
        Task<Payments> PostPayments(Payments item);
        Task<Payments> UpdatePayment(Payments item, int id);
        IEnumerable<PaymentDetails> GetPaymentByResidentId(int id);

    }
}
