namespace DataLayer.Crud
{
    public abstract class BaseCrud
    {
        protected readonly SqlDataAccess SqlDataAccess;

        protected BaseCrud()
        {
            SqlDataAccess = new SqlDataAccess();
        }
    }
}