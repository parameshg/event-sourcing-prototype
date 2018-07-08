using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NinjaBoard.Business.Services;
using NinjaBoard.Database.Repositories;
using NinjaBoard.Events;
using NinjaBoard.Model;

namespace NinjaBoard
{
    public partial class Board : Form
    {
        #region Properties

        private Guid Game { get; set; }

        private Player Player { get; set; }

        private Button Coin { get; set; }

        private IServer Server { get; set; }

        #endregion

        #region .ctor

        public Board()
        {
            InitializeComponent();

            Server = new Server(new Repository(), new EventRepository());

            Game = Server.CreateGame();
        }

        #endregion

        #region Events

        private void OnLoad(object sender, EventArgs e)
        {
            foreach (var i in Server.GetGameById(Game))
                SetPosition(GetCoin(i.Key), GetPosition(i.Value));

            Player = Player.White;

            Application.DoEvents();
        }

        private void OnNewGame(object sender, EventArgs e)
        {
            Game = Server.CreateGame();

            Reset();

            Application.DoEvents();
        }

        private void OnReplayGame(object sender, EventArgs e)
        {
            foreach (var i in Server.GetEvents(Game))
            {
                if (i is GameStartedEvent)
                {
                    Reset();
                }

                if (i is CoinMovedEvent)
                {
                    var moved = i as CoinMovedEvent;

                    if (moved != null)
                    {
                        SetPosition(GetCoin(moved.Coin), GetPosition(moved.Destination));
                    }
                }

                Thread.Sleep(1000);

                Application.DoEvents();
            }
        }

        private void OnEntityClicked(object sender, EventArgs e)
        {
            var coin = sender as Button;

            if (coin != null)
            {
                if (Coin != null)
                {
                    if (Coin.Name == coin.Name)
                    {
                        Unselect();
                    }
                    else
                    {
                        Replace(Coin, coin);
                    }
                }
                else
                {
                    Select(coin);
                }
            }

            Application.DoEvents();
        }

        private void OnLocationClicked(object sender, EventArgs e)
        {
            if (Coin != null)
            {
                var position = sender as Panel;

                if (position != null)
                {
                    SetPosition(Coin, position);
                    Server.Move(Game, Player, GetCoin(Coin), GetPosition(Coin.Parent as Panel), GetPosition(position));
                    Unselect();
                    Change();
                }
            }

            Application.DoEvents();
        }

        #endregion

        #region Helpers

        private void SetPosition(Button coin, Panel position)
        {
            var source = coin.Parent as Panel;

            if (source != null)
            {
                coin.Dock = DockStyle.None;
                source.Controls.Remove(coin);
                position.Controls.Add(coin);
                coin.Dock = DockStyle.Fill;
            }
        }

        private void Replace(Button coin, Button target)
        {
            var position = target.Parent as Panel;
            position.Controls.Remove(target);
            coin.Controls.Remove(coin);
            position.Controls.Add(coin);
            Unselect();

            Server.Replace(Game, Player, GetCoin(coin), GetCoin(target), GetPosition(coin.Parent as Panel), GetPosition(target.Parent as Panel));
        }

        private void Select(Button coin)
        {
            Unselect();
            Coin = coin;
            coin.FlatStyle = FlatStyle.Flat;
            coin.FlatAppearance.BorderSize = 2;
            coin.FlatAppearance.BorderColor = Color.Red;
        }

        private void Unselect()
        {
            Coin = null;

            foreach (var i in GetControls(this, typeof(Button)))
            {
                var person = i as Button;

                if (person != null)
                    person.FlatStyle = FlatStyle.Standard;
            }
        }

        private void Reset()
        {
            SetPosition(Black_Rook_1, A8);
            SetPosition(Black_Knight_1, B8);
            SetPosition(Black_Bishop_1, C8);
            SetPosition(Black_Queen, D8);
            SetPosition(Black_King, E8);
            SetPosition(Black_Bishop_2, F8);
            SetPosition(Black_Knight_2, G8);
            SetPosition(Black_Rook_2, H8);

            SetPosition(Black_Pawn_1, A7);
            SetPosition(Black_Pawn_2, B7);
            SetPosition(Black_Pawn_3, C7);
            SetPosition(Black_Pawn_4, D7);
            SetPosition(Black_Pawn_5, E7);
            SetPosition(Black_Pawn_6, F7);
            SetPosition(Black_Pawn_7, G7);
            SetPosition(Black_Pawn_8, H7);

            SetPosition(White_Rook_1, A1);
            SetPosition(White_Knight_1, B1);
            SetPosition(White_Bishop_1, C1);
            SetPosition(White_Queen, D1);
            SetPosition(White_King, E1);
            SetPosition(White_Bishop_2, F1);
            SetPosition(White_Knight_2, G1);
            SetPosition(White_Rook_2, H1);

            SetPosition(White_Pawn_1, A2);
            SetPosition(White_Pawn_2, B2);
            SetPosition(White_Pawn_3, C2);
            SetPosition(White_Pawn_4, D2);
            SetPosition(White_Pawn_5, E2);
            SetPosition(White_Pawn_6, F2);
            SetPosition(White_Pawn_7, G2);
            SetPosition(White_Pawn_8, H2);
        }

        private void Change()
        {
            if (PlayerText.Text.Equals(Player.White.ToString()))
            {
                Player = Player.Black;
            }
            else
            {
                Player = Player.White;
            }

            PlayerText.Text = Player.ToString();
        }

        private IEnumerable<Control> GetControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetControls(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        private Button GetCoin(Coin coin)
        {
            Button result = null;

            foreach (var i in GetControls(this, typeof(Button)))
            {
                var button = i as Button;

                if (button != null)
                {
                    if (button.Name == coin.ToString())
                    {
                        result = button;
                        break;
                    }
                }
            }

            return result;
        }

        private Coin GetCoin(Button button)
        {
            var result = Model.Coin.Black_King;

            result = (Coin)Enum.Parse(typeof(Coin), button.Name);

            return result;
        }

        private Panel GetPosition(Position position)
        {
            Panel result = null;

            foreach (var i in GetControls(this, typeof(Panel)))
            {
                var panel = i as Panel;

                if (panel != null)
                {
                    if (panel.Name == position.ToString())
                    {
                        result = panel;
                        break;
                    }
                }
            }

            return result;
        }

        private Position GetPosition(Panel panel)
        {
            var result = Position.Dead;

            result = (Position)Enum.Parse(typeof(Position), panel.Name);

            return result;
        }

        #endregion
    }
}