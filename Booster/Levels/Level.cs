using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Booster.States;
using Booster.Input;
using System.IO;
using Booster.States.Menus;
using Booster.Util;
using System.Xml.Linq;
using System.Linq;
using System.Xml;

namespace Booster.Levels
{
    public class Level : IGameState, IGameStateContext
    {
        public Resources Resources { get; set; }
        public Game Game { get; set; }

        private IGameStateContext stateManager;

        public Dictionary<GameStates, IGameState> States { get; set; }

        private XElement level;

        private GameStates currentState;
        public GameStates CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                if (States.ContainsKey(value))
                {
                    currentState = value;
                    if (currentState != GameStates.Playing)
                    {
                        InitializeCurrentGameState();
                    }
                }
                else
                {
                    stateManager.CurrentState = value;
                    if (stateManager.CurrentState == GameStates.LevelCompleted)
                    {
                        XDocument xdoc = XDocument.Load(@"Content\Levels\Levels.xml");
                        XElement score = new XElement("Score");
                        score.Add(new XAttribute("score", ((StateLevelPlaying)States[GameStates.Playing]).Map.Player.Score));
                        score.Add(new XAttribute("date", XmlConvert.ToString(System.DateTime.Today, "dd-MM-yyyy")));
                        IEnumerable<XElement> levels = xdoc.Descendants("Level").Where(level => level.Parent.Name == this.level.Parent.Name);
                        
                        foreach (XElement level in levels)
                        {
                            if (this.level.Attribute("name").Value == level.Attribute("name").Value)
                            {
                                level.Add(score);
                            }
                            if (this.level.NextNode == null)
                            {
                                return;
                            }
                            if (this.level.NodesAfterSelf().OfType<XElement>().First().Attribute("name").Value == level.Attribute("name").Value)
                            {
                                level.Attribute("enabled").Value = "true";
                                break;
                            }
                        }
                        xdoc.Save(@"Content\Levels\Levels.xml");
                    }
                }
            }
        }

        public Level(IGameStateContext stateManager)
        {
            this.Game = stateManager.Game;
            this.stateManager = stateManager;
            Resources = stateManager.Resources;
        }

        public void LoadMap(XElement level)
        {
            this.level = level;
            ((StateLevelPlaying)States[GameStates.Playing]).LoadMap(@"Content\Levels\" + level.Attribute("file").Value);
            InitializeCurrentGameState();
        }

        public void InitializeCurrentGameState()
        {
            States[CurrentState].Initialize();
        }

        public void Initialize()
        {
            States = new Dictionary<GameStates, IGameState>();
            States[GameStates.Playing] = new StateLevelPlaying(this);
            States[GameStates.Pause] = new StateLevelPause(this);

            CurrentState = GameStates.Playing;
        }

        public void Update(GameTime gameTime)
        {
            States[CurrentState].Update(gameTime);
        }

        //public void MovePlayer(Vector2 nextPosition)
        //{
        //    Vector2 playerMovement = nextPosition - player.Position;
        //    int numberOfChecks = (int)playerMovement.Length() + 1;
        //    Vector2 nextPositionToTry = player.Position + playerMovement / numberOfChecks;

        //    for (int i = 0; i < numberOfChecks; i++)
        //    {
        //        Rectangle playerRectangle = player.BoundingBox.HitBoxInPosition(nextPositionToTry);
        //        if (!CheckPlayerTilesCollisions(playerRectangle))
        //        {
        //            player.Position = nextPositionToTry;
        //            nextPositionToTry += playerMovement / numberOfChecks;
        //        }
        //        else
        //        {
        //            if (playerMovement.X != 0 && playerMovement.Y != 0)
        //            {
        //                MovePlayer(nextPosition * Vector2.UnitX + player.Position * Vector2.UnitY);
        //                MovePlayer(nextPosition * Vector2.UnitY + player.Position * Vector2.UnitX);
        //            }
        //            break;
        //        }
        //    }
        //}

        public void Draw(GameTime gameTime)
        {
            States[CurrentState].Draw(gameTime);
        }
    }
}