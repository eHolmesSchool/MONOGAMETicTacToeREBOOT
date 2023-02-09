using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MONOGAMETicTacToeREREBOOT.Game1;

namespace TicTacToe
{
    public class Tile //Knows where it is, know if the mouse is on it, know what state its in. DOES NOT need to know game state, textures etc.
    {
        public Rectangle _rectangle { get; private set;}

        public enum TileState
        {
            Blank,
            X,
            O
        }
        public TileState _tileState { get; set; }


        public int TilePosX { get; set; }
        public int TilePosY { get; set; }

        public bool Victorious { get; set; } //Is the tile part of the winning row  bool


        public Tile(Rectangle rectangle)
        {
            _rectangle = rectangle;
            _tileState = TileState.Blank;
            Victorious = false;
        }

        public Tile(Rectangle rectangle, TileState tileState)
        {
            _rectangle = rectangle;
            _tileState = tileState;
            Victorious = false;
        }

        public void Reset()
        {
            _tileState = TileState.Blank;
        }

        public bool TrySet(Point point) // an X and Y co-ordinate and the state we want the Tile to be. We will do the X or O logic in main
        {
            if (_tileState == TileState.Blank && _rectangle.Contains(point))
            {
                return true;
            }
            return false;
        }

        public void SetState(TileState state) // an X and Y co-ordinate and the state we want the Tile to be. We will do the X or O logic in main
        {
            _tileState = state;
            return;
        }
    }
}
