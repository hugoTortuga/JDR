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

        public Illustration Get(string backgroundImage)
        {
            
            try
            {
                var fileInfo = new FileInfo(_BasePath + backgroundImage);
                return new Illustration
                {
                    Content = File.ReadAllBytes(_BasePath + backgroundImage),
                    Name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length),
                    Extension = fileInfo.Extension,
                };
            }
            catch (Exception ex)
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
