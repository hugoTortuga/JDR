using JDR.Model;

namespace JDR.Service
{
    public class ServiceBase
    {
        private readonly IGameRepository _Repository;
        public ServiceBase(IGameRepository repo) 
        {
			_Repository = repo;

		}

        public void Save(Game game) {
			if (game == null) return;
			_Repository.Save(game);
		}

		public void Delete(Game game) {
			if (game == null) return;
			_Repository.Delete(game);
		}

	}
}