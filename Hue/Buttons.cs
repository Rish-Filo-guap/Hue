using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hue
{
    internal class NButton:Button
    {
        public int x;
        public int y;
        public NButton(int x, int y): base()
        {
            this.x = x;
            this.y = y;
            //Text=x.ToString() +"," + y.ToString();
            TextColor = Color.Black;
            //FontSize = 5;
            
            CornerRadius = 0;
        }
    }
    internal class CButton : Button {

        public CButton(string name) : base() { 
            Text = name;
            
            BorderWidth = 0;
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            CornerRadius = 10;
        }
    }
}
