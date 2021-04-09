using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using PaymentAPI.Models.NewFolder;
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

        public IEnumerable<PaymentDetails> GetPaymentByResidentId(int id)
        {
            List<Payments> data = _context.Payments.Include(emp => emp.Employee).Include(serv=> serv.Service).ToList();

            List<PaymentDetails> paymentDetailsList = new List<PaymentDetails>();
            foreach (var ser in data)
            {
                if (ser.ResidentId == id)
                {

                    PaymentDetails temppaymentedetails = new PaymentDetails()
                    {
                        PaymentId = ser.PaymentId,
                        PaymentFor = ser.PaymentFor,
                         Amount = ser.Amount,
                        ResidentId = ser.ResidentId,
                        EmployeeId = ser.EmployeeId,
                        EmployeeName = ser.Employee.EmployeeName,
                        PaymentStatus = ser.PaymentStatus,
                        ServiceId = ser.ServiceId,
                        ServiceType = ser.Service.ServiceType
                    };
                    paymentDetailsList.Add(temppaymentedetails);
                }
            }
            return paymentDetailsList;
   
        }

        public Payments GetPaymentByServiceID(int id)
        {
            return _context.Payments.FirstOrDefault(service => service.ServiceId == id);
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
                payment = new Payments() { PaymentFor = item.PaymentFor,Amount = item.Amount, ResidentId = item.ResidentId, EmployeeId = item.EmployeeId,PaymentStatus = "Requested", ServiceId = item.ServiceId};
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
