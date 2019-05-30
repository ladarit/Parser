using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;

namespace GovernmentParse.Controls
{
    public partial class BaseWorkAreaControl : UserControl
    {
        public readonly ResourceReader ResourceReader;

        public static int Multiplie(int a, double b) { return (int)(a * b); }

        public static int Divide(int a, double b) { return (int)Math.Ceiling(a / b); }

        public delegate int Operation(int a, double b);

        public static Operation Calculate;

        public BaseWorkAreaControl()
        {
            InitializeComponent();
            ResourceReader = new ResourceReader();
        }

        public void ChangeButtonStyleAndState(Control ctrlContainer, bool state)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                    ctrl.Enabled = state;
                if (ctrl.HasChildren)
                    ChangeButtonStyleAndState(ctrl, state);
            }
        }

        public virtual void SetElementSize(double widthCoef, double heightCoef, bool isEnlarge = false)
        {
            if (isEnlarge)
                Calculate = Multiplie;
            else
                Calculate = Divide;
        }

        public int CalcVerticalDistanceBetweenControls(Control lowerControl, Control upperControl, int distance)
        {
            return lowerControl.Location.Y - upperControl.Height - distance;
        }

        public List<string> FillConvocationList(int y)
        {
            var convocationList = new List<string>();
            for (int i = ConvocationDeterminant.ConvocationNumberForForm; i >= y; i--)
                convocationList.Add(RomanArabicNumerals.ToRoman(i - 1) + " скликання");
            return convocationList;
        }
    }
}
