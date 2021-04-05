using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Repository
{
    public class PayRepos : IPayRepos
    {
        private readonly CommunityGateDatabaseContext _context;

        public PayRepos()
        {

        }

        public PayRepos(CommunityGateDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Payments> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payments GetPaymentsbyId(int id)
        {
            Payments item = _context.Payments.Find(id);
            return item;
        }

        public IEnumerable<Payments> GetPaymentByResidentId(int id)
        {
            var item = _context.Payments.Where(t => t.ResidentId == id && t.PaymentStatus == "Requested").ToList();
            return item;
        }

        public async Task<Payments> PostPayments(Payments item)
        {
            Payments payment = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                payment = new Payments() { PaymentFor = item.PaymentFor,Amount = item.Amount, ResidentId = item.ResidentId, EmployeeId = item.EmployeeId,PaymentStatus = item.PaymentStatus, ServiceId = item.ServiceId};
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
            }
            return payment;
        }

        public async Task<Payments> UpdatePayment(Payments item, int id)
        {
            Payments payment = await _context.Payments.FindAsync(id);
            payment.PaymentStatus = item.PaymentStatus;
            await _context.SaveChangesAsync();
            return payment;
        }


    }
}
