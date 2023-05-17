using JDR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities
{
    [Table("illustration")]
    public class IllustrationEntity
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }

        public IllustrationEntity()
        {

        }

        public Illustration ToIllustration()
        {
            throw new NotImplementedException();
            return new Illustration()
            {
                
            };
        }
    }
}
