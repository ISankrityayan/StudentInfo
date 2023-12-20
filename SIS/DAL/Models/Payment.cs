using System;
namespace Application
{
	public class Payment
	{
        public int PaymentID { get; set; }
        public int StudentID { get; set; } 
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            StudentID = student.StudentID;
            Amount = amount;
            PaymentDate = paymentDate;
            student.MakePayment(amount, paymentDate);
        }
    }
	
}

