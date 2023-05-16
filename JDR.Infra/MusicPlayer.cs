using JDR.Core;
using JDR.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra
{
    public class MusicPlayer : IMusicPlayer
    {
        private byte[] MusicData;
        private MemoryStream MusicStream;
        private Mp3FileReader Mp3Reader;
        private WaveOutEvent OutputDevice;
        private bool DisposedValue;

        public double CurrentVolume
        {
            get
            {
                return OutputDevice?.Volume ?? 0;
            }
            set
            {
                if (OutputDevice != null)
                {
                    OutputDevice.Volume = (float)value;
                }
            }
        }

        public void SetMusic(Music music)
        {
            if (music == null || music.Content == null) return;
            CurrentVolume = 0.2f;

            MusicData = music.Content;
            MusicStream = new MemoryStream(MusicData);
            Mp3Reader = new Mp3FileReader(MusicStream);
            OutputDevice = new WaveOutEvent();
            OutputDevice.Init(Mp3Reader);
        }

        public void Play()
        {
            OutputDevice.Play();
        }

        public void Pause()
        {
            OutputDevice.Pause();
        }

        public void Stop()
        {
            OutputDevice?.Stop();
        }

        public void SetVolume(float volume)
        {
            CurrentVolume = volume;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    Mp3Reader?.Dispose();
                    OutputDevice?.Dispose();
                }

                MusicData = null;
                MusicStream = null;
                Mp3Reader = null;
                OutputDevice = null;

                DisposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
