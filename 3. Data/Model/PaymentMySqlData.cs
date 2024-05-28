using _3._Data.Context;
using _3._Data.Model;
using Microsoft.EntityFrameworkCore;

namespace _3._Data;

public class PaymentMySqlData: IPaymentData
{
    private StudyMentorDB _studyMentorDb;

    public PaymentMySqlData(StudyMentorDB studyMentorDb)
    {
        _studyMentorDb = studyMentorDb;
    }
    public Payment GetById(int id)
    {
        // DB-TABLA-
        return _studyMentorDb.Payments.Where(t => t.StudentId == id && t.IsActive).First();
    }
    public async Task<List<Payment>> GetAllAsync()
    {
        return await _studyMentorDb.Payments.Where(t=>t.IsActive).ToListAsync();
    }

    public Payment GetByCardNumber(string cardNumber)
    {
        return _studyMentorDb.Payments.Where(t => t.CardNumber==cardNumber && t.IsActive).FirstOrDefault();
    }

    public bool Create(Payment payment)
    {
        try
        {
            _studyMentorDb.Payments.Add(payment);
            //OBLIGATARIO
            _studyMentorDb.SaveChanges();
            return true;
        }
        catch (Exception error)
        {
            //log
            return false;
        }
    }
}