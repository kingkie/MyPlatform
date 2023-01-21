using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yu3zx.WinFormControls
{
    public class LEDControl : Control, ISupportInitialize
    {
        public enum Alignment
        {
            Left,
            Right
        }

        private const float WIDTHHEIGHTRATIO = 0.5f;

        private GraphicsPath[] m_CachedPaths = new GraphicsPath[8];

        private bool m_bIsCacheBuild;

        private int m_nBorderWidth = 1;

        private Color m_colBorderColor = Color.Gray;

        private Color m_colFocusedBorderColor = Color.Cyan;

        private int m_nCornerRadius = 5;

        private int m_nCharacterNumber = 5;

        private float m_fBevelRate = 0.25f;

        private Color m_colFadedColor = Color.DimGray;

        private Color m_colCustomBk1 = Color.Black;

        private Color m_colCustomBk2 = Color.DimGray;

        private float m_fWidthSegWidthRatio = 0.2f;

        private float m_fWidthIntervalRatio = 0.05f;

        private LEDControl.Alignment m_enumAlign;

        private bool m_bRoundRect;

        private bool m_bGradientBackground;

        private bool m_bShowHighlight;

        private byte m_nHighlightOpaque = 50;

        private bool m_smoothingMode;

        private bool m_italicMode;

        private bool m_bIsInitializing;

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("文本显示斜体")]
        public bool UseItalicStyle
        {
            get
            {
                return this.m_italicMode;
            }
            set
            {
                if (this.m_italicMode == value)
                {
                    return;
                }
                this.m_italicMode = value;
                this.m_bIsCacheBuild = false;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("平滑显示")]
        public bool UseSmoothingMode
        {
            get
            {
                return this.m_smoothingMode;
            }
            set
            {
                if (this.m_smoothingMode == value)
                {
                    return;
                }
                this.m_smoothingMode = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(1), Description("控件边框大小")]
        public int BorderWidth
        {
            get
            {
                return this.m_nBorderWidth;
            }
            set
            {
                if (this.m_nBorderWidth == value)
                {
                    return;
                }
                if (value < 0 || value > 5)
                {
                    throw new ArgumentException("This value should be between 0 and 5");
                }
                this.m_nBorderWidth = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "Gray"), Description("控件边框颜色")]
        public Color BorderColor
        {
            get
            {
                return this.m_colBorderColor;
            }
            set
            {
                if (value == this.m_colBorderColor)
                {
                    return;
                }
                this.m_colBorderColor = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "Cyan"), Description("控件选中时边框颜色")]
        public Color FocusedBorderColor
        {
            get
            {
                return this.m_colFocusedBorderColor;
            }
            set
            {
                if (value == this.m_colFocusedBorderColor)
                {
                    return;
                }
                this.m_colFocusedBorderColor = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(50), Description("高亮度")]
        public byte HighlightOpaque
        {
            get
            {
                return this.m_nHighlightOpaque;
            }
            set
            {
                if (value > 100)
                {
                    throw new ArgumentException("This value should be between 0 and 50");
                }
                if (this.m_nHighlightOpaque == value)
                {
                    return;
                }
                this.m_nHighlightOpaque = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("是否高亮显示")]
        public bool ShowHighlight
        {
            get
            {
                return this.m_bShowHighlight;
            }
            set
            {
                if (this.m_bShowHighlight == value)
                {
                    return;
                }
                this.m_bShowHighlight = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(5), Description("设置圆角半径")]
        public int CornerRadius
        {
            get
            {
                return this.m_nCornerRadius;
            }
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentException("This value should be between 1 and 10");
                }
                if (this.m_nCornerRadius == value)
                {
                    return;
                }
                this.m_nCornerRadius = value;
                if (this.m_bIsInitializing)
                {
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("Set if the background was filled in gradient colors")]
        public bool GradientBackground
        {
            get
            {
                return this.m_bGradientBackground;
            }
            set
            {
                if (this.m_bGradientBackground == value)
                {
                    return;
                }
                this.m_bGradientBackground = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "System.Drawing.Color.Black"), Description("Set thr first custom background color")]
        public Color BackColor_1
        {
            get
            {
                return this.m_colCustomBk1;
            }
            set
            {
                this.m_colCustomBk1 = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "System.Drawing.Color.DimGray"), Description("Set thr second custom background color")]
        public Color BackColor_2
        {
            get
            {
                return this.m_colCustomBk2;
            }
            set
            {
                this.m_colCustomBk2 = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false), Description("Set the background bound style")]
        public bool RoundCorner
        {
            get
            {
                return this.m_bRoundRect;
            }
            set
            {
                if (this.m_bRoundRect == value)
                {
                    return;
                }
                this.m_bRoundRect = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(40), Description("Set segment interval ratio")]
        public int SegmentIntervalRatio
        {
            get
            {
                return (int)((this.m_fWidthIntervalRatio - 0.01f) * 1000f);
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("This value should be between 0 and 100");
                }
                this.m_fWidthIntervalRatio = 0.01f + (float)value * 0.001f;
                if (!this.m_bIsInitializing)
                {
                    this.m_bIsCacheBuild = false;
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(LEDControl.Alignment), "Left"), Description("Set the alignment of the text")]
        public LEDControl.Alignment TextAlignment
        {
            get
            {
                return this.m_enumAlign;
            }
            set
            {
                this.m_enumAlign = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(50), Description("Set the segment width ratio")]
        public int SegmentWidthRatio
        {
            get
            {
                return (int)((this.m_fWidthSegWidthRatio - 0.1f) * 500f);
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("This value should be between 0 and 100");
                }
                this.m_fWidthSegWidthRatio = 0.1f + (float)value * 0.002f;
                if (!this.m_bIsInitializing)
                {
                    this.m_bIsCacheBuild = false;
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(5), Description("设置字符显示数")]
        public int TotalCharCount
        {
            get
            {
                return this.m_nCharacterNumber;
            }
            set
            {
                if (value < 2)
                {
                    throw new ArgumentException("This value should be greater than 2.");
                }
                this.m_nCharacterNumber = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue(0.25), Description("Set the bevel rate of each segment")]
        public float BevelRate
        {
            get
            {
                return this.m_fBevelRate * 2f;
            }
            set
            {
                if (value < 0f || value > 1f)
                {
                    throw new ArgumentException("This value should be between 0.0 and 1");
                }
                this.m_fBevelRate = value / 2f;
                if (!this.m_bIsInitializing)
                {
                    this.m_bIsCacheBuild = false;
                    base.Invalidate();
                    return;
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "System.Color.DimGray"), Description("设置显示字符的背景颜色")]
        public Color FadedColor
        {
            get
            {
                return this.m_colFadedColor;
            }
            set
            {
                if (this.m_colFadedColor == value)
                {
                    return;
                }
                this.m_colFadedColor = value;
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue("Yu3zx"), Description("设置控件显示字符")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value.ToUpper();
                if (!this.m_bIsInitializing)
                {
                    base.Invalidate();
                }
            }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = null;
            }
        }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
            }
        }

        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
            }
        }

        public LEDControl()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.ForeColor = Color.LightGreen;
            this.BackColor = Color.Transparent;
            base.Click += new EventHandler(this.EvClick);
            base.KeyDown += new KeyEventHandler(this.EvKeyDown);
            base.GotFocus += new EventHandler(this.EvFocus);
            base.LostFocus += new EventHandler(this.EvFocus);
        }

        protected override void Dispose(bool disposing)
        {
            this.DestoryCache();
            base.Dispose(disposing);
        }

        private void DrawSegment(Graphics g, Rectangle rectBound, Color colSegment, int nIndex, float bevelRate, float segmentWidth, float segmentInterval)
        {
            if (!this.m_bIsCacheBuild)
            {
                this.DestoryCache();
                this.CreateCache(rectBound, bevelRate, segmentWidth, segmentInterval);
            }
            GraphicsPath graphicsPath = (GraphicsPath)this.m_CachedPaths[nIndex - 1].Clone();
            Matrix matrix = new Matrix();
            matrix.Translate((float)rectBound.X, (float)rectBound.Y);
            graphicsPath.Transform(matrix);
            SolidBrush solidBrush = new SolidBrush(colSegment);
            g.Clip = new Region(base.ClientRectangle);
            g.FillPath(solidBrush, graphicsPath);
            solidBrush.Dispose();
            matrix.Dispose();
            graphicsPath.Dispose();
        }

        private void DrawSingleChar(Graphics g, Rectangle rectBound, Color colCharacter, char character, float bevelRate, float segmentWidth, float segmentInterval)
        {
            switch (character)
            {
                case '(':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ')':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '*':
                case '+':
                case '@':
                    break;
                case 'X':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '!':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '^':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '=':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '<':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '>':
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '?':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ';':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ',':
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '[':
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ']':
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '\\':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '/':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ':':
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '-':
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '.':
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '0':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '1':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '2':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '3':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '4':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '5':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '6':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '7':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '8':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '9':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'A':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'B':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'C':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'D':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'E':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'F':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'G':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'H':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'I':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'J':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'K':
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'L':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'M':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'N':
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'O':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'P':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Q'://小写Q
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'R':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'S':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'T':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'U':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'V':
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'W':
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Y':
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Z':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    break;
                case '_':
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                default:
                    return;
            }
        }

        private void DrawSingleCharWithFadedBk(Graphics g, Rectangle rectBound, Color colCharacter, Color colFaded, char character, float bevelRate, float segmentWidth, float segmentInterval)
        {
            switch (character)
            {
                case '(':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ')':
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                //===================
                case 'X':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '!':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '^':
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    //this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '=':
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '<':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '>':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '?':
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ';':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ',':
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '[':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ']':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '\\':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '/':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case ':':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '-':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '.':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 8, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '0':
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '1':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '2':
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '3':
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '4':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '5':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '6':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '7':
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '8':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '9':
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'A':
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'B':
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'C':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'D':
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'E':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'F':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'G':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'H':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'I':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'J':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'K':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'L':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'N':
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'M':
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'O':
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'P':
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Q'://小写Q
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'R':
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'S':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'T':
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'U':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'V':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'W':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Y':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case 'Z':
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
                case '_':
                    this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
                    this.DrawSegment(g, rectBound, colCharacter, 4, bevelRate, segmentWidth, segmentInterval);
                    return;
            }
            this.DrawSegment(g, rectBound, colFaded, 1, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 2, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 3, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 4, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 5, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 6, bevelRate, segmentWidth, segmentInterval);
            this.DrawSegment(g, rectBound, colFaded, 7, bevelRate, segmentWidth, segmentInterval);
        }

        private void DestoryCache()
        {
            for (int i = 0; i < 8; i++)
            {
                if (this.m_CachedPaths[i] != null)
                {
                    this.m_CachedPaths[i].Dispose();
                    this.m_CachedPaths[i] = null;
                }
            }
        }

        private void CreateCache(Rectangle rectBound, float bevelRate, float segmentWidth, float segmentInterval)
        {
            Matrix matrix = new Matrix(1f, 0f, 0f, 1f, 0f, 0f);
            matrix.Shear(-0.1f, 0f);
            PointF[] array = new PointF[6];
            PointF[] array2 = new PointF[4];
            for (int i = 0; i < 8; i++)
            {
                if (this.m_CachedPaths[i] == null)
                {
                    this.m_CachedPaths[i] = new GraphicsPath();
                }
            }
            array[0].X = segmentWidth * bevelRate + segmentInterval;
            array[0].Y = segmentWidth * bevelRate;
            array[1].X = segmentInterval + segmentWidth * bevelRate * 2f;
            array[1].Y = 0f;
            array[2].X = (float)rectBound.Width - segmentInterval - segmentWidth * bevelRate * 2f;
            array[2].Y = 0f;
            array[3].X = (float)rectBound.Width - segmentInterval - segmentWidth * bevelRate;
            array[3].Y = segmentWidth * bevelRate;
            array[4].X = (float)rectBound.Width - segmentInterval - segmentWidth;
            array[4].Y = segmentWidth;
            array[5].X = segmentWidth + segmentInterval;
            array[5].Y = segmentWidth;
            this.m_CachedPaths[0].AddPolygon(array);
            this.m_CachedPaths[0].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[0].Transform(matrix);
            }
            array[0].X = (float)rectBound.Width - segmentWidth;
            array[0].Y = segmentWidth + segmentInterval;
            array[1].X = (float)rectBound.Width - segmentWidth * bevelRate;
            array[1].Y = segmentWidth * bevelRate + segmentInterval;
            array[2].X = (float)(rectBound.Width + 1);
            array[2].Y = segmentWidth * bevelRate * 2f + segmentInterval + 1f;
            array[3].X = (float)(rectBound.Width + 1);
            array[3].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f - segmentInterval - 1f;
            array[4].X = (float)rectBound.Width - segmentWidth * 0.5f;
            array[4].Y = (float)(rectBound.Height >> 1) - segmentInterval;
            array[5].X = (float)rectBound.Width - segmentWidth;
            array[5].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f - segmentInterval;
            this.m_CachedPaths[1].AddPolygon(array);
            this.m_CachedPaths[1].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[1].Transform(matrix);
            }
            array[0].X = (float)rectBound.Width - segmentWidth;
            array[0].Y = (float)(rectBound.Height >> 1) + segmentInterval + segmentWidth * 0.5f;
            array[1].X = (float)rectBound.Width - segmentWidth * 0.5f;
            array[1].Y = (float)(rectBound.Height >> 1) + segmentInterval;
            array[2].X = (float)(rectBound.Width + 1);
            array[2].Y = (float)(rectBound.Height >> 1) + segmentInterval + segmentWidth * 0.5f + 1f;
            array[3].X = (float)(rectBound.Width + 1);
            array[3].Y = (float)rectBound.Height - segmentInterval - segmentWidth * bevelRate * 2f - 1f;
            array[4].X = (float)rectBound.Width - segmentWidth * bevelRate;
            array[4].Y = (float)rectBound.Height - segmentWidth * bevelRate - segmentInterval;
            array[5].X = (float)rectBound.Width - segmentWidth;
            array[5].Y = (float)rectBound.Height - segmentWidth - segmentInterval;
            this.m_CachedPaths[2].AddPolygon(array);
            this.m_CachedPaths[2].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[2].Transform(matrix);
            }
            array[0].X = segmentWidth * bevelRate + segmentInterval;
            array[0].Y = (float)rectBound.Height - segmentWidth * bevelRate;
            array[1].X = segmentWidth + segmentInterval;
            array[1].Y = (float)rectBound.Height - segmentWidth;
            array[2].X = (float)rectBound.Width - segmentInterval - segmentWidth;
            array[2].Y = (float)rectBound.Height - segmentWidth;
            array[3].X = (float)rectBound.Width - segmentInterval - segmentWidth * bevelRate;
            array[3].Y = (float)rectBound.Height - segmentWidth * bevelRate;
            array[4].X = (float)rectBound.Width - segmentInterval - segmentWidth * bevelRate * 2f;
            array[4].Y = (float)rectBound.Height;
            array[5].X = segmentWidth * bevelRate * 2f + segmentInterval;
            array[5].Y = (float)rectBound.Height;
            this.m_CachedPaths[3].AddPolygon(array);
            this.m_CachedPaths[3].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[3].Transform(matrix);
            }
            array[0].X = 0f;
            array[0].Y = (float)(rectBound.Height >> 1) + segmentInterval + segmentWidth * 0.5f;
            array[1].X = segmentWidth * 0.5f;
            array[1].Y = (float)(rectBound.Height >> 1) + segmentInterval;
            array[2].X = segmentWidth;
            array[2].Y = (float)(rectBound.Height >> 1) + segmentInterval + segmentWidth * 0.5f;
            array[3].X = segmentWidth;
            array[3].Y = (float)rectBound.Height - segmentWidth - segmentInterval;
            array[4].X = segmentWidth * bevelRate;
            array[4].Y = (float)rectBound.Height - segmentWidth * bevelRate - segmentInterval;
            array[5].X = 0f;
            array[5].Y = (float)rectBound.Height - segmentInterval - segmentWidth * bevelRate * 2f;
            this.m_CachedPaths[4].AddPolygon(array);
            this.m_CachedPaths[4].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[4].Transform(matrix);
            }
            array[0].X = 0f;
            array[0].Y = segmentWidth * bevelRate * 2f + segmentInterval;
            array[1].X = segmentWidth * bevelRate;
            array[1].Y = segmentWidth * bevelRate + segmentInterval;
            array[2].X = segmentWidth;
            array[2].Y = segmentWidth + segmentInterval;
            array[3].X = segmentWidth;
            array[3].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f - segmentInterval;
            array[4].X = segmentWidth * 0.5f;
            array[4].Y = (float)(rectBound.Height >> 1) - segmentInterval;
            array[5].X = 0f;
            array[5].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f - segmentInterval;
            this.m_CachedPaths[5].AddPolygon(array);
            this.m_CachedPaths[5].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[5].Transform(matrix);
            }
            array[0].X = segmentWidth * 0.5f + segmentInterval;
            array[0].Y = (float)(rectBound.Height >> 1);
            array[1].X = segmentWidth + segmentInterval;
            array[1].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f;
            array[2].X = (float)rectBound.Width - segmentInterval - segmentWidth;
            array[2].Y = (float)(rectBound.Height >> 1) - segmentWidth * 0.5f;
            array[3].X = (float)rectBound.Width - segmentInterval - segmentWidth * 0.5f;
            array[3].Y = (float)(rectBound.Height >> 1);
            array[4].X = (float)rectBound.Width - segmentInterval - segmentWidth;
            array[4].Y = (float)(rectBound.Height >> 1) + segmentWidth * 0.5f;
            array[5].X = segmentWidth + segmentInterval;
            array[5].Y = (float)(rectBound.Height >> 1) + segmentWidth * 0.5f;
            this.m_CachedPaths[6].AddPolygon(array);
            this.m_CachedPaths[6].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[6].Transform(matrix);
            }
            array2[0].X = 0f;
            array2[0].Y = (float)rectBound.Height;
            array2[1].X = segmentWidth;
            array2[1].Y = (float)rectBound.Height;
            array2[2].X = segmentWidth;
            array2[2].Y = (float)rectBound.Height - segmentWidth;
            array2[3].X = 0f;
            array2[3].Y = (float)rectBound.Height - segmentWidth;
            this.m_CachedPaths[7].AddPolygon(array2);
            this.m_CachedPaths[7].CloseFigure();
            if (this.UseItalicStyle)
            {
                this.m_CachedPaths[7].Transform(matrix);
            }
            this.m_bIsCacheBuild = true;
        }

        private void DrawChars(Graphics g, float segmentWidth, float segmentInterval)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            Rectangle rectBound = default(Rectangle);
            int width = (int)((float)clientRectangle.Height * 0.5f);
            int height = clientRectangle.Height;
            int nCharacterNumber = this.m_nCharacterNumber;
            int num = ((double)segmentInterval > 0.5) ? ((int)(segmentInterval * 2f)) : 1;
            int num2 = 0;
            if (this.m_enumAlign == LEDControl.Alignment.Right)
            {
                if (this.Text.Length >= nCharacterNumber)
                {
                    num2 = 0;
                }
                else
                {
                    num2 = nCharacterNumber - this.Text.Length;
                }
            }
            for (int i = 0; i < nCharacterNumber; i++)
            {
                rectBound.Width = width;
                rectBound.Height = height;
                rectBound.X = i * rectBound.Width + 5;
                rectBound.Y = 0;
                rectBound.Inflate(-num, -num);
                if (this.m_enumAlign == LEDControl.Alignment.Left)
                {
                    if (i < this.Text.Length)
                    {
                        this.DrawSingleCharWithFadedBk(g, rectBound, this.ForeColor, this.m_colFadedColor, this.Text[i], this.m_fBevelRate, segmentWidth, segmentInterval);
                    }
                    else
                    {
                        this.DrawSingleChar(g, rectBound, this.m_colFadedColor, '8', this.m_fBevelRate, segmentWidth, segmentInterval);
                    }
                }
                else if (i >= num2)
                {
                    this.DrawSingleCharWithFadedBk(g, rectBound, this.ForeColor, this.m_colFadedColor, this.Text[i - num2], this.m_fBevelRate, segmentWidth, segmentInterval);
                }
                else
                {
                    this.DrawSingleChar(g, rectBound, this.m_colFadedColor, '8', this.m_fBevelRate, segmentWidth, segmentInterval);
                }
            }
        }

        private void CalculateDrawingParams(out float segmentWidth, out float segmentInterval)
        {
            float num = (float)base.ClientRectangle.Height * 0.5f;
            segmentWidth = num * this.m_fWidthSegWidthRatio;
            segmentInterval = num * this.m_fWidthIntervalRatio;
        }

        private void DrawRoundRect(Graphics g, Rectangle rect, float radius, Color col1, Color col2, Color colBorder, int nBorderWidth, bool bGradient, bool bDrawBorder)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            float num = radius + radius;
            RectangleF rect2 = new RectangleF(0f, 0f, num, num);
            rect2.X = (float)rect.Left;
            rect2.Y = (float)rect.Top;
            graphicsPath.AddArc(rect2, 180f, 90f);
            rect2.X = (float)(rect.Right - 1) - num;
            graphicsPath.AddArc(rect2, 270f, 90f);
            rect2.Y = (float)(rect.Bottom - 1) - num;
            graphicsPath.AddArc(rect2, 0f, 90f);
            rect2.X = (float)rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);
            graphicsPath.CloseFigure();
            Brush brush;
            if (bGradient)
            {
                brush = new LinearGradientBrush(rect, col1, col2, 90f, false);
            }
            else
            {
                brush = new SolidBrush(col1);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPath(brush, graphicsPath);
            if (bDrawBorder)
            {
                Pen pen = new Pen(colBorder);
                pen.Width = (float)nBorderWidth;
                g.DrawPath(pen, graphicsPath);
                pen.Dispose();
            }
            g.SmoothingMode = (this.UseSmoothingMode ? SmoothingMode.AntiAlias : SmoothingMode.None);
            brush.Dispose();
            graphicsPath.Dispose();
        }

        private void DrawNormalRect(Graphics g, Rectangle rect, Color col1, Color col2, Color colBorder, int nBorderWidth, bool bGradient, bool bDrawBorder)
        {
            Brush brush;
            if (bGradient)
            {
                brush = new LinearGradientBrush(rect, col1, col2, 90f);
                g.FillRectangle(brush, rect);
            }
            else
            {
                brush = new SolidBrush(col1);
                g.FillRectangle(brush, rect);
            }
            if (bDrawBorder)
            {
                rect.Width--;
                rect.Height--;
                Pen pen = new Pen(colBorder);
                g.DrawRectangle(pen, rect);
                pen.Dispose();
            }
            brush.Dispose();
        }

        private void DrawBackground(Graphics g)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            Color colBorder = this.Focused ? this.m_colFocusedBorderColor : this.m_colBorderColor;
            if (this.m_bRoundRect)
            {
                this.DrawRoundRect(g, clientRectangle, (float)this.m_nCornerRadius, this.m_colCustomBk1, this.m_colCustomBk2, colBorder, this.m_nBorderWidth, this.m_bGradientBackground, this.m_nBorderWidth != 0);
                return;
            }
            if (this.m_colCustomBk1 == Color.Transparent)
            {
                return;
            }
            this.DrawNormalRect(g, clientRectangle, this.m_colCustomBk1, this.m_colCustomBk2, colBorder, this.m_nBorderWidth, this.m_bGradientBackground, this.m_nBorderWidth != 0);
        }

        private void DrawHighlight(Graphics g)
        {
            if (this.m_bShowHighlight)
            {
                Rectangle clientRectangle = base.ClientRectangle;
                clientRectangle.Height >>= 1;
                clientRectangle.Inflate(-2, -2);
                Color col = Color.FromArgb(100, 255, 255, 255);
                Color col2 = Color.FromArgb((int)this.m_nHighlightOpaque, 255, 255, 255);
                this.DrawRoundRect(g, clientRectangle, (float)((this.m_nCornerRadius - 1 > 1) ? (this.m_nCornerRadius - 1) : 1), col, col2, Color.Empty, 0, true, false);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = (this.UseSmoothingMode ? SmoothingMode.AntiAlias : SmoothingMode.None);
            float segmentWidth = 0f;
            float segmentInterval = 0f;
            if (base.ClientRectangle.Height >= 20 && base.ClientRectangle.Width >= 20)
            {
                this.DrawBackground(graphics);
                this.CalculateDrawingParams(out segmentWidth, out segmentInterval);
                this.DrawChars(graphics, segmentWidth, segmentInterval);
                this.DrawHighlight(graphics);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.m_bIsCacheBuild = false;
            base.OnSizeChanged(e);
        }

        void ISupportInitialize.BeginInit()
        {
            this.m_bIsInitializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            this.m_bIsInitializing = false;
            base.Invalidate();
        }

        private void EvClick(object sender, EventArgs e)
        {
            base.Focus();
        }

        private void EvKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                try
                {
                    Clipboard.SetText(this.Text);
                }
                catch
                {
                }
            }
        }

        private void EvFocus(object sender, EventArgs e)
        {
            base.Invalidate();
        }
    }
}
