namespace JDR.Service
{
    public class ServiceBase
    {
        private readonly IBaseRepository _Repository;
        public ServiceBase(IBaseRepository repo) 
        {
			_Repository = repo;

		}

        public void Save(object obj) {
			if (obj == null) return;
			_Repository.Save(obj);
		}

		public void Delete(object obj) {
			if (obj == null) return;
			_Repository.Delete(obj);
		}

	}
}