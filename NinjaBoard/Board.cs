using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NinjaBoard.Model;

namespace NinjaBoard
{
    public partial class Board : Form
    {
        #region Properties

        private Player Player { get; set; }

        private Button Selected { get; set; }

        private ApiProxy Api { get; set; }

        #endregion

        #region .ctor

        public Board()
        {
            InitializeComponent();

            Api = new ApiProxy();
        }

        #endregion

        #region Events

        private void OnLoad(object sender, EventArgs e)
        {
            OnWhitePlayer(sender, e);

            Application.DoEvents();
        }

        private void OnNewGame(object sender, EventArgs e)
        {
            Reset();

            Application.DoEvents();
        }

        private void OnReplayGame(object sender, EventArgs e)
        {

        }

        private void OnReplayGameBackwards(object sender, EventArgs e)
        {

        }

        private void OnEntityClicked(object sender, EventArgs e)
        {
            var person = sender as Button;

            if (person != null)
            {
                if (Selected != null)
                {
                    if (Selected.Name == person.Name)
                    {
                        Unselect();
                    }
                    else
                    {
                        Replace(Selected, person);
                    }
                }
                else
                {
                    Select(person);
                }
            }

            Application.DoEvents();
        }

        private void OnLocationClicked(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                var location = sender as Panel;

                if (location != null)
                {
                    SetLocation(Selected, location);
                    ReversePlayer();
                }
            }

            Application.DoEvents();
        }

        private void OnBlackPlayer(object sender, EventArgs e)
        {
            Player = Player.Black;

            tsmiBlackPlayer.Checked = true;
            tsmiWhitePlayer.Checked = false;

            Application.DoEvents();
        }

        private void OnWhitePlayer(object sender, EventArgs e)
        {
            Player = Player.White;

            tsmiBlackPlayer.Checked = false;
            tsmiWhitePlayer.Checked = true;

            Application.DoEvents();
        }

        #endregion

        #region Helpers

        private void SetLocation(Button person, Panel location)
        {
            var source = person.Parent as Panel;

            if (source != null)
            {
                person.Dock = DockStyle.None;
                source.Controls.Remove(person);
                location.Controls.Add(person);
                person.Dock = DockStyle.Fill;
                Unselect();
            }
        }

        private void Replace(Button person, Button target)
        {
            var location = target.Parent as Panel;
            location.Controls.Remove(target);
            person.Controls.Remove(person);
            location.Controls.Add(person);
            Unselect();
        }

        private void Select(Button person)
        {
            Unselect();
            Selected = person;
            person.FlatStyle = FlatStyle.Flat;
            person.FlatAppearance.BorderSize = 2;
            person.FlatAppearance.BorderColor = Color.Red;
        }

        private void Unselect()
        {
            Selected = null;

            foreach (var i in GetControls(this, typeof(Button)))
            {
                var person = i as Button;

                if (person != null)
                    person.FlatStyle = FlatStyle.Standard;
            }
        }

        private void Reset()
        {
            SetLocation(Black_Rook_1, A8);
            SetLocation(Black_Knight_1, B8);
            SetLocation(Black_Bishop_1, C8);
            SetLocation(Black_Queen, D8);
            SetLocation(Black_King, E8);
            SetLocation(Black_Bishop_2, F8);
            SetLocation(Black_Knight_2, G8);
            SetLocation(Black_Rook_2, H8);

            SetLocation(Black_Pawn_1, A7);
            SetLocation(Black_Pawn_2, B7);
            SetLocation(Black_Pawn_3, C7);
            SetLocation(Black_Pawn_4, D7);
            SetLocation(Black_Pawn_5, E7);
            SetLocation(Black_Pawn_6, F7);
            SetLocation(Black_Pawn_7, G7);
            SetLocation(Black_Pawn_8, H7);

            SetLocation(White_Rook_1, A1);
            SetLocation(White_Knight_1, B1);
            SetLocation(White_Bishop_1, C1);
            SetLocation(White_Queen, D1);
            SetLocation(White_King, E1);
            SetLocation(White_Bishop_2, F1);
            SetLocation(White_Knight_2, G1);
            SetLocation(White_Rook_2, H1);

            SetLocation(White_Pawn_1, A2);
            SetLocation(White_Pawn_2, B2);
            SetLocation(White_Pawn_3, C2);
            SetLocation(White_Pawn_4, D2);
            SetLocation(White_Pawn_5, E2);
            SetLocation(White_Pawn_6, F2);
            SetLocation(White_Pawn_7, G2);
            SetLocation(White_Pawn_8, H2);
        }

        private void Disable(Player player)
        {
            foreach (var i in GetControls(this, typeof(Button)))
            {
                var person = i as Button;

                if (person != null && person.Tag != null)
                {
                    if (person.Tag.ToString().Equals(player.ToString()))
                    {
                        person.Enabled = false;
                        person.Parent.Enabled = false;
                    }
                    else
                    {
                        person.Enabled = true;
                        person.Parent.Enabled = true;
                    }
                }
            }
        }

        private void ReversePlayer()
        {
            if (tsmiBlackPlayer.Checked)
            {
                OnWhitePlayer(null, new EventArgs());
            }
            else
            {
                OnBlackPlayer(null, new EventArgs());
            }
        }

        private IEnumerable<Control> GetControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetControls(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        #endregion
    }
}