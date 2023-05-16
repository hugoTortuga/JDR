using JDR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core
{
    public interface IMusicStorage
    {

        Music Get(string? path);
        Task Upload(byte[] fileContent, string path);
        int GetDurationInSeconds(string filePath);

    }
}
