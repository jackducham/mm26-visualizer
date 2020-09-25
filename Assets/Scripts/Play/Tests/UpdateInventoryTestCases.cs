using System.Collections;
using System.Collections.Generic;
using MM26.IO.Models;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.Play.Tests
{
    public class UpdateInventoryTestCases : IEnumerable
    {
        private VisualizerTurn GetTurn(string playerBoard)
        {
            GameState gameState = new GameState();
            GameChange gameChange = new GameChange();

            gameState.PlayerNames["player"] = new Player()
            {
                Character = new Character()
                {
                    CurrentHealth = 10,
                    Level = 22,
                    Experience = 0,
                    Position = new Position()
                    {
                        BoardId = playerBoard,
                        X = 0,
                        Y = 0
                    },

                    Weapon = new Weapon()
                    {
                        Sprite = "mm26_wearables/weapons/wands/4.png"
                    }
                },

                Hat = new Hat()
                {
                    Sprite = "mm26_wearables/clothes/alchemist_head.png"
                },
                Clothes = new Clothes()
                {
                    Sprite = "mm26_wearables/clothes/alchemist_head.png"
                },
                Shoes = new Shoes()
                {
                    Sprite = "mm26_wearables/clothes/alchemist_head.png"
                },
                //Accessory = new Accessory()
                //{
                //    Sprite = "mm26_wearables/clothes/alchemist_head.png"
                //}
            };

            gameChange.CharacterChanges["player"] = new CharacterChange()
            {
                Respawned = false,
                Died = false,
                Decision = new CharacterDecision()
                {
                    DecisionType = DecisionType.Equip
                },
                HatChanged = true,
            };

            var turn = new VisualizerTurn()
            {
                State = gameState,
                Change = gameChange
            };

            return turn;
        }

        private object[] GetOnBoard()
        {
            return new object[]
            {
                this.GetTurn("test"),
                new HashSet<Task>()
                {
                    new UpdateInventoryTask("player")
                    {
                        hat_changed = true,
                        Head = "mm26_wearables/clothes/alchemist_head.png"
                    }
                }
            };
        }

        private object[] GetNotOnBoard()
        {
            return new object[]
            {
                this.GetTurn("other"),
                new HashSet<Task>()
            };
        }

        public IEnumerator GetEnumerator()
        {
            yield return this.GetOnBoard();
            yield return this.GetNotOnBoard();
        }
    }
}
