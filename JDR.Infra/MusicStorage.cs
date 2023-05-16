using JDR.Core;
using JDR.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra
{
    public class MusicStorage : IMusicStorage
    {
        private string _BasePath;
        public MusicStorage(string basePath) {
            _BasePath = basePath;
        }
        public Music Get(string? path)
        {
            var fileInfo = new FileInfo(_BasePath + path);
            if (!fileInfo.Exists) throw new FileNotFoundException();

            using var audioFile = new AudioFileReader(fileInfo.FullName);
            int durationTime = (int)audioFile.TotalTime.TotalSeconds;
            return new Music
            {
                Content = File.ReadAllBytes(_BasePath + path),
                Name = fileInfo.Name[..^fileInfo.Extension.Length],
                DurationInSecond = durationTime
            };
        }

        public async Task Upload(byte[] fileContent, string path)
        {
            if (fileContent == null || fileContent.Length == 0) throw new ArgumentNullException("Music content is empty");
            File.WriteAllBytes(_BasePath + path, fileContent);
        }

        public int GetDurationInSeconds(string filePath)
        {
            using var audioFile = new AudioFileReader(filePath);
            return (int)audioFile.TotalTime.TotalSeconds;
        }
    }
}
