using JDR.Core;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra
{
    public class ImageStorage : IImageStorage
    {

        private string _BasePath;

        public ImageStorage(string basePath)
        {
            _BasePath = basePath;
        }

        public Illustration Get(string imagePath)
        {
            try
            {
                var fileInfo = new FileInfo(_BasePath + imagePath);
                return new Illustration
                (
                    fileInfo.Name[..^fileInfo.Extension.Length],
                    fileInfo.Extension
                );
            }
            catch
            {
                return null;
            }

        }

        public async Task Upload(byte[] fileContent, string nameImage)
        {
            File.WriteAllBytes(_BasePath + nameImage, fileContent);
        }
    }
}
