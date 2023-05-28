using AutoMapper;
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
        public Guid? Id { get; set; }
        
        private string? _Name;
        public string? Name
        {
            get => _Name; set
            {
                if (value == null || (value != null && value.Length <= 50))
                {
                    _Name = value;
                }
            }
        }

        public IllustrationEntity? IllustationEntity { get; set; }
        public IllustrationEntity? IllustrationTokenEntity { get; set; }
        public string? Description { get; set; }
        public int Level { get; set; }
        public int IdRace { get; set; }
        public Skills Skills { get; set; }
        public Inventory Inventory { get; set; }
        public IList<Spell> Spells { get; set; }
        public int HPMax { get; set; }
        public int ManaMax { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int CurrentXP { get; set; }

        public CharacterEntity()
        {
            Skills = new Skills();
            Inventory = new Inventory();
            Spells = new List<Spell>();
        }

        public Character ToCharacter(IMapper mapper)
        {
            return mapper.Map<Character>(this);
        }

        public void UpdateAttributesFrom(Character character)
        {
            Name = character.Name;
            IdRace = (int)character.Race;
            Description = character.Description;
            HP = character.HP;
            HPMax = character.HPMax;
            IllustationEntity = character.Illustration == null ? null : IllustrationEntity.ToIllustrationEntity(character.Illustration);
            IllustrationTokenEntity = character.Token == null ? null : IllustrationEntity.ToIllustrationEntity(character.Token);
            Inventory = character.Inventory;
            Level = character.Level;
            CurrentXP = character.CurrentXP;
            Mana = character.Mana;
            ManaMax = character.ManaMax;
            Skills = character.Skills;
            Spells = character.Spells;
        }

        public static CharacterEntity ToCharacterEntity(Character character)
        {
            return new CharacterEntity
            {
                Id = character.Id,
                Name = character.Name,
                IdRace = (int)character.Race,
                Description = character.Description,
                HP = character.HP,
                HPMax = character.HPMax,
                IllustationEntity = character.Illustration == null ? null : IllustrationEntity.ToIllustrationEntity(character.Illustration),
                IllustrationTokenEntity = character.Token == null ? null : IllustrationEntity.ToIllustrationEntity(character.Token),
                Inventory = character.Inventory,
                Level = character.Level,
                CurrentXP = character.CurrentXP,
                Mana = character.Mana,
                ManaMax = character.ManaMax,
                Skills = character.Skills,
                Spells = character.Spells
            };
        }
    }
}
