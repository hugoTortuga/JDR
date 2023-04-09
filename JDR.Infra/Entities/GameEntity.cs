using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities {

	[Table("game")]
	public class GameEntity {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int MaxPlayer { get; set; }
        public IList<SceneEntity> Scenes { get; set; }

        public GameEntity()
        {
            Scenes = new List<SceneEntity>();
        }

    }
}
