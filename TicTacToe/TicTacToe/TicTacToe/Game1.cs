using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using TicTacToe;

namespace MONOGAMETicTacToeREREBOOT
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D OhTexture;
        private Texture2D ExTexture;
        private Texture2D BoardTexture;
        private Rectangle OhRectangle;
        private Rectangle ExRectangle;
        private Rectangle BoardRectangle;
        bool DownLastFrame;

        float MouseX;
        float MouseY;

        public enum GameState
        {
            Initialize,
            SwapTurn,
            ExecuteTurn,
            EvaluateBoard,
            GameEnd
        }
        GameState currentGameState = GameState.Initialize;

        public enum MouseState
        {
            Released,
            Pressed,
            WasReleasedThisFrame,
            WasPressedThisFrame,
        }
        MouseState currentMouseState = MouseState.Released;

        public enum Turn
        {
            OTurn,
            XTurn
        }
        Turn currentTurn = Turn.OTurn;

        //public enum TileState
        //{
        //    BlankState,
        //    XState,
        //    OState
        //}
        //TileState currentTileState = TileState.BlankState;


        Tile[,] GameBoardArray = new Tile[3, 3];

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = BoardTexture.Height;
            _graphics.PreferredBackBufferWidth = BoardTexture.Width;
            _graphics.ApplyChanges();

            ExRectangle = ExTexture.Bounds;
            OhRectangle = OhTexture.Bounds;
            BoardRectangle = BoardTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            OhTexture = Content.Load<Texture2D>("O");
            ExTexture = Content.Load<Texture2D>("X");
            BoardTexture = Content.Load<Texture2D>("TicTacToeBoard");
        }


        protected override void Update(GameTime gameTime)
        {
            switch (currentMouseState)
            {
                case MouseState.Released:
                    break;
                case MouseState.Pressed:
                    break;
                case MouseState.WasReleasedThisFrame:
                    currentMouseState = MouseState.Released;
                    break;
                case MouseState.WasPressedThisFrame:
                    currentMouseState = MouseState.Pressed;
                    break;
                default:
                    break;
            }

            switch (currentGameState)
            {
                case GameState.Initialize:

                    currentMouseState = MouseState.Released;
                    for (int currentRow = 0; currentRow < 3; currentRow++)
                    {
                        for (int currentCol = 0; currentCol < 3; currentCol++)
                        {
                            GameBoardArray[currentRow, currentCol] =
                                new Tile (new Rectangle(new
                                ((currentCol * 50) + (currentCol * 10),
                                (currentRow * 50) + (currentRow * 10)),
                                new(50, 50)), Tile.TileState.Blank );
                        }
                    }

                    foreach (Tile tile in GameBoardArray)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.SwapTurn:
                    if (currentTurn == Turn.OTurn) {currentTurn = Turn.XTurn;}
                    else if (currentTurn == Turn.XTurn) {currentTurn = Turn.OTurn;}
                    else { Debug.Write("SWAPTURN ERROR");}

                    currentGameState = GameState.ExecuteTurn;
                    break;
                case GameState.ExecuteTurn:
                    if (currentMouseState == MouseState.WasPressedThisFrame)
                    {
                        if(currentTurn == Turn.OTurn)
                        {
                            foreach (Tile currentTile in GameBoardArray  )
                            {
                                currentTile.TrySetState()
                            }
                        }
                    }

                    break;
                case GameState.EvaluateBoard:
                    break;
                case GameState.GameEnd:
                    break;
                default:
                    break;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && DownLastFrame == false)
            {
                MouseX = Mouse.GetState().X;
                MouseY = Mouse.GetState().Y;
                Debug.WriteLine(MouseX);
                Debug.WriteLine(MouseY);

                if (currentTurn == Turn.OTurn)
                {
                    currentTurn = Turn.XTurn;
                }
                else
                {
                    currentTurn = Turn.OTurn;
                }

                DownLastFrame = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                DownLastFrame = false;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here


            _spriteBatch.Begin();
            _spriteBatch.Draw(BoardTexture, new Vector2(0, 0), Color.White);

            foreach (Tile tile in GameBoardArray)
            {
                if(tile._tileState != Tile.TileState.Blank)
                {
                    if (tile._tileState == Tile.TileState.X)
                    {
                        _spriteBatch.Draw(ExTexture, tile._rectangle, Color.White);
                    }
                    else if (tile._tileState == Tile.TileState.O)
                    {
                        _spriteBatch.Draw(OhTexture, tile._rectangle, Color.White);
                    }
                }
                
            }
             
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}