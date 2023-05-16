using JDR.Core;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities
{
    [Table("music")]
    public class MusicEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public int DurationInSeconds { get; set; }

        public MusicEntity()
        {
        }

        public Music ToMusic(IMusicStorage musicStorage)
        {
            return musicStorage.Get(Path);
        }

        public static MusicEntity ToMusicEntity(Music music)
        {
            return new MusicEntity
            {
                DurationInSeconds = music.DurationInSecond,
                Name = music.Name,
                Path = music.Path
            };
        }
    }
}
