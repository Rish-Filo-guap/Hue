using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPage : TabbedPage
    {
        public TabbedMainPage()
        {
            InitializeComponent();

            Children.Add (new HuePage(9, 9));
            Children.Add (new HuePage(6, 6));
            Children.Add (new HuePage(4, 4));
            Children.Add (new HuePage(7, 12));
            Children.Add (new HuePage(10, 10));
            Children.Add (new HuePage(12, 12));
            
            BarBackgroundColor = Color.Black;
        }
    }
}