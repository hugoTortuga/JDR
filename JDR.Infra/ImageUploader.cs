using JDR.Core;
using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra {
	public class ImageUploader : IImageUploader {

        private string _BasePath;

        public ImageUploader(string basePath)
        {
			_BasePath = basePath;

		}

        public Illustration Get(string? backgroundImage)
        {
            var fileInfo = new FileInfo(backgroundImage);
            return new Illustration
            {
                Content = File.ReadAllBytes(_BasePath + backgroundImage),
                Name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length),
                Extension = fileInfo.Extension,
            };
        }

        public async Task Upload(byte[] fileContent, string nameImage) {
			File.WriteAllBytes(_BasePath + nameImage, fileContent);
		}
	}
}
