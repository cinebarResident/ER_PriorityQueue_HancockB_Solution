namespace ER_Priority_Queue_Hancock_Project
{
    internal class Patient
    {
        private string _firstName;
        private string _lastName;
        private DateOnly _dateOfBirth;
        private int _priority;
        private DateTime _arrivalTime;

        public Patient(string FirstName, string LastName, DateTime DateOfBirth, int Priority, DateTime ArrivalTime)
        {
            _firstName = FirstName;
            _lastName = LastName;
            decimal age = (DateTime.Now - DateOfBirth).Days / 365;
            if (age < 21 || age >= 65)
            {
                Priority = 1;
            }
            DateOnly dateOfBirth = DateOnly.FromDateTime(DateOfBirth);
            _dateOfBirth = dateOfBirth;
            _priority = Priority;
            _arrivalTime = ArrivalTime;
        }

        public int Priority
        {
            get { return _priority; }
        }
        public DateTime ArrivalTime
        {
            get { return _arrivalTime; }
        }

        public override string ToString()
        {
            return String.Format("{0,-15} {1,-20} {2,10:MM/dd/yyyy} {3,8}", _firstName, _lastName, _dateOfBirth, _priority);
        }
    }
}