using CRM.Models;

namespace CRM.Strategy
{
    public interface IStatusStudent
    {
        void CreatePeriod(Student student);
        void CreateComment(Student student);
    }
}