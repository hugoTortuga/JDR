using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core {
	public interface IImageStorage {
        Illustration Get(string? backgroundImage);
        Task Upload(byte[] fileContent, string nameImage);
	}
}
