using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core
{
    public class GameCore
    {

        private IMainRepository _Repository;
        private IImageStorage _ImageUploader;

        public GameCore(IMainRepository repo, IImageStorage imageUploader)
        {
            _Repository = repo;
            _ImageUploader = imageUploader;
        }

        public void AddCharacterToGame(Character character)
        {
            ArgumentNullException.ThrowIfNull(character);
            ArgumentNullException.ThrowIfNullOrEmpty(character.Name);
            _Repository.SaveCharacter(character);
        }

        public IEnumerable<Character> GetCharacters()
        {
            return _Repository.GetAllCharacters();

        }

        public Character GetCharacter(Guid id)
        {
            return _Repository.GetCharacterById(id);
        }

        public IEnumerable<InventoryItem> GetAllItems()
        {
            return _Repository.GetAllItems();
        }

        public IEnumerable<Race> GetAllRaces()
        {
            return _Repository.GetAllRaces();
        }

        public IList<Game> GetAvailableGames()
        {
            return _Repository.GetYourGames();
        }

        public List<Scene> GetAvailableScenes()
        {
            return _Repository.GetAllScenes();
        }

        public Game GetLastGame()
        {
            return _Repository.GetLastGame();
        }

        public async Task SaveGame(Game game)
        {
            await _Repository.SaveGame(game);
        }

        public async Task SaveScene(Scene currentScene)
        {

            if (currentScene?.Background != null
                && currentScene.Background?.Content != null
                && !string.IsNullOrEmpty(currentScene.Background.Name)
                && !string.IsNullOrEmpty(currentScene.Background.Extension))
                await _ImageUploader.Upload(currentScene.Background.Content, currentScene.Background.Name + currentScene.Background.Extension);
        }

        public void SaveCharacter(Character character)
        {
            if (character == null) return;
            _Repository.SaveCharacter(character);
        }

        public Character GetCharacterTest()
        {

            return new Character("Julio",
                        new Race(),
                        new Skills
                        {
                            Agility = 35,
                            Force = 40,
                            Intelligence = 50,
                            Courage = 40,
                            Discretion = 25,
                            Persuasion = 50,
                            Observation = 20
                        }
                    )
            {

                Illustration = new Illustration("julio", "jpg"),
                HP = 14,
                HPMax = 14,
                Mana = 10,
                ManaMax = 10,
                Level = 1,
                Spells = new List<Spell> {
                                new Spell {
                                    Category = MagicCategory.Heal,
                                    Level = 1
                                },
                                new Spell {
                                    Category = MagicCategory.Nature,
                                    Level = 1
                                }
                            },
                Inventory = new Inventory
                {
                    Objects = new List<InventoryItem> {
                                new InventoryItem("Epée courte"),
                                new InventoryItem("Bouclier"),
                                new InventoryItem("Arc court (8 flèches)"),
                                new InventoryItem("Calice"),
                                new InventoryItem("Fiole")
                            }
                }
            };
        }
    }

}
