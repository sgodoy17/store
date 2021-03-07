namespace StoreTest.Modules.Config.Entities
{
    public class ConfigEntity
    {
        protected string compamy;

        protected bool deploy;

        public ConfigEntity() { }

        public ConfigEntity(string compamy, bool deploy)
        {
            this.compamy = compamy;
            this.deploy = deploy;
        }

        public string Company
        {
            get { return compamy; }
            set { compamy = value; }
        }

        public bool Deploy
        {
            get { return deploy; }
            set { deploy = value; }
        }
    }
}
