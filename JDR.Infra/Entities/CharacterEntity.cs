using JDR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Infra.Entities
{
    [Table("character")]
    public class CharacterEntity
    {
        public Guid Id { get; set; }
        public static CharacterEntity ToCharacterEntity(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
