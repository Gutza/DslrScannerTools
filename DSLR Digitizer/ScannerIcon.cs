using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DSLR_Digitizer
{
    public class ScannerIcon : PictureBox
    {
        [Bindable(true)]
        public Image ImageDisabled { get; set; }

        [Bindable(true)]
        public Image ImageDefault { get; set; }

        [Bindable(true)]
        public Image ImageActive { get; set; }

        [Bindable(true)]
        public Image ImageHover { get; set; }

        public enum IconStates
        {
            Disabled,
            Default,
            Active,
        }

        private PictureBox HoverPicture;

        public bool Hovering { get { return HoverPicture.Visible; } set { HoverPicture.Visible = value; } }

        private IconStates _iconState;

        public IconStates IconState { get { return _iconState; } set { SetState(value); } }

        private void SetState(IconStates value)
        {
            switch (value)
            {
                case IconStates.Active:
                    Activate();
                    break;
                case IconStates.Default:
                    Reset();
                    break;
                case IconStates.Disabled:
                    Disable();
                    break;
                default:
                    throw new NotImplementedException("Unknown icon state: " + value.ToString());
            }
        }

        public void Activate()
        {
            _iconState = IconStates.Active;
            Image = ImageActive;
        }

        public void Disable()
        {
            _iconState = IconStates.Disabled;
            Image = ImageDisabled;
        }

        public void Reset()
        {
            _iconState = IconStates.Default;
            Image = ImageDefault;
        }

        public void Initialize()
        {
            HoverPicture = new PictureBox()
            {
                Image = ImageHover,
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height,
            };
            this.Parent.Controls.Add(HoverPicture);

            
        }
    }

}
