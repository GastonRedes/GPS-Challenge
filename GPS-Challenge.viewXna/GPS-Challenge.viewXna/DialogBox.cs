using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace GPS_Challenge.ViewXNA
{
    public class DialogBox : WindowControl
    {
        
        /// <summary>A label used ask the user to enter his name</summary>
        private Nuclex.UserInterface.Controls.LabelControl nameEntryLabel;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl okButton;

        public DialogBox(String title, String texto)
        {
            this.nameEntryLabel = new LabelControl();
            this.okButton = new ButtonControl();
      
          //
          // nameEntryLabel
          //
          this.nameEntryLabel.Text = texto;
          this.nameEntryLabel.Bounds = new UniRectangle(10.0f, 30.0f, 110.0f, 24.0f);
      
          //
          // okButton
          //
          this.okButton.Bounds = new UniRectangle(10.0f,50.0f,50.0f,24.0f);
          this.okButton.Text = "OK";
          this.okButton.Pressed += new EventHandler(okClicked);
          
          //
          // DialogBox
          //
          this.Bounds = new UniRectangle(200.0f, 200.0f, .0f, 384.0f);
          this.Title = title;
          Children.Add(this.nameEntryLabel);
          Children.Add(this.okButton);
        }

        /// <summary>Called when the user clicks on the okay button</summary>
        /// <param name="sender">Button the user has clicked on</param>
        /// <param name="arguments">Not used</param>
        private void okClicked(object sender, EventArgs arguments)
        {
            Close();
        }
    }
}
