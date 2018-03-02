using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;


namespace HR
{
	/// <summary>
	/// Summary description for formWait.
	/// </summary>
	public class formWait : System.Windows.Forms.Form
	{
		// Threading
//		static SplashScreen ms_frmSplash = null;
//		static Thread ms_oThread = null;

		// Fade in and out.
//		private double m_dblOpacityIncrement = .05;
//		private double m_dblOpacityDecrement = .08;
		private const int TIMER_INTERVAL = 50;

		// Status and progress bar
		//private string m_sStatus;
		private double m_dblCompletionFraction = 0;
		private Rectangle m_rProgress;

		// Progress smoothing
		private double m_dblLastCompletionFraction = 0.0;
		private double m_dblPBIncrementPerTimerInterval = .015;

		// Self-calibration support
		private bool m_bFirstLaunch = false;
		private DateTime m_dtStart;
		private bool m_bDTSet = false;
		private int m_iIndex = 1;
		private int m_iActualTicks = 0;
		private ArrayList m_alPreviousCompletionFraction;
		private ArrayList m_alActualTimes = new ArrayList();
		private string RegPrefix;
		private const string REG_KEY_INITIALIZATION = "Initialization";
		private string REGVALUE_PB_MILISECOND_INCREMENT;
		private string REGVALUE_PB_PERCENTS;

