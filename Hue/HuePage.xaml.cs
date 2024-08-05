using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Hue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HuePage : ContentPage
    {
        public HuePage(int Ccount, int Rcount)
        {
            InitializeComponent();
            Title = Ccount.ToString()+ "x" + Rcount.ToString();
            StackLayout stackLayout = new StackLayout() {
                BackgroundColor = Color.Black,

            };
            
            Field field = new Field(Rcount, Ccount);


            Grid fieldGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.White,
                
            };
            for (int i = 0; i < Ccount; i++) {
                fieldGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }
            for (int j = 0; j < Rcount; j++){
                fieldGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });  
            }
            

            for (int i = 0; i < Ccount; i++)
            {
                for (int j = 0; j < Rcount; j++)
                {
                    fieldGrid.Children.Add(field.nButtons[i][j], i, j);
                }
            }
            stackLayout.Children.Add(fieldGrid);

            Grid buttonsGrid = new Grid()
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 5,
                RowSpacing = 0,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(50) },

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star},
                    new ColumnDefinition { Width = GridLength.Star},
                    new ColumnDefinition { Width = GridLength.Star}
                }


            };
            CButton buttonSwap = new CButton("Перемешать");
            buttonSwap.VerticalOptions = LayoutOptions.Center;
            buttonSwap.Clicked += ButtonSwap_Clicked;

            CButton buttonTip = new CButton("Подсказка");
            buttonTip.Clicked += ButtonTip_Clicked;

            CButton buttonNew = new CButton("Новая");
            buttonNew.Clicked += ButtonNew_Clicked;
            


            buttonsGrid.Children.Add(buttonSwap, 0, 0);
            buttonsGrid.Children.Add(buttonTip, 1, 0);
            buttonsGrid.Children.Add(buttonNew, 2, 0);


            stackLayout.Children.Add(buttonsGrid);
            Content = stackLayout;


            async void ButtonNew_Clicked(object sender, EventArgs e)
            {
                if (!field.IsWin()) {
                    bool result = await DisplayAlert("Подтвердить действие", "Создать новый?", "Да", "Нет");
                    if (result) {
                        field.GenField();
                        field.FillButtonsColor();
                    }
                }
                else
                {
                    field.GenField();
                    field.FillButtonsColor();
                }
            }
            void ButtonSwap_Clicked(object sender, EventArgs e)
            {
                field.SwapColors();
            }
            void ButtonTip_Clicked(object sender, EventArgs e)
            {
                field.GetTip();
            }
            
        }

    }
    } 
