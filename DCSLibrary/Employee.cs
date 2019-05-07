namespace DCSLibrary
{
    public class Employee
    {
        protected int id;
        protected string name;
        protected string store;
        protected EmployeeRank title;

        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Store
        {
            get { return store; }
        }
        public EmployeeRank Title
        {
            get { return title; }
        }

    }
}