		internal System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer timer1;
		internal System.Windows.Forms.Label labelTimeRemaining;
		private System.Windows.Forms.Panel pnlStatus;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formWait(string reg)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.RegPrefix = reg;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formWait));
			this.label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.labelTimeRemaining = new System.Windows.Forms.Label();
			this.pnlStatus = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(360, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Моля изчакайте докато се прехвърлят данните в Еxcel :";
			// 
			// timer1
			// 
			this.timer1.Interval = 50;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// labelTimeRemaining
			// 
			this.labelTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.labelTimeRemaining.Location = new System.Drawing.Point(368, 16);
			this.labelTimeRemaining.Name = "labelTimeRemaining";
			this.labelTimeRemaining.Size = new System.Drawing.Size(40, 16);
			this.labelTimeRemaining.TabIndex = 0;
			// 
			// pnlStatus
			// 
			this.pnlStatus.Location = new System.Drawing.Point(8, 40);
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(416, 24);
			this.pnlStatus.TabIndex = 1;
			this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
			// 
			// formWait
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 70);
			this.Controls.Add(this.pnlStatus);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelTimeRemaining);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formWait";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Моля изчакайте!";
			this.Load += new System.EventHandler(this.formWait_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Abe tuka si otbelqzwame koga da cyka
		/// </summary>
		private void SetReferenceInternal()
		{
			if( m_bDTSet == false )
			{
				m_bDTSet = true;
				m_dtStart = DateTime.Now;
				ReadIncrements();
			}
			double dblMilliseconds = ElapsedMilliSeconds();
			m_alActualTimes.Add(dblMilliseconds);
			m_dblLastCompletionFraction = m_dblCompletionFraction;
			if( m_alPreviousCompletionFraction != null 
				&& m_iIndex < m_alPreviousCompletionFraction.Count )
				m_dblCompletionFraction = 
					(double)m_alPreviousCompletionFraction[m_iIndex++];
			else
				m_dblCompletionFraction = ( m_iIndex > 0 )? 1: 0;
		}

		// Utility function to return elapsed Milliseconds since the 
		// SplashScreen was launched.
		private double ElapsedMilliSeconds()
		{
			TimeSpan ts = DateTime.Now - m_dtStart;
			return ts.TotalMilliseconds;
		}

		
		/// <summary>
		/// Static method called from the initializing application to 
		/// give the splash screen reference points.  Not needed if
		/// you are using a lot of status strings.
		/// </summary>
		
		public void SetReferencePoint()
		{			
			this.SetReferenceInternal();
		}


		// Function to read the checkpoint intervals 
		// from the previous invocation of the
		// splashscreen from the registry.
		private void ReadIncrements()
		{
			string sPBIncrementPerTimerInterval = 
				RegistryAccess.GetStringRegistryValue( 
				REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
			double dblResult;

			if( Double.TryParse(sPBIncrementPerTimerInterval, 
				System.Globalization.NumberStyles.Float,
				System.Globalization.NumberFormatInfo.InvariantInfo, 
				out dblResult) )
				m_dblPBIncrementPerTimerInterval = dblResult;
			else
				m_dblPBIncrementPerTimerInterval = .0015;

			string sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(
				REGVALUE_PB_PERCENTS, "" );

			if( sPBPreviousPctComplete != "" )
			{
				string [] aTimes = sPBPreviousPctComplete.Split(null);
				m_alPreviousCompletionFraction = new ArrayList();

				for(int i = 0; i < aTimes.Length; i++ )
				{
					double dblVal;
					if( Double.TryParse(aTimes[i],
						System.Globalization.NumberStyles.Float, 
						System.Globalization.NumberFormatInfo.InvariantInfo, 
						out dblVal) )
						m_alPreviousCompletionFraction.Add(dblVal);
					else
						m_alPreviousCompletionFraction.Add(1.0);
				}
			}
			else
			{
				m_bFirstLaunch = true;
				labelTimeRemaining.Text = "";
			}      
		}
		/// <summary>
		/// Method to store the intervals (in percent complete)
		/// from the current invocation of
		/// the splash screen to the registry.
		/// </summary>
		public void StoreIncrements()
		{
			string sPercent = "";
			double dblElapsedMilliseconds = ElapsedMilliSeconds();
			for( int i = 0; i < m_alActualTimes.Count; i++ )
				sPercent += ((double)m_alActualTimes[i]/
					dblElapsedMilliseconds).ToString("0.####",
					System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

			RegistryAccess.SetStringRegistryValue( 
				REGVALUE_PB_PERCENTS, sPercent );

			m_dblPBIncrementPerTimerInterval = 1.0/(double)m_iActualTicks;
			RegistryAccess.SetStringRegistryValue( 
				REGVALUE_PB_MILISECOND_INCREMENT, 
				m_dblPBIncrementPerTimerInterval.ToString("#.000000",
				System.Globalization.NumberFormatInfo.InvariantInfo));
		}

		// Tick Event handler for the Timer control.  
		// Handle fade in and fade out.  Also
		// handle the smoothed progress bar.
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			m_iActualTicks++;
			if( m_bFirstLaunch == false && m_dblLastCompletionFraction 
				< m_dblCompletionFraction )
			{
				m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
				int width = (int)Math.Floor(
					pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
				int height = pnlStatus.ClientRectangle.Height;
				int x = pnlStatus.ClientRectangle.X;
				int y = pnlStatus.ClientRectangle.Y;
				if( width > 0 && height > 0 )
				{
					m_rProgress = new Rectangle( x, y, width, height);
					pnlStatus.Invalidate(m_rProgress);
					int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * 
						((1.0 - m_dblLastCompletionFraction)/
						m_dblPBIncrementPerTimerInterval)) / 1000;
					labelTimeRemaining.Text = string.Format( "{0}",	iSecondsLeft);

				}
			}
		}

		// Paint the portion of the panel invalidated during the tick event.
		private void pnlStatus_Paint(object sender, 
			System.Windows.Forms.PaintEventArgs e)
		{
			if( m_bFirstLaunch == false && e.ClipRectangle.Width > 0 
				&& m_iActualTicks > 1 )
			{
				LinearGradientBrush brBackground = 
					new LinearGradientBrush(m_rProgress, 
					Color.FromArgb(100, 100, 100),
					Color.FromArgb(150, 150, 255), 
					LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brBackground, m_rProgress);
			}
		}

		private void formWait_Load(object sender, System.EventArgs e)
		{
			timer1.Interval = TIMER_INTERVAL;
			timer1.Start();
			this.REGVALUE_PB_MILISECOND_INCREMENT = this.RegPrefix + "Increments";
			this.REGVALUE_PB_PERCENTS = this.RegPrefix + "Percents";
			this.SetReferencePoint();			
		}
	}
}