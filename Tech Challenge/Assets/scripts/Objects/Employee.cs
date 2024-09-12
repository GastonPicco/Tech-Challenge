namespace Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Seniority { get; set; }
        public string YearsInCompany { get; set; }

    
        public Employee()
        {

        }

   
        public Employee(int id, string firstName, string lastName, string position, string seniority, string yearsInCompany)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Seniority = seniority;
            YearsInCompany = yearsInCompany;
        }
    }
}