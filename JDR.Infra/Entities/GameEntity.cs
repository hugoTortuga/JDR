using JDR.Core;
using JDR.Model;
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

        public Game ToGame(IImageStorage imageUploader, IMusicStorage musicStorage)
        {
            var game = new Game
            {
                MaxPlayer = MaxPlayer,
                Name = Name,
                Scenes = Scenes.Select(s => s.ToScene(imageUploader, musicStorage)).ToList(),
            };
            return game;
        }

    }
}
