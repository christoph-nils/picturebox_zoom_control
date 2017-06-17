using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pbox_zoom_control
{
    public partial class pbox_zoom: UserControl
    {
        public event EventHandler Click2;
        public event MouseEventHandler Mouse_Move_pic;
        public event EventHandler Mouse_Hover_pic;
        public event EventHandler Mouse_Enter_pic;
        public event EventHandler Mouse_Leave_pic;
        private string path = "";
        private void HandleClick2(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            this.OnClick2(EventArgs.Empty);
        }
        protected virtual void OnClick2(EventArgs e)
        {
            this.Click2?.Invoke(this, e);
        }
        private void HandleMouse_Move_pic(object sender, MouseEventArgs e)
        {
            // we'll explain this in a minute
            this.OnMouse_Move_pic(e);
        }
        private void OnMouse_Move_pic(MouseEventArgs e)
        {
            this.Mouse_Move_pic?.Invoke(this, e);
        }
        private void HandleMouse_Hover_pic(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            this.OnMouse_Hover_pic(EventArgs.Empty);
        }
        protected virtual void OnMouse_Hover_pic(EventArgs e)
        {
            this.Mouse_Hover_pic?.Invoke(this, e);
        }
        private void HandleMouse_Enter_pic(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            this.OnMouse_Enter_pic(EventArgs.Empty);
        }
        protected virtual void OnMouse_Enter_pic(EventArgs e)
        {
            this.Mouse_Enter_pic?.Invoke(this, e);
        }
        private void HandleMouse_Leave_pic(object sender, EventArgs e)
        {
            // we'll explain this in a minute
            this.OnMouse_Leave_pic(EventArgs.Empty);
        }
        protected virtual void OnMouse_Leave_pic(EventArgs e)
        {
            this.Mouse_Leave_pic?.Invoke(this, e);
        }

        private double ZOOMFACTOR = 1.25;   // = 25% smaller or larger
        private int MINMAX = 10;             // 5 times bigger or smaller than the ctrl
        private Bitmap bild;
        private int mouse_x = 0;
        private int mouse_y = 0;

        public pbox_zoom()
        {
            InitializeComponent();
            InitCtrl();
        }
        private void InitCtrl()
        {
            panel1.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            panel1.MouseEnter += new EventHandler(pbox_1_MouseEnter);
            pbox_1.MouseEnter += new EventHandler(pbox_1_MouseEnter);
            panel1.MouseWheel += new MouseEventHandler(pbox_1_MouseWheel);
            pbox_1.Click += HandleClick2;
            pbox_1.MouseMove += HandleMouse_Move_pic;
            pbox_1.MouseHover += HandleMouse_Hover_pic;
            pbox_1.MouseEnter += HandleMouse_Enter_pic;
            pbox_1.MouseLeave += HandleMouse_Leave_pic;
        }
        public Bitmap Image
        {
            get
            {
                return bild;
            }
            set
            {
                open_picture(value);
            }
        }
        public double zoomfactor
        {
            get
            {
                return ZOOMFACTOR;
            }
            set
            {
                ZOOMFACTOR = value;
            }
        }
        public int minmax
        {
            get
            {
                return MINMAX;
            }
            set
            {
                MINMAX = value;
            }
        }
        public string ImageLocation
        {
            get
            {
                return path;
            }
            set
            {                
                if(value != "" && value != null)
                open_picture(value);
            }
        }
        public Point Mouse_Position
        {
            get
            {
                return new Point(mouse_x,mouse_y);
            }
            
        }
        public int Mouse_X
        {
            get
            {
                return mouse_x;
            }

        }
        public int Mouse_Y
        {
            get
            {
                return mouse_y;
            }

        }
        public void open_picture(string pic_path)
        {
            bool save_position = false;
            Point position = new Point();
            Size size = new Size();
            Size old_size = new Size();
            if (pbox_1.Image != null)
                old_size = pbox_1.Image.Size;
            int v = 0;
            int h = 0;
            if (bild != null) bild.Dispose();
            bild = new Bitmap(pic_path);
            path = pic_path;
            if(bild != null && pbox_1.Image != null)
                if (bild.Size == old_size) save_position = true;
            if (save_position)
            {
                position = pbox_1.Location;
                size = pbox_1.Size;
                v = VerticalScroll.Value;
                h = HorizontalScroll.Value;
            }
            pbox_1.Image = bild;
            if (((double)bild.Width / (double)bild.Height) > ((double)panel1.Width / (double)panel1.Height))
            {
                pbox_1.Width = panel1.Width;
                pbox_1.Height = pbox_1.Width * bild.Height / bild.Width;
            }
            else
            {
                pbox_1.Height = panel1.Height;
                pbox_1.Width = pbox_1.Height * bild.Width / bild.Height;
            }
            if (save_position)
            {
                pbox_1.Location = position;
                pbox_1.Size = size;
                VerticalScroll.Value = v;
                HorizontalScroll.Value = h;
            }
            else
            {
                update_scrollbar();
                update_picture_position();
            }
        }
        public void open_picture(Bitmap Image)
        {
            bool save_position = false;
            Point position = new Point();
            Size size = new Size();
            Size old_size = new Size();
            if (pbox_1.Image != null)
                old_size = pbox_1.Image.Size;
            int v = 0;
            int h = 0;
            path = "";
            if (bild != null) bild.Dispose();
            bild = new Bitmap(Image);
            if (bild != null && pbox_1.Image != null)
                if (bild.Size == old_size) save_position = true;
            if (save_position)
            {
                position = pbox_1.Location;
                size = pbox_1.Size;
                v = VerticalScroll.Value;
                h = HorizontalScroll.Value;
            }
            pbox_1.Image = bild;
            if (((double)bild.Width / (double)bild.Height) > ((double)panel1.Width / (double)panel1.Height))
            {
                pbox_1.Width = panel1.Width;
                pbox_1.Height = pbox_1.Width * bild.Height / bild.Width;
            }
            else
            {
                pbox_1.Height = panel1.Height;
                pbox_1.Width = pbox_1.Height * bild.Width / bild.Height;
            }
            if (save_position)
            {
                pbox_1.Location = position;
                pbox_1.Size = size;
                VerticalScroll.Value = v;
                HorizontalScroll.Value = h;
            }
            else
            {
                update_scrollbar();
                update_picture_position();
            }
        }
        private void ZoomIn()
        {
            if ((pbox_1.Width < (MINMAX * panel1.Width)) &&
                (pbox_1.Height < (MINMAX * panel1.Height)))
            {
                pbox_1.Width = Convert.ToInt32(pbox_1.Width * ZOOMFACTOR);
                pbox_1.Height = Convert.ToInt32(pbox_1.Height * ZOOMFACTOR);
                pbox_1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void ZoomOut()
        {
            if ((pbox_1.Width > (panel1.Width / MINMAX)) &&
                (pbox_1.Height > (panel1.Height / MINMAX)))
            {
                pbox_1.SizeMode = PictureBoxSizeMode.StretchImage;
                pbox_1.Width = Convert.ToInt32(pbox_1.Width / ZOOMFACTOR);
                pbox_1.Height = Convert.ToInt32(pbox_1.Height / ZOOMFACTOR);
            }
        }
        private void pbox_1_MouseEnter(object sender, EventArgs e)
        {
            if (pbox_1.Focused == false)
            {
                pbox_1.Focus();
                update_picture_position();
            }
        }
        private void pbox_1_MouseWheel(object sender, MouseEventArgs e)
        {
            Point old_position = pbox_1.Location;
            Size old_size_picture = pbox_1.Size;
            Size old_size_frame = panel1.Size;
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
            Size new_size_picture = pbox_1.Size;
            Size new_size_frame = panel1.Size;
            //pbox_1.Location = new_picture_position(old_position, old_size_picture, old_size_frame, new_size_picture, new_size_frame);
            Point mouse = e.Location;
            pbox_1.Location = new_picture_position(old_position, old_size_picture, mouse, new_size_picture, mouse);
            update_scrollbar();
            update_picture_position();
        }
        Point new_picture_position(Point old_position, Size old_size_picture, Size old_size_frame, Size new_size_picture, Size new_size_frame)
        {
            Point old_position_center = new Point(old_size_frame.Width / 2 - old_position.X, old_size_frame.Height / 2 - old_position.Y);
            Point new_position_center = new Point(old_position_center.X * new_size_picture.Width / old_size_picture.Width, old_position_center.Y * new_size_picture.Height / old_size_picture.Height);
            Point new_pos = new Point(new_size_frame.Width / 2 - new_position_center.X, new_size_frame.Height / 2 - new_position_center.Y);
            return new_pos;
        }
        Point new_picture_position(Point old_position, Size old_size_picture, Point old_position_curser, Size new_size_picture, Point new_position_curser)
        {
            Point old_position_center = new Point(old_position_curser.X - old_position.X, old_position_curser.Y - old_position.Y);
            Point new_position_center = new Point(old_position_center.X * new_size_picture.Width / old_size_picture.Width, old_position_center.Y * new_size_picture.Height / old_size_picture.Height);
            Point new_pos = new Point(new_position_curser.X - new_position_center.X, new_position_curser.Y - new_position_center.Y);
            return new_pos;
        }
        private void pbox_1_SizeChanged(object sender, EventArgs e)
        {
            update_scrollbar();
            update_picture_position();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            update_scrollbar();
            update_picture_position();
        }
        void update_scrollbar()
        {
            int max_x = pbox_1.Width - panel1.Width;
            int max_y = pbox_1.Height - panel1.Height;
            if (0 < max_x)
            {
                hScrollBar1.Enabled = true;
                hScrollBar1.Maximum = max_x;
                hScrollBar1.Value = Math.Max(0, Math.Min(max_x, -pbox_1.Location.X));
            }
            else
            {
                hScrollBar1.Enabled = false;
            }
            if (0 < max_y)
            {
                vScrollBar1.Enabled = true;
                vScrollBar1.Maximum = max_y;
                vScrollBar1.Value = Math.Max(0, Math.Min(max_y, -pbox_1.Location.Y));
            }
            else
            {
                vScrollBar1.Enabled = false;
            }
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            update_picture_position();
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            update_picture_position();
        }
        void update_picture_position()
        {
            int x = -hScrollBar1.Value;
            int y = -vScrollBar1.Value;
            if (pbox_1.Size.Width < panel1.Width)
                x = (panel1.Width - pbox_1.Width) / 2;
            if (pbox_1.Size.Height < panel1.Height)
                y = (panel1.Height - pbox_1.Height) / 2;
            pbox_1.Location = new Point(x, y);

        }
        private void pbox_1_MouseMove(object sender, MouseEventArgs e)
        {
            mouse_x = bild.Width * e.Location.X / pbox_1.Width;
            mouse_y = bild.Height * e.Location.Y / pbox_1.Height;
                        
        }
        private void pbox_zoom_Load(object sender, EventArgs e)
        {
            bild = new Bitmap(10, 10);
        }
               
    }
}
