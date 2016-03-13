using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalculadoraXamarinTest
{
    public class CalculadoraTest : ContentPage
    {
        public Label pantalla;
        private int buffer;
        private bool isFirstNumber;
        private bool isAnAnswer;
        private char opBuffer;

        public CalculadoraTest()
        {
            Init();
        }

        private void Init()
        {
            isFirstNumber = true;
            isAnAnswer = true;
            opBuffer = '\0';
            var superLayout = new AbsoluteLayout();
            pantalla = new Label() { Text = string.Empty };
            
            AbsoluteLayout.SetLayoutBounds(pantalla, new Rectangle(0, 0, 1, 0.2));
            AbsoluteLayout.SetLayoutFlags(pantalla, AbsoluteLayoutFlags.All);
            
            var buttons = BuildButtonsGrid();
            AbsoluteLayout.SetLayoutBounds(buttons, new Rectangle(0, 0.25, 1, 0.7));
            AbsoluteLayout.SetLayoutFlags(buttons, AbsoluteLayoutFlags.All);

            superLayout.Children.Add(pantalla);
            superLayout.Children.Add(buttons);
            

            Content = superLayout;
        }

        private Grid BuildButtonsGrid()
        {
            var grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int i = 9;
            for (int x = 0; x < 3; ++x)
            {
                for (int y = 2; y >= 0; --y)
                {
                    var btn = new Button()
                    {
                        Text = string.Format("{0}", i--),
                        FontSize = 21
                    };
                    btn.Clicked += AddNumber;
                    grid.Children.Add(btn, y, x);
                }
            }
            
            var btnC = new Button() { Text = "C" };
            btnC.Clicked += AddOperando;
            
            var btn0 = new Button() { Text = "0" };
            btn0.Clicked += AddNumber;

            var btnDiv = new Button() { Text = "%" };
            btnDiv.Clicked += AddOperando;

            var btnMul = new Button() { Text = "x" };
            btnMul.Clicked += AddOperando;

            var btnRes = new Button() { Text = "-" };
            btnRes.Clicked += AddOperando;

            var btnEq = new Button() { Text = "=" };
            btnEq.Clicked += AddOperando;

            var btnPlus = new Button() { Text = "+" };
            btnPlus.Clicked += AddOperando;

            grid.Children.Add(btnDiv, 3, 0);
            grid.Children.Add(btnMul, 3, 1);
            grid.Children.Add(btnRes, 3, 2);
            grid.Children.Add(btnC, 0, 3);
            grid.Children.Add(btn0, 1, 3);
            grid.Children.Add(btnEq, 2, 3);
            grid.Children.Add(btnPlus, 3, 3);
            return grid;
        }

        public void AddNumber(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var text = btn.Text;
            if (isAnAnswer)
                pantalla.Text = text;
            else
                pantalla.Text += text;
            isAnAnswer = false;
        }

        public void AddOperando(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pantalla.Text))
                return;
            var btn = sender as Button;
            var oldBuffer = buffer;
            buffer = Int32.Parse(pantalla.Text);
            isAnAnswer = true;

            var txt = btn.Text;
            switch(txt)
            {
                case "%":
                    if (isFirstNumber)
                    {
                        isFirstNumber = false;
                    }
                    else
                    {
                        ShowAnswer(oldBuffer, buffer, opBuffer);
                    }
                    opBuffer = '%';
                    break;
                case "x":
                    if (isFirstNumber)
                    {
                        isFirstNumber = false;
                    }
                    else
                    {
                        ShowAnswer(oldBuffer, buffer, opBuffer);
                    }
                    opBuffer = 'x';
                    break;
                case "-":
                    if (isFirstNumber)
                    {
                        isFirstNumber = false;
                    }
                    else
                    {
                        ShowAnswer(oldBuffer, buffer, opBuffer);
                    }
                    opBuffer = '-';
                    break;
                case "=":
                    ShowAnswer(oldBuffer, buffer, opBuffer);
                    isFirstNumber = true;
                    break;
                case "+":
                    if (isFirstNumber)
                    {
                        isFirstNumber = false;
                    }
                    else
                    {
                        ShowAnswer(oldBuffer, buffer, opBuffer);
                    }
                    opBuffer = '+';
                    break;
                case "C":
                    pantalla.Text = string.Empty;
                    buffer = 0;
                    isFirstNumber = true;
                    break;
            }
            
        }

        private void ShowAnswer(int num1, int num2, char oper)
        {
            switch (oper)
            {
                case '%':
                    if (num2 == 0)
                    {
                        pantalla.Text = "0";
                        break;
                    }
                    pantalla.Text = string.Format("{0}", (num1 / num2));
                    break;
                case 'x':
                    pantalla.Text = string.Format("{0}", (num1 * num2));
                    break;
                case '-':
                    pantalla.Text = string.Format("{0}", (num1 - num2));
                    break;
                case '+':
                    pantalla.Text = string.Format("{0}", (num1 + num2));
                    break;
            }
            isAnAnswer = true;
            buffer = Int32.Parse(pantalla.Text);
        }
    }
}
