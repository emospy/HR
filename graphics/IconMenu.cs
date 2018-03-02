/*
 * by Mattias Sjögren
 * mattias@mvps.org
 * http://www.msjogren.net/dotnet/
 *
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;



namespace LichenSystaw2004
{

	/// <summary>
	/// Required designer variable.
	/// </summary>
	public class IconMenuItem : MenuItem
	{

		Icon m_Icon;
		Font m_Font;


		/// <summary>
		/// Required designer variable.
		/// </summary>
		public IconMenuItem() : this( "", null, null, Shortcut.None ) { }


		/// <summary>
		/// Required designer variable.
		/// </summary>
		public IconMenuItem(string text, Icon icon, EventHandler onClick, Shortcut shortcut) :
			base( text, onClick, shortcut )
		{
			OwnerDraw = true;
			m_Font = new Font( "Comic Sans MS", 8 );
			m_Icon = icon;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			
			m_Font.Dispose();
			m_Font = null;
			m_Icon.Dispose();
			m_Icon = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Icon Icon
		{
			get { return m_Icon; }
			set { m_Icon = value; }
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			StringFormat sf = new StringFormat();
        
			sf.HotkeyPrefix = HotkeyPrefix.Show;
			sf.SetTabStops( 60, new float[] { 0 } );

			base.OnMeasureItem( e );

			e.ItemHeight = 22;
			e.ItemWidth = (int) e.Graphics.MeasureString( GetRealText(), m_Font, 10000, sf ).Width + 10;
			sf.Dispose();
			sf = null;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem( e );

			Brush br;
			bool fDisposeBrush = false;

    
			if ( m_Icon != null )
				e.Graphics.DrawIcon( m_Icon, e.Bounds.Left + 2, e.Bounds.Top + 2 );

			Rectangle rcBk = e.Bounds;
			rcBk.X += 24;

			if ( (e.State & DrawItemState.Selected) != 0 ) 
			{
				br = new LinearGradientBrush( rcBk, SystemColors.Highlight, SystemColors.Control, 0f );
				fDisposeBrush = true;
			}
			else
				br = SystemBrushes.Control;

			e.Graphics.FillRectangle( br, rcBk );
			// Only Dispose the brush if we created it, not if it was retrieved from SystemBrushes
			if ( fDisposeBrush )
				br.Dispose();

			br = null;

			StringFormat sf = new StringFormat();
			sf.HotkeyPrefix = HotkeyPrefix.Show;
			sf.SetTabStops( 60, new float[] { 0 } );
			br = new SolidBrush( e.ForeColor );
			e.Graphics.DrawString( GetRealText(), m_Font, br, e.Bounds.Left + 25, e.Bounds.Top + 2, sf );
			br.Dispose();
			br = null;
			sf.Dispose();
			sf = null;

		}

		private string GetRealText()
		{
			string s = Text;

			// Append shortcut if one is set and it should be visible
			if ( ShowShortcut && (Shortcut != Shortcut.None) ) 
			{
				// To get a string representation of a Shortcut value, cast
				// it into a Keys value and use the KeysConverter class (via TypeDescriptor).
				Keys k = (Keys) Shortcut;
				s += "\t" + TypeDescriptor.GetConverter( typeof(Keys) ).ConvertToString( k );
			}
  
			return s;
		}

	}  // class IconMenuItem
}
