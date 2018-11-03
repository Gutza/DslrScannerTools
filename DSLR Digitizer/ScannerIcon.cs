using System;
using System.ComponentModel;
using System.Diagnostics;
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

        public PictureBox HoverPicture;

        private bool Hovering { get { return HoverPicture==null ? false : HoverPicture.Visible; } set { HoverPicture.Visible = value; } }

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
            SetState(IconState);

            HoverPicture = new PictureBox()
            {
                Image = ImageHover,
                Left = 0,
                Top = 0,
                Width=this.Width,
                Height=this.Height,
                BackColor = Color.Transparent,
            };
            this.Controls.Add(HoverPicture);
            this.Controls.SetChildIndex(HoverPicture, 0);
            Hovering = false;

            this.MouseEnter += MouseHoverOn;
            HoverPicture.MouseLeave += MouseHoverOff;
            HoverPicture.Click += OnHoverClick;
        }

        private void OnHoverClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void MouseHoverOff(object sender, EventArgs e)
        {
            Debug.Write("-");
            Hovering = false;
        }

        private void MouseHoverOn(object sender, EventArgs e)
        {
            Debug.Write("+");
            Hovering = true;
        }
    }

}
