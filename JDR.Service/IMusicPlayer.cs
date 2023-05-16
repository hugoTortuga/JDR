using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core
{
    public interface IMusicPlayer
    {
        void SetMusic(Music music);
        void Play();
        void Pause();
        void Stop();
        void SetVolume(float volume);
    }
}
