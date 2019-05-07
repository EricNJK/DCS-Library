namespace DCSLibrary
{
    public class LocalManager : Employee
    {
        internal LocalManager(int id, string name, string store)
        {
            this.id = id;
            this.name = name;
            this.store = store;
            this.title = EmployeeRank.LOCAL_MANAGER;
        }

    }
}
