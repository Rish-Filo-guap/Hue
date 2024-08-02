using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Hue
{
    internal class Field
    {
        public List<List<Color>> originalField;
        public List<List<Color>> swappedField;
        public List<List<NButton>> nButtons;
        List<List<Color>> stohList = new List<List<Color>>();
        Pnt first= new Pnt(0,0);
        Pnt second= new Pnt(0, 0);
        bool isFirstSelected=false;
        int rows;
        int columns;
        int countswapped=0;
        public Field(int rows, int columns) { 
            this.rows = rows;
            this.columns = columns;

            originalField = new List<List<Color>>();
            swappedField = new List<List<Color>>();
            nButtons = new List<List<NButton>>();

            for (int i = 0; i < columns; i++)
            {
                var tempClist = new List<Color>();
                var tempCOlist = new List<Color>();
                var tempBlist = new List<NButton>();
                for (int j = 0; j < rows; j++)
                {
                    tempClist.Add(Color.Black);
                    tempCOlist.Add(Color.Black);
                    tempBlist.Add(new NButton(i, j));
                    tempBlist[j].Clicked += Field_Clicked;
                }
                swappedField.Add(tempClist);
                originalField.Add(tempCOlist);
                nButtons.Add(tempBlist);
            }



            nButtons[0][0].Text = "●"; 
            nButtons[columns - 1][0].Text = "●"; 
            nButtons[0][rows - 1].Text = "●"; 
            nButtons[columns - 1][rows - 1].Text = "●";
            //int fntsz = 50;
            //nButtons[0][0].FontSize=  (fntsz/columns*rows)* (fntsz / columns * rows);
            //nButtons[columns - 1][0].FontSize = (fntsz / columns * rows) * (fntsz / columns * rows);
            //nButtons[0][rows - 1].FontSize = (fntsz / columns * rows) * (fntsz / columns * rows);
            //nButtons[columns - 1][rows - 1].FontSize = (fntsz / columns * rows) * (fntsz / columns * rows);


            GenField();
            FillButtonsColor();
        }
        public void GenField() {
            Random rnd = new Random();
            double sat = 1;
            isFirstSelected = false;
            
            double hslide=15;
            double rndColor = rnd.Next(0, 100);
            int x=9; int y=9;
            int[] xys = new int[5]{ 9, 17, 33, 65, 129 };

            for (int i = 4; i >= 0; i--) {
                if (Math.Max(rows, columns) >= xys[i]) {
                    x = xys[i + 1];
                    y = xys[i + 1];
                }
            }

            
            
            for (int i = 0; i < x; i++) {
                List<Color> tmplist = new List<Color>();
                for (int j = 0; j < y; j++) {
                    tmplist.Add(Color.Aqua);
                }
                stohList.Add(tmplist);
            }

            stohList[0][0] = Color.FromHsv((double)((rndColor) % 100) / 100, sat, 1);

            stohList[x - 1][0] = Color.FromHsv((double)((rndColor + hslide) % 100) / 100, sat, 1);

            stohList[0][y - 1] = Color.FromHsv((double)((rndColor + 2 * hslide) % 100) / 100, sat, 1);

            stohList[x - 1][y - 1] = Color.FromHsv((double)((rndColor + 3 * hslide) % 100) / 100, sat, 1);

            GenGrad(new Pnt(0, 0), new Pnt(x - 1, 0), new Pnt(0, y - 1), new Pnt(x - 1, y - 1));




            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    originalField[i][j]=stohList[i][j];
                    swappedField[i][j]=originalField[i][j];
                    nButtons[i][j].BorderWidth = 0;

                }
            }
        }
        public void FillButtonsColor()
        {

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    nButtons[i][j].BackgroundColor = swappedField[i][j];
                    
                }
            }

        }
        public void SwapColors() { 
            
            Random rnd = new Random();
            int i = 0;
            while (i < (int)(0.4 * rows * columns)){ 
                
                Pnt fir=new Pnt(rnd.Next(0, columns), rnd.Next(0, rows));
                Pnt sec=new Pnt(rnd.Next(0, columns), rnd.Next(0, rows));
                if (!(fir.x == sec.x && fir.y == sec.y) && nButtons[fir.x][fir.y].Text != "●" && nButtons[sec.x][sec.y].Text != "●") { 
                    Color tmp = swappedField[fir.x][fir.y];
                    swappedField[fir.x][fir.y] = swappedField[sec.x][sec.y];
                    swappedField[sec.x][sec.y] = tmp;
                    i++;
                }
            }
            FillButtonsColor();
        }

        private void Field_Clicked(object sender, EventArgs e)
        {
            if (((NButton)sender).Text != "●")
            {
                if (isFirstSelected)
                {
                    second.x = ((NButton)sender).x;
                    second.y = ((NButton)sender).y;
                    isFirstSelected = false;
                    ChangeButtons();
                    
                    nButtons[first.x][first.y].BorderWidth = 0;
                   

                }
                else
                {
                    first.x = ((NButton)sender).x;
                    first.y = ((NButton)sender).y;
                    isFirstSelected = true;
                    ((NButton)sender).BorderWidth += 3;
                    ((NButton)sender).BorderColor = Color.White;
                }
            }
        }
        private void ChangeButtons() {
            Color color = swappedField[first.x][first.y];

            swappedField[first.x][first.y] = swappedField[second.x][second.y];
            nButtons[first.x][first.y].BackgroundColor = nButtons[second.x][second.y].BackgroundColor;

            nButtons[second.x][second.y].BackgroundColor = color;
            swappedField[second.x][second.y] = color;
            IsWin();
        }
        private void IsWin() {
            countswapped = 0;
            for (int i = 0; i < columns; i++) { 
                for (int j = 0; j < rows; j++)
                {
                    if (swappedField[i][j].Hue != originalField[i][j].Hue) { 
                        countswapped++;
                    }

                }
            
            }
           // nButtons[1][1].Text=countswapped.ToString();
            if (countswapped == 0)
            {
                FieldWinAnim(second);
                
            }
        }
        private async void WinAnim(Pnt coords) {
            if (coords.x >= 0 && coords.x < columns && coords.y >= 0 && coords.y < rows) { 
            await Task.Delay(100);
            await nButtons[coords.x][coords.y].FadeTo(0.75, 200);
            await Task.Delay(100);
            await nButtons[coords.x][coords.y].FadeTo(1, 300);
            }

        }
        private async void FieldWinAnim(Pnt start) {
            WinAnim(start);
            for (int i = 1; i < Math.Max(rows, columns); i++) {

                for (int j = -i + 1; j < i; j++)
                {
                    WinAnim(new Pnt(start.x + i, start.y + j));
                    WinAnim(new Pnt(start.x - i, start.y + j));
                }
                for (int j = -i; j <= i; j++)
                {
                    WinAnim(new Pnt(start.x + j, start.y - i));
                    WinAnim(new Pnt(start.x + j, start.y + i));

                }
                await Task.Delay(100);
            }
        }
        private void GenGrad(Pnt lu, Pnt ru, Pnt ld, Pnt rd)
        {

            if ((lu.x + ru.x) % 2 == 0 || (lu.y + ld.y) % 2 == 0 || (ru.y + rd.y % 2) == 0 || (ld.x + rd.x) % 2 == 0)
            {
                GenAveColor( lu, ru, ld, rd);
                GenAveColor(lu, ru);
                GenAveColor(ld, lu);
                GenAveColor(ru, rd);
                GenAveColor(ld, rd);
                GenGrad(lu, new Pnt((lu.x + ru.x) / 2, (lu.y + ru.y) / 2), new Pnt((ld.x + lu.x) / 2, (ld.y + lu.y) / 2), new Pnt((lu.x + ru.x + ld.x + rd.x) / 4, (lu.y + ru.y + ld.y + rd.y) / 4));
                GenGrad(new Pnt((lu.x + ru.x) / 2, (lu.y + ru.y) / 2), ru, new Pnt((lu.x + ru.x + ld.x + rd.x) / 4, (lu.y + ru.y + ld.y + rd.y) / 4), new Pnt((ru.x + rd.x) / 2, (ru.y + rd.y) / 2));
                GenGrad(new Pnt((ld.x + lu.x) / 2, (ld.y + lu.y) / 2), new Pnt((lu.x + ru.x + ld.x + rd.x) / 4, (lu.y + ru.y + ld.y + rd.y) / 4), ld, new Pnt((ld.x + rd.x) / 2, (ld.y + rd.y) / 2));
                GenGrad(new Pnt((lu.x + ru.x + ld.x + rd.x) / 4, (lu.y + ru.y + ld.y + rd.y) / 4), new Pnt((ru.x + rd.x) / 2, (ru.y + rd.y) / 2), new Pnt((ld.x + rd.x) / 2, (ld.y + rd.y) / 2), rd);
            }
            else return;
        }
        private void GenAveColor(Pnt lu, Pnt ru, Pnt ld, Pnt rd)
        {
            stohList[(lu.x + ru.x + ld.x + rd.x) / 4][(lu.y + ru.y + ld.y + rd.y) / 4] = new Color(
                              (stohList[lu.x][lu.y].R + stohList[ru.x][ru.y].R + stohList[ld.x][ld.y].R + stohList[rd.x][rd.y].R) / 4,
                              (stohList[lu.x][lu.y].G + stohList[ru.x][ru.y].G + stohList[ld.x][ld.y].G + stohList[rd.x][rd.y].G) / 4,
                              (stohList[lu.x][lu.y].B + stohList[ru.x][ru.y].B + stohList[ld.x][ld.y].B + stohList[rd.x][rd.y].B) / 4);
                              
        }
        private void GenAveColor(Pnt f, Pnt s)
        {
            stohList[(f.x + s.x) / 2][(f.y + s.y) / 2] = new Color(
                              (stohList[f.x][f.y].R + stohList[s.x][s.y].R) / 2,
                              (stohList[f.x][f.y].G + stohList[s.x][s.y].G) / 2,
                              (stohList[f.x][f.y].B + stohList[s.x][s.y].B) / 2);
        }
    }
}
