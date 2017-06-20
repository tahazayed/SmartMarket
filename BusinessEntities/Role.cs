namespace BusinessEntities
{

    public class Role
    {
        public long Id { get; set; }
        public string Roles { get; set; }

        public bool IsSystem { get; set; }

        public Role()
        {
            IsSystem = false;
        }
    }
}