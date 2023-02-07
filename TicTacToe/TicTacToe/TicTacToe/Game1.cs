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

        int MouseX;
        int MouseY;

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
        MouseState currentMouseState;

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
                case MouseState.Pressed:
                    if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        currentMouseState = MouseState.WasReleasedThisFrame;
                    }
                    break;

                case MouseState.Released:

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentMouseState = MouseState.WasPressedThisFrame;
                    }
                    break;

                default:
                    break;
            }

            switch (currentMouseState) // MouseState2 Electric Boogaloo
            {
                case MouseState.WasReleasedThisFrame:

                    Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    break;

                case MouseState.WasPressedThisFrame:

                    MouseX = Mouse.GetState().X;
                    MouseY = Mouse.GetState().Y;
                    Debug.WriteLine(MouseX);
                    Debug.WriteLine(MouseY);

                    foreach(Tile aTile in GameBoardArray)
                    {
                        if (currentTurn == Turn.OTurn)
                        {
                            aTile.TrySetState(new Point(MouseX, MouseY), Tile.TileState.O);
                        }
                        else if (currentTurn == Turn.XTurn)
                        {
                            aTile.TrySetState(new Point(MouseX, MouseY), Tile.TileState.X);
                        }
                    }

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
                                currentTile.TrySetState(new Point(MouseX, MouseY), Tile.TileState.O );
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



            //Set currentMouseState to either Released or Pressed
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                currentMouseState = MouseState.Released;
            }else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                currentMouseState = MouseState.Pressed;
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
                        Debug.WriteLine("OOOOO");
                        _spriteBatch.Draw(OhTexture, tile._rectangle, Color.White);
                    }
                }
            }
             
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}