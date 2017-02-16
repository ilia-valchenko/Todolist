using System;
using System.Text;

namespace Infrastructure.Logger.Models
{
    public sealed class ErrorModel
    {
        public string Message { get; }
        public DateTime Date { get; }
        public string StackTrace { get; }

        public ErrorModel(string message, DateTime date) :this(message, date, String.Empty) { }

        public ErrorModel(string message, DateTime date, string stackTrace)
        {
            Message = message;
            Date = date;
            StackTrace = stackTrace;
        }

        public override string ToString()
        {
            StringBuilder stringRepresentation = new StringBuilder();
            stringRepresentation.Append(Environment.NewLine);
            stringRepresentation.Append("Date: ");
            stringRepresentation.Append(Date.ToString());
            stringRepresentation.Append(Environment.NewLine);
            stringRepresentation.Append("Error message: ");
            stringRepresentation.Append(Message);
            stringRepresentation.Append(Environment.NewLine);
            stringRepresentation.Append("StackTrace: ");
            stringRepresentation.Append(StackTrace);
            stringRepresentation.Append(Environment.NewLine);

            return stringRepresentation.ToString();
        }
    }
}
