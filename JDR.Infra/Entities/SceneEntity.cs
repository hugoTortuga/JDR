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

		public IList<Obstacle> Obstacles { get; set; }
		public string? BackgroundImage { get; set; }

		public SceneEntity() { 
			Obstacles = new List<Obstacle>();
		}	

		public Scene ToScene(IImageUploader imageUploader)
		{
			return new Scene(Name)
			{
				Obstacles = Obstacles,
				Background = imageUploader.Get(BackgroundImage)
			};
        }
    }
}
