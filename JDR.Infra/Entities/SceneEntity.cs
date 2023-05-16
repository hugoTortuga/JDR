using JDR.Core;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities {

	[Table("scene")]
	public class SceneEntity {

		public Guid Id { get; set; }
		public string? Name { get; set; }
        public double ZoomValue { get; set; }
        public int XMapTranslation { get; set; }
        public int YMapTranslation { get; set; }
        public bool HasFogOfWarEnable { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public IList<Obstacle> Obstacles { get; set; }
        public IList<MusicEntity> Musics { get; set; }
        public string? BackgroundImage { get; set; }

        public SceneEntity()
        {
            Obstacles = new List<Obstacle>();
            Musics = new List<MusicEntity>();
        }

		public Scene ToScene(IImageStorage imageUploader, IMusicStorage musicStorage)
		{
			return new Scene(Name ?? "")
			{
				Obstacles = Obstacles,
                Musics = Musics.Select(m => m.ToMusic(musicStorage)).ToList(),
				Background = imageUploader.Get(BackgroundImage),
                ZoomValue = ZoomValue, 
				XMapTranslation = XMapTranslation, 
				YMapTranslation = YMapTranslation,
				HasFogOfWarEnable = HasFogOfWarEnable,
				Height = Height,
				Width = Width
            };
        }

		public static SceneEntity ToSceneEntity(Scene scene)
		{
            return new SceneEntity
            {
                BackgroundImage = scene.Background?.Name + scene.Background?.Extension,
                Name = scene.Name,
                Obstacles = scene.Obstacles,
                Musics = scene.Musics.Select(m => MusicEntity.ToMusicEntity(m)).ToList(),
                XMapTranslation = scene.XMapTranslation,
                YMapTranslation = scene.YMapTranslation,
                ZoomValue = scene.ZoomValue,
                HasFogOfWarEnable = scene.HasFogOfWarEnable,
                Height = scene.Height,
                Width = scene.Width
            };
        }
    }
}
