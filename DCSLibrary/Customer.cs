namespace DCSLibrary
{
    public class Customer
    {
        public static int LOCAL_ID = 2;     //Customer to identify all from store
        private int id,
            age;
        private string fullName,
            username,
            address;

        public Customer(int id, int age, string fullName, string username, string address)
        {
            this.id = id;
            this.age = age;
            this.fullName = fullName;
            this.username = username;
            this.address = address;
        }

        public int Id
        {
            get { return id; }
        }
        public int Age
        {
            get { return age; }
        }
        public string FullName
        {
            get { return fullName; }
        }
        public string Username
        {
            get { return username; }
        }
        public string Address
        {
            get { return address; }
        }

    }
}
